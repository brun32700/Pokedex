using Microsoft.EntityFrameworkCore;
using Pokedex.Persistence;
using Pokedex.Services.Pokemons;

// To start the web api, run the following command in the terminal:
// dotnet run --project .\Pokedex\ --urls "http://0.0.0.0:5007/"

// To start both the front end react and back end asp web abpi at the same time:
// https://learn.microsoft.com/en-us/aspnet/core/client-side/spa/intro?view=aspnetcore-7.0

// Authentication and Authorization in ASP.NET Core Web API
// https://learn.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-7.0
// https://learn.microsoft.com/en-us/aspnet/web-api/overview/security/authentication-and-authorization-in-aspnet-web-api
// https://openid.net/
// https://www.keycloak.org/

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<IPokedexService, PokedexService>();
    builder.Services.AddDbContext<PokedexDbContext>(options => options.UseSqlite($"Data Source=pokedex.db"));
    builder.Services.AddCors(options => 
    {
        options.AddDefaultPolicy(
            policy =>
            {
                policy.WithOrigins("http://localhost:3000", "http://192.168.1.105:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }
        );
    });
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseExceptionHandler("/error");
    app.UseCors();
    app.MapControllers();
    app.Run();
}