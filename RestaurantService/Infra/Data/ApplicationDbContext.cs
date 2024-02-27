using Microsoft.EntityFrameworkCore;
using RestaurantService.Infra.Entities;

namespace RestaurantService.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Restaurant> Restaurants { get; set; }
}
