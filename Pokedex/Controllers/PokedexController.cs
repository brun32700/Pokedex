using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Contracts.Pokemon;
using Pokedex.Models;
using Pokedex.Services.Pokemons;

namespace Pokedex.Controllers;

public class PokedexController : ApiController
{
    private readonly IPokedexService _pokedexService;

    public PokedexController(IPokedexService pokedexService)
    {
        _pokedexService = pokedexService;
    }

    [HttpPost]
    public IActionResult CreatePokemon(CreatePokemonRequest request)
    {
        // Map request to internal Pokemon model
        ErrorOr<Pokemon> requestToPokemonResult = Pokemon.From(request);

        if (requestToPokemonResult.IsError)
        {
            return Problem(requestToPokemonResult.Errors);
        }

        // Save Pokemon to database
        Pokemon pokemon = requestToPokemonResult.Value;
        ErrorOr<Created> createPokemonResult = _pokedexService.CreatePokemon(pokemon);

        return createPokemonResult.Match(
            created => CreatedAsGetPokemon(pokemon),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetPokemon(Guid id)
    {
        ErrorOr<Pokemon> getPokemonResult = _pokedexService.GetPokemon(id);

        return getPokemonResult.Match(
            pokemon => Ok(MapPokemonToPokemonResponse(pokemon)),
            errors => Problem(errors));
    }

    [HttpGet]
    public IActionResult GetAllPokemon()
    {
        ErrorOr<List<Pokemon>> getAllPokemonResult = _pokedexService.GetAllPokemon();

        return getAllPokemonResult.Match(
            pokemons => Ok(new AllPokemonResponse(pokemons.ConvertAll(p => MapPokemonToPokemonResponse(p)))),
            errors => Problem(errors));
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdatePokemon(Guid id, UpsertPokemonRequest request)
    {
        // Map request to internal Pokemon model
        ErrorOr<Pokemon> requestToPokemonResult = Pokemon.From(id, request);

        if (requestToPokemonResult.IsError)
        {
            return Problem(requestToPokemonResult.Errors);
        }

        // Upsert Pokemon to database
        Pokemon pokemon = requestToPokemonResult.Value;
        ErrorOr<UpsertedPokemonResult> upsertedPokemonResult = _pokedexService.UpsertPokemon(pokemon);

        return upsertedPokemonResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAsGetPokemon(pokemon) : NoContent(),
            errors => Problem (errors));
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeletePokemon(Guid id)
    {
        ErrorOr<Deleted> deletedPokemonResult = _pokedexService.DeletePokemon(id);

        return deletedPokemonResult.Match(
            pokemon => NoContent(), 
            errors => Problem(errors));
    }

    private static PokemonResponse MapPokemonToPokemonResponse(Pokemon pokemon)
    {
        return new PokemonResponse(
            pokemon.Id, 
            pokemon.Name, 
            pokemon.PokedexId, 
            pokemon.Description, 
            pokemon.Type, 
            pokemon.Weaknesses, 
            pokemon.LastModifiedDateTime);
    }

    private CreatedAtActionResult CreatedAsGetPokemon(Pokemon pokemon)
    {
        return CreatedAtAction(
            actionName: nameof(GetPokemon),
            routeValues: new { id = pokemon.Id },
            value: MapPokemonToPokemonResponse(pokemon));
    }
}