namespace Services.DataService;

using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Services.AirportInfoService;
using Services.WeatherReportJobService;

public class ApiDbContext : DbContext
{
    // public ApiDb(DbContextOptions<ApiDb> options)
    //     : base(options) { }
    public ApiDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // https://entityframeworkcore.com/providers-sqlite
        //var dirPath = Assembly.GetCallingAssembly().Location;
        //var dirPath = Assembly.GetAssembly(Type.GetType("Services.DataService.ApiDbContext")).Location;
        var dirPath = Assembly.GetExecutingAssembly().Location;
        dirPath = Path.GetDirectoryName(dirPath);

        Console.WriteLine(dirPath);

        string dbfilename = "ApiDB.sqlite";
        string connectionString = Path.GetFullPath(Path.Combine(dirPath, dbfilename));

        Console.WriteLine(connectionString);

        //use this to configure the contex
        // "DefaultConnection": "DataSource=./data/database/sqlite.db"
        optionsBuilder.UseSqlite($"DataSource={connectionString}");
    }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //use this to configure the model
        // modelBuilder.Entity<Airport>().ToTable("Airports");
        // modelBuilder.Entity<WeatherReportJob>().ToTable("WeatherReportJobs");

        base.OnModelCreating(modelBuilder);
    }    

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

    public async Task<List<WeatherReportJob>> GetWeatherReportJobsAsync(){

        List<WeatherReportJob> jobs = new List<WeatherReportJob>();
        using (var db = new ApiDbContext())
        {
            jobs = await db.WeatherReportJobs.ToListAsync<WeatherReportJob>();
        }        
        return jobs;
    }

}


