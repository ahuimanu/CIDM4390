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
    public TodoDb(DbContextOptions<TodoDb> options)
        : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();
}