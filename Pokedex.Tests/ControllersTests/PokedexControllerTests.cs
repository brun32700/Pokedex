using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pokedex.Contracts.Pokemon;
using Pokedex.Controllers;
using Pokedex.Models;
using Pokedex.Services.Pokemons;
using Xunit;

namespace Pokedex.Tests.ControllersTests
{
    public class PokedexControllerTests
    {
        [Fact]
        public void CreatePokemon_ValidCreatePokemonRequestPassed_ReturnsCreatedAtActionResponse()
        {
            // Arrange
            var createPokemonRequest = new CreatePokemonRequest(
                Name: "Pikachu",
                PokedexId: 25,
                Description: "When it is angered, it immediately discharges the energy stored in the pouches in its cheeks.",
                Type: new List<string> { "Electric" },
                Weaknesses: new List<string> { "Ground" });
            var pokemon = Pokemon.From(createPokemonRequest).Value;

            var mockPokedexService = new Mock<IPokedexService>();
            mockPokedexService.Setup(x => x.CreatePokemon(pokemon)).Returns(Result.Created);
            var pokedexController = new PokedexController(mockPokedexService.Object);

            // Act
            var actualResponse = pokedexController.CreatePokemon(createPokemonRequest);

            // Assert
            Assert.IsType<CreatedAtActionResult>(actualResponse);
        }

        [Fact]
        public void GetPokemon_ValidPokemonIdPassed_ReturnsPokemonResponse()
        {
            // Arrange
            var createPokemonRequest = new CreatePokemonRequest(
                Name: "Pikachu",
                PokedexId: 25,
                Description: "When it is angered, it immediately discharges the energy stored in the pouches in its cheeks.",
                Type: new List<string> { "Electric" },
                Weaknesses: new List<string> { "Ground" });
            var pokemon = Pokemon.From(createPokemonRequest).Value;

            var mockPokedexService = new Mock<IPokedexService>();
            mockPokedexService.Setup(x => x.GetPokemon(pokemon.Id)).Returns(pokemon);
            var pokedexController = new PokedexController(mockPokedexService.Object);

            // Act
            var actualResponse = pokedexController.GetPokemon(pokemon.Id);

            // Assert
            Assert.IsType<OkObjectResult>(actualResponse);

            OkObjectResult okObjectResult = (OkObjectResult)actualResponse;
            PokemonResponse pokemonResponse = (PokemonResponse)okObjectResult.Value!;

            Assert.Equal(pokemon.Id, pokemonResponse.Id);
        }

        [Fact]
        public void UpdatePokemon_ExistingIdAndUpsertPokemonRequestPassed_ReturnsNoContentResponse()
        {
            // Arrange
            var upsertPokemonRequest = new UpsertPokemonRequest(
                Name: "Pikachu",
                PokedexId: 25,
                Description: "When it is angered, it immediately discharges the energy stored in the pouches in its cheeks.",
                Type: new List<string> { "Electric" },
                Weaknesses: new List<string> { "Ground" });
            var pokemon = Pokemon.From(Guid.NewGuid(), upsertPokemonRequest).Value;

            var mockPokedexService = new Mock<IPokedexService>();
            mockPokedexService.Setup(x => x.UpsertPokemon(pokemon)).Returns(new UpsertedPokemonResult(false));
            var pokedexController = new PokedexController(mockPokedexService.Object);

            // Act
            var actualResponse = pokedexController.UpdatePokemon(pokemon.Id, upsertPokemonRequest);

            // Assert
            Assert.IsType<NoContentResult>(actualResponse);
        }

        [Fact]
        public void UpdatePokemon_NewIdAndUpsertPokemonRequestPassed_ReturnsCreatedAtActionResponse()
        {
            // Arrange
            var upsertPokemonRequest = new UpsertPokemonRequest(
                Name: "Pikachu",
                PokedexId: 25,
                Description: "When it is angered, it immediately discharges the energy stored in the pouches in its cheeks.",
                Type: new List<string> { "Electric" },
                Weaknesses: new List<string> { "Ground" });
            var pokemon = Pokemon.From(Guid.NewGuid(), upsertPokemonRequest).Value;

            var mockPokedexService = new Mock<IPokedexService>();
            bool IsNewlyCreated = true;
            mockPokedexService.Setup(x => x.UpsertPokemon(pokemon)).Returns(new UpsertedPokemonResult(IsNewlyCreated));
            var pokedexController = new PokedexController(mockPokedexService.Object);

            // Act
            var actualResponse = pokedexController.UpdatePokemon(pokemon.Id, upsertPokemonRequest);

            // Assert
            Assert.IsType<CreatedAtActionResult>(actualResponse);
        }
    }
}