using Microsoft.EntityFrameworkCore;
using Pokedex.Persistence;
using Pokedex.Services.Pokemons;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<IPokedexService, PokedexService>();
    builder.Services.AddDbContext<PokedexDbContext>(options => options.UseSqlite($"Data Source=pokedex.db"));
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseExceptionHandler("/error");
    app.MapControllers();
    app.Run();
}