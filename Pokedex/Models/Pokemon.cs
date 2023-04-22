using ErrorOr;
using Pokedex.Contracts.Pokemon;
using Pokedex.ServiceErrors;

namespace Pokedex.Models;

public class Pokemon
{
    public const int NameMinimumLength = 3;
    public const int NameMaximumLength = 15;
    public const int PokedexIdMinimumValue = 1;
    public const int PokedexIdMaximumValue = 1008;
    public const int DescriptionMinimumLength = 50;
    public const int DescriptionMaximumLength = 150;
    public const int TypeMinimumLength = 1;
    public const int WeaknessesMinimumLength = 1;

    public Guid Id { get; private set; }
    public string Name { get; private set; } = "";
    public int PokedexId { get; private set; }
    public string Description { get; private set; } = "";
    public List<string> Type { get; private set; } = new List<string>();
    public List<string> Weaknesses { get; private set; } = new List<string>();
    public DateTime LastModifiedDateTime { get; private set; }

    // EF Core will use this constructor when creating a new Pokemon object
    // It can be private because EF Core will use reflection to create a new instance
    private Pokemon() { }

    private Pokemon(
        Guid id, 
        string name, 
        int pokedexId, 
        string description, 
        List<string> type, 
        List<string> weaknesses, 
        DateTime lastModifiedDateTime)
    {
        Id = id;
        Name = name;
        PokedexId = pokedexId;
        Description = description;
        Type = type;
        Weaknesses = weaknesses;
        LastModifiedDateTime = lastModifiedDateTime;
    }

    public static ErrorOr<Pokemon> Create(
        string name, 
        int pokedexId, 
        string description,  
        List<string> type, 
        List<string> weaknesses,
        Guid? id = null)
    {
        List<Error> errors = new();

        // enforce validation rules
        if (name.Length < NameMinimumLength | name.Length > NameMaximumLength)
        {
            errors.Add(Errors.Pokemon.InvalidName);
        }

        if (pokedexId < PokedexIdMinimumValue | pokedexId > PokedexIdMaximumValue)
        {
            errors.Add(Errors.Pokemon.InvalidPokedexId);
        }

        if (description.Length < DescriptionMinimumLength | description.Length > DescriptionMaximumLength)
        {
            errors.Add(Errors.Pokemon.InvalidDescription);
        }

        if (type.Count < TypeMinimumLength)
        {
            errors.Add(Errors.Pokemon.InvalidType);
        }

        if (weaknesses.Count < WeaknessesMinimumLength)
        {
            errors.Add(Errors.Pokemon.InvalidWeaknesses);
        }

        if (errors.Count > 0)
        {
            return errors;
        }
        else
        {
            return new Pokemon(
                // ?? returns the left hand side if it is not null, otherwise it returns the right hand side
                id ?? Guid.NewGuid(),
                name,
                pokedexId,
                description,
                type,
                weaknesses,
                DateTime.UtcNow);
        }
    }

    public static ErrorOr<Pokemon> From(CreatePokemonRequest request)
    {
        return Create(
            request.Name, 
            request.PokedexId,
            request.Description,
            request.Type,
            request.Weaknesses);
    }

    public static ErrorOr<Pokemon> From(Guid id, UpsertPokemonRequest request)
    {
        return Create(
            request.Name, 
            request.PokedexId,
            request.Description,
            request.Type,
            request.Weaknesses,
            id);
    }
}
