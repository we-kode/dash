using Dash.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Dash.Persistence;

public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Information> Informations { get; set; }
    public DbSet<Calendar> Calendars { get; set; }
    public DbSet<CalendarEvent> CalendarEvents { get; set; }
    public DbSet<Display> Displays { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<Element> Elements { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        builder.Entity<Component>()
            .HasIndex(c => c.Identifier)
            .IsUnique();
    }
}
