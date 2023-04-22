using System.ComponentModel.DataAnnotations;

namespace Pokedex.Contracts.Pokemon;

public record CreatePokemonRequest(
    string Name,
    int PokedexId,
    string Description,
    List<string> Type,
    List<string> Weaknesses);