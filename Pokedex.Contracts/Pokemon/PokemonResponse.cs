namespace Pokedex.Contracts.Pokemon;

public record PokemonResponse(
    Guid Id,
    string Name,
    int PokedexId,
    string Description,
    List<string> Type,
    List<string> Weaknesses,
    DateTime LastModifiedDateTime);
