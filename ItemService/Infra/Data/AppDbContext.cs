using ItemService.Infra.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemService.Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Item> Items { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
}
