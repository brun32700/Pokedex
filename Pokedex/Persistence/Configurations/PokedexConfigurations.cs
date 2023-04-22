using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokedex.Models;

namespace Pokedex.Persistence.Configurations;

public class PokedexConfigurations : IEntityTypeConfiguration<Pokemon>
{
    public void Configure(EntityTypeBuilder<Pokemon> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .HasMaxLength(Pokemon.NameMaximumLength);

        builder.Property(p => p.Description)
            .HasMaxLength(Pokemon.DescriptionMaximumLength);

        builder.Property(p => p.Type)
            .HasConversion(
                types => string.Join(",", types),
                types => types.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
                new ValueComparer<List<string>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));

        builder.Property(p => p.Weaknesses)
            .HasConversion(
                weaknesses => string.Join(",", weaknesses),
                weaknesses => weaknesses.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
                new ValueComparer<List<string>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));
    }
}