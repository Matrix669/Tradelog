using Microsoft.EntityFrameworkCore;
namespace Tradelog.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // tu później: public DbSet<Trade> Trades => Set<Trade>();
}