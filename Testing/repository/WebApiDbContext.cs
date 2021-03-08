using System;
using Microsoft.EntityFrameworkCore;
using domain;
using domain.NOAAStationAggregate;
using domain.VatsimMETARAggregate;

namespace repository
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext (DbContextOptions<WebApiDbContext> options)
            : base(options){}

        public DbSet<NOAAStation> Stations { get; set; }

        public DbSet<VatsimMETAR> VatsimMETARs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<NOAAStation>().HasKey(n => n.ICAO);
            //composite key: https://docs.microsoft.com/en-us/ef/core/modeling/keys?tabs=data-annotations
            modelBuilder.Entity<VatsimMETAR>().HasKey(n => new { n.ICAO, n.RetreivedTimeStamp, n.ObservationTime });
        }
    }
}