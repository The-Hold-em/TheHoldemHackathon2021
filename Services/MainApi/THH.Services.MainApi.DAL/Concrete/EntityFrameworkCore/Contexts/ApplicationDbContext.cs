#nullable disable
using Microsoft.EntityFrameworkCore;

using THH.Services.MainApi.Entities.Concrete;

namespace THH.Services.MainApi.DAL.Concrete.EntityFrameworkCore.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(City).Assembly);
    }

    public DbSet<Node> Nodes { get; set; }
    public DbSet<PollingStation> PollingStations { get; set; }
    public DbSet<Election> Elections { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<District> Districts { get; set; }
}
