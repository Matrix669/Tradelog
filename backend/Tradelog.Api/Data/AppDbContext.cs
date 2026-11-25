using Microsoft.EntityFrameworkCore;
using Tradelog.Api.Models;

namespace Tradelog.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Trade> Trades => Set<Trade>();
    public DbSet<Strategy> Strategies => Set<Strategy>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trade>(e =>
        {
            e.Property(t => t.Direction).HasConversion<string>();
            e.Property(t => t.InstrumentType).HasConversion<string>();
            e.Property(t => t.Instrument).HasMaxLength(50);
        });
        modelBuilder.Entity<Strategy>(e =>
        {
            e.Property(s => s.Name).HasMaxLength(100);
        });
    }
}