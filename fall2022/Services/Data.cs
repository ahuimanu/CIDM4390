namespace Services.Data;

using Microsoft.EntityFrameworkCore;

record class Airport
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}

record class METARReport
{
    
}

class ApiDb : DbContext
{
    public ApiDb(DbContextOptions<ApiDb> options)
        : base(options) { }

    public DbSet<Airport> Airports => Set<Airport>();
}