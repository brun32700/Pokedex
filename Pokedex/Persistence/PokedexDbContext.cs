using Microsoft.EntityFrameworkCore;
using Pokedex.Models;

namespace Pokedex.Persistence;

public class PokedexDbContext : DbContext
{
    public DbSet<Pokemon> Pokemons { get; set; } = null!;

    public PokedexDbContext(DbContextOptions<PokedexDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PokedexDbContext).Assembly);
    }
}