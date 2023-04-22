using ErrorOr;

namespace Pokedex.ServiceErrors;

public static class Errors
{
    public static class Pokemon
    {
        public static Error NotFound => Error.NotFound(
            code: "Pokemon.NotFound",
            description: "The pokemon was not found");

        public static Error Exists => Error.Conflict(
            code: "Pokemon.Exists",
            description: "The pokemon already exists.");

        public static Error InvalidName => Error.Validation(
            code: "Pokemon.InvalidName",
            description: $"The pokemon name must be between { Models.Pokemon.NameMinimumLength } and { Models.Pokemon.NameMaximumLength } characters long.");

        public static Error InvalidPokedexId => Error.Validation(
            code: "Pokemon.InvalidPokedexId",
            description: $"The pokemon pokedex id must be between { Models.Pokemon.PokedexIdMinimumValue } and { Models.Pokemon.PokedexIdMaximumValue }.");

        public static Error InvalidDescription => Error.Validation(
            code: "Pokemon.InvalidDescription",
            description: $"The pokemon description must be between { Models.Pokemon.DescriptionMinimumLength } and { Models.Pokemon.DescriptionMaximumLength } characters long.");

        public static Error InvalidType => Error.Validation(
            code: "Pokemon.InvalidType",
            description: $"The pokemon must have at least { Models.Pokemon.TypeMinimumLength } type(s).");

        public static Error InvalidWeaknesses => Error.Validation(
            code: "Pokemon.InvalidWeaknesses",
            description: $"The pokemon must have at least { Models.Pokemon.WeaknessesMinimumLength } weakness(es).");
    }
}