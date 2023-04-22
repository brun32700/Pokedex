namespace Pokedex.Contracts.Pokemon;

public record UpsertPokemonRequest(
    string Name,
    int PokedexId,
    string Description,
    List<string> Type,
    List<string> Weaknesses);