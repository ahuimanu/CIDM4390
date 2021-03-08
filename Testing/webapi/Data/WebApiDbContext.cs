using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data
{
    public class WebApiDbContext_OFF : DbContext
    {
        public WebApiDbContext_OFF (DbContextOptions<WebApiDbContext_OFF> options)
            : base(options)
        {
        }

        public DbSet<NOAAStation> Stations { get; set; }

        public DbSet<webapi.Models.VatsimMETAR> VatsimMETAR { get; set; }

        // public DbSet<Student> Students { get; set; }
        // public DbSet<Enrollment> Enrollments { get; set; }
        // public DbSet<Course> Courses { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Course>().ToTable("Course");
        //     modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
        //     modelBuilder.Entity<Student>().ToTable("Student");
        // }
    }
}