namespace Services.CaptivePortalDataService;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Services.CaptivePortalEmailService;
using Services.CaptivePortalProfileJobService;
using Services.CaptivePortalProfileService;


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

    //add a Guest Profile
    public async Task<GuestProfile> AddGuestProfileAsync(GuestProfile profile)
    {
        using (var db = new CaptivePortalDbContext())
        {
            db.GuestProfiles.Add(profile);
            await db.SaveChangesAsync();
        }
        return profile;
    }

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

}