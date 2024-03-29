namespace Services.WeatherDataService;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Services.AirportInfoService;
using Services.WeatherReportJobService;

public class WeatherDbContext : DbContext
{

    public WeatherDbContext()
    {
        // important for SQLite as it is file-based
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
        modelBuilder.Entity<WeatherReportJobResult>().ToTable("WeatherReportJobResults");

        base.OnModelCreating(modelBuilder);
    }

    // DB Sets
    public DbSet<Airport> Airports => Set<Airport>();
    public DbSet<WeatherReportJob> WeatherReportJobs => Set<WeatherReportJob>();
    public DbSet<WeatherReportJobResult> WeatherReportJobResults => Set<WeatherReportJobResult>();

    //add a weather report job
    public async Task<WeatherReportJob> AddWeatherReportJobAsync(WeatherReportJob job)
    {
        using (var db = new WeatherDbContext())
        {
            db.WeatherReportJobs.Add(job);
            await db.SaveChangesAsync();
        }
        return job;
    }

    //add a weather report job
    public async Task<WeatherReportJobResult> AddWeatherReportJobResultAsync(WeatherReportJobResult result)
    {
        using (var db = new WeatherDbContext())
        {
            db.WeatherReportJobResults.Add(result);
            await db.SaveChangesAsync();
        }
        return result;
    }

    public bool CheckJobTimer(WeatherReportJob job)
    {
        TimeSpan difference = DateTime.Now - job.JobScheduledAt;
        return difference.TotalMinutes >= job.JobFrequencyInMinutes ? true : false;
    }    

    public async Task<WeatherReportJob> GetWeatherReportJobByIdAsync(int id)
    {

        WeatherReportJob? job;
        using (var db = new WeatherDbContext())
        {
            job = await db.WeatherReportJobs
                          .Where(r => r.ID == id)
                          .FirstOrDefaultAsync<WeatherReportJob>();

        }
        return job!;
    }

    public async Task UpdateJobTimeStampByIdAync(int id)
    {
        WeatherReportJob? job;
        using (var db = new WeatherDbContext())
        {
            job = await db.WeatherReportJobs
                          .Where(r => r.ID == id)
                          .FirstOrDefaultAsync<WeatherReportJob>();

            if(job != null){
                job.JobScheduledAt = DateTime.Now;
                //save changes
                await db.SaveChangesAsync();
            }                          
        }
    }

    public async Task<List<WeatherReportJob>> GetWeatherReportJobsDueAsync()
    {
        List<WeatherReportJob> currentJobs;

        using (var db = new WeatherDbContext())
        {

            //TODO - Create Time Delta
            //TODO - use EntityFunctions DiffMinutes
            //https://learn.microsoft.com/en-us/dotnet/api/system.data.objects.entityfunctions.diffminutes?view=netframework-4.8#system-data-objects-entityfunctions-diffminutes(system-nullable((system-datetime))-system-nullable((system-datetime)))


            var allJobs = await db.WeatherReportJobs.ToListAsync<WeatherReportJob>();

            // hard-coding this right now, would need to change in the future
            // this is handled entirely in memory right now as well - not good if the number of jobs gets larger
            // would be fixed by using a better database provider.
            currentJobs = allJobs
                .Where(j => (Math.Abs(DateTime.Now.Minute - j.JobScheduledAt.Minute)) > j.JobFrequencyInMinutes)
                .ToList<WeatherReportJob>();

        }
        return currentJobs!;
    }

    /// <summary>
    /// Calls web api service to obtain a list of currently scheduled jobs
    /// </summary>
    /// <returns>List of WeatherReportJobs as JSON</returns>
    public async Task<List<WeatherReportJob>> GetWeatherReportJobsAsync()
    {
        List<WeatherReportJob> jobs = new List<WeatherReportJob>();
        using (var db = new WeatherDbContext())
        {
            jobs = await db.WeatherReportJobs.ToListAsync<WeatherReportJob>();
        }
        return jobs;
    }
}


