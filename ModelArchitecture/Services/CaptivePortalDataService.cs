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

        base.OnModelCreating(modelBuilder);
    }

    // DB Sets
    public DbSet<GuestProfile> GuestProfiles => Set<GuestProfile>();
    public DbSet<GuestProfileJob> GuestProfileJob => Set<GuestProfileJob>();

    //add a Guest Profile job
    public async Task<GuestProfileJob> AddGuestProfileJobAsync(GuestProfileJob job)
    {
        using (var db = new CaptivePortalDbContext())
        {
            db.GuestProfileJob.Add(job);
            await db.SaveChangesAsync();
        }
        return job;
    }

    //add a Guest Profile

    //add a weather report job
    public async Task<GuestProfileJobResult> AddGuestProfileJobResultAsync(GuestProfileJobResult result)
    {
        using (var db = new CaptivePortalDbContext())
        {
            db.GuestProfiles.Add(result);
            await db.SaveChangesAsync();
        }
        return result;
    }
}


