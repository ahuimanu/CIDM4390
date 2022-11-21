namespace Services.DataService;

using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Services.AirportInfoService;
using Services.WeatherReportJobService;

public class ApiDbContext : DbContext
{

    public ApiDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // https://entityframeworkcore.com/providers-sqlite
        //var dirPath = Assembly.GetCallingAssembly().Location;
        //var dirPath = Assembly.GetAssembly(Type.GetType("Services.DataService.ApiDbContext")).Location;
        
        // this finds where the data service library is being called from and 
        // places the SQLite file right next to the DLL
        string? dirPath = Assembly.GetExecutingAssembly().Location;
        dirPath = Path.GetDirectoryName(dirPath);

        // DEBUG CHECK
        //Console.WriteLine(dirPath);

        string dbfilename = "ApiDB.sqlite";
        string connectionString = Path.GetFullPath(Path.Combine(dirPath!, dbfilename));

        // DEBUG CHECK
        //Console.WriteLine(connectionString);

        //use this to configure the context
        optionsBuilder.UseSqlite($"DataSource={connectionString}");
    }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //use this to configure the model
        modelBuilder.Entity<Airport>().ToTable("Airports");
        modelBuilder.Entity<WeatherReportJob>().ToTable("WeatherReportJobs");

        base.OnModelCreating(modelBuilder);
    }    

    // DB Sets
    public DbSet<Airport> Airports => Set<Airport>();
    public DbSet<WeatherReportJob> WeatherReportJobs => Set<WeatherReportJob>();

    //add a weather report job
    public async Task<WeatherReportJob> AddWeatherReportJobAsync(WeatherReportJob job){
        using (var db = new ApiDbContext())
        {
            db.WeatherReportJobs.Add(job);
            await db.SaveChangesAsync();
        }        
        return job;
    }

    /// <summary>
    /// Calls web api service to obtain a list of currently scheduled jobs
    /// </summary>
    /// <returns>List of WeatherReportJobs as JSON</returns>
    public async Task<List<WeatherReportJob>> GetWeatherReportJobsAsync(){

        List<WeatherReportJob> jobs = new List<WeatherReportJob>();
        using (var db = new ApiDbContext())
        {
            jobs = await db.WeatherReportJobs.ToListAsync<WeatherReportJob>();
        }        
        return jobs;
    }
}


