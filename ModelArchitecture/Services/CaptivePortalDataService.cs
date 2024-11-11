namespace Services.CaptivePortalDataService;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Services.CaptivePortalEmailService;
using Services.CaptivePortalProfileService;
using Services.CaptivePortalProfileJobService;


public class CaptivePortalDbContext : DbContext
{

    public CaptivePortalDbContext()
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
        modelBuilder.Entity<GuestProfile>().ToTable("GuestProfiles");
        modelBuilder.Entity<GuestProfileJob>().ToTable("GuestProfileJobs");
        modelBuilder.Entity<GuestProfileJobResult>().ToTable("GuestProfileJobResults");

        base.OnModelCreating(modelBuilder);
    }

    // DB Sets
    public DbSet<GuestProfile> GuestProfiles => Set<GuestProfile>();
    public DbSet<GuestProfileJob> GuestProfileJobs => Set<GuestProfileJob>();
    public DbSet<GuestProfileJobResult> GuestProfileJobResults => Set<GuestProfileJobResult>();

    //add a Guest Profile job
    public async Task<GuestProfileJob> AddGuestProfileJobAsync(GuestProfileJob job)
    {
        using (var db = new CaptivePortalDbContext())
        {
            db.GuestProfileJobs.Add(job);
            await db.SaveChangesAsync();
        }
        return job;
    }

    //add a Guest Profile

    //add a Guest Profile job report
    public async Task<GuestProfileJobResult> AddGuestProfileJobResultAsync(GuestProfileJobResult result)
    {
        using (var db = new CaptivePortalDbContext())
        {
            db.GuestProfileJobResults.Add(result);
            await db.SaveChangesAsync();
        }
        return result;
    }

    public bool CheckJobTimer(GuestProfileJob job)
    {
        // TimeSpan difference = DateTime.Now - job.JobScheduledAt;
        // return difference.TotalMinutes >= job.JobFrequencyInMinutes ? true : false;
        return true;
    }

    public async Task<GuestProfileJob> GetGuestProfileJobByIdAsync(int id)
    {

        GuestProfileJob? job;
        using (var db = new CaptivePortalDbContext())
        {
            job = await db.GuestProfileJobs
                          .Where(job => job.ID == id)
                          .FirstOrDefaultAsync<GuestProfileJob>();

        }
        return job!;
    }

    public async Task UpdateJobTimeStampByIdAync(int id)
    {
        GuestProfileJob? job;
        using (var db = new CaptivePortalDbContext())
        {
            job = await db.GuestProfileJobs
                          .Where(job => job.ID == id)
                          .FirstOrDefaultAsync<GuestProfileJob>();

            if (job != null)
            {
                job.JobScheduledAt = DateTime.Now;
                //save changes
                await db.SaveChangesAsync();
            }
        }
    }

    public async Task<List<GuestProfileJob>> GetWeatherReportJobsDueAsync()
    {
        List<GuestProfileJob> currentJobs;

        using (var db = new CaptivePortalDbContext())
        {

            //TODO - Create Time Delta
            //TODO - use EntityFunctions DiffMinutes
            //https://learn.microsoft.com/en-us/dotnet/api/system.data.objects.entityfunctions.diffminutes?view=netframework-4.8#system-data-objects-entityfunctions-diffminutes(system-nullable((system-datetime))-system-nullable((system-datetime)))


            var allJobs = await db.GuestProfileJobs.ToListAsync<GuestProfileJob>();

            // hard-coding this right now, would need to change in the future
            // this is handled entirely in memory right now as well - not good if the number of jobs gets larger
            // would be fixed by using a better database provider.
            currentJobs = allJobs
                .Where(j => (Math.Abs(DateTime.Now.Minute - j.JobScheduledAt.Minute)) > j.JobFrequencyInMinutes)
                .ToList<GuestProfileJob>();

        }
        return currentJobs!;
    }

    /// <summary>
    /// Calls web api service to obtain a list of currently scheduled jobs
    /// </summary>
    /// <returns>List of GuestProfileJobs as JSON</returns>
    public async Task<List<GuestProfileJob>> GetGuestProfileJobsAsync()
    {
        List<GuestProfileJob> jobs = new List<GuestProfileJob>();
        using (var db = new CaptivePortalDbContext())
        {
            jobs = await db.GuestProfileJobs.ToListAsync<GuestProfileJob>();
        }
        return jobs;
    }

}
