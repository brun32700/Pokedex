using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Pokedex.Models;
using Pokedex.Persistence;
using Pokedex.ServiceErrors;

namespace Pokedex.Services.Pokemons;

public class PokedexService : IPokedexService
{
    private readonly PokedexDbContext _dbContext;

    public PokedexService(PokedexDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ErrorOr<Created> CreatePokemon(Pokemon pokemon)
    {
        var pokemonExists = _dbContext.Pokemons.Any(p => 
                p.Name == pokemon.Name && 
                p.PokedexId == pokemon.PokedexId &&
                p.Description == pokemon.Description &&
                p.Type == pokemon.Type &&
                p.Weaknesses == pokemon.Weaknesses);

        if (pokemonExists)
        {
            return Errors.Pokemon.Exists;
        }

        _dbContext.Pokemons.Add(pokemon);
        _dbContext.SaveChanges();

        return Result.Created;
    }

    public ErrorOr<Deleted> DeletePokemon(Guid id)
    {
        var pokemon = _dbContext.Pokemons.Find(id);

        if (pokemon is null)
        {
            return Errors.Pokemon.NotFound;
        }

        _dbContext.Pokemons.Remove(pokemon);
        _dbContext.SaveChanges();

        return Result.Deleted;
    }

    public ErrorOr<Pokemon> GetPokemon(Guid id)
    {
        if (_dbContext.Pokemons.Find(id) is Pokemon pokemon)
        {
            return pokemon;
        }
        
        return Errors.Pokemon.NotFound;
    }

    public ErrorOr<List<Pokemon>> GetAllPokemon()
    {
        return _dbContext.Pokemons.ToList();
    }

    public ErrorOr<UpsertedPokemonResult> UpsertPokemon(Pokemon pokemon)
    {
        var isNewlyCreated = !_dbContext.Pokemons.Any(p => p.Id == pokemon.Id);

        if (isNewlyCreated)
        {
            _dbContext.Pokemons.Add(pokemon);
        }
        else
        {
            _dbContext.Pokemons.Update(pokemon);
        }

        _dbContext.SaveChanges();
        
        return new UpsertedPokemonResult(isNewlyCreated);
    }
}
