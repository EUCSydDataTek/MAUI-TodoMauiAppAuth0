using Microsoft.EntityFrameworkCore;

namespace TodoApiAuth0.Data;

public class TodoDbContext : DbContext
{
    public DbSet<TodoItem> TodoItems { get; set; } = null!;

    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<TodoItem>().HasData(
                        new TodoItem
                        {
                            Id = 1,
                            Description = "Learn ASP.NET Core",
                            Priority = PriorityLevel.Low,
                            Completed = false
                        },
                        new TodoItem
                        {
                            Id = 2,
                            Description = "Build awesome apps",
                            Priority = PriorityLevel.Normal,
                            Completed = false
                        },
                        new TodoItem
                        {
                            Id = 3,
                            Description = "Have a life!",
                            Priority = PriorityLevel.High,
                            Completed = true
                        }
                    );

        base.OnModelCreating(modelBuilder);
    }
}
