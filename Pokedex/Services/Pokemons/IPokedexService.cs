using ErrorOr;
using Pokedex.Models;

namespace Pokedex.Services.Pokemons;

public interface IPokedexService
{
    ErrorOr<Created> CreatePokemon(Pokemon pokemon);
    ErrorOr<Pokemon> GetPokemon(Guid id);
    ErrorOr<UpsertedPokemonResult> UpsertPokemon(Pokemon pokemon);
    ErrorOr<Deleted> DeletePokemon(Guid id);
    ErrorOr<List<Pokemon>> GetAllPokemon();
}