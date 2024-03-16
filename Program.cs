using System.Resources;
using GameStore.api.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

List<Game> games = new List<Game>()
{
    new Game()
    {
        Id = 1,
        Name = "Outriders",
        Genre = "Action",
        Price = 19.90M,
        ReleaseDate = new DateTime(2014,2,24),
        ImageUri = "https://placehold.co/200"
    },
    new Game()
    {
        Id = 2,
        Name = "Need for Speed",
        Genre = "Racing",
        Price = 14.90M,
        ReleaseDate = new DateTime(2002,12,31),
        ImageUri = "https://placehold.co/200"
    }
};

const string getGameEndPointName = "Get Game";
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/games", () => games);
app.MapGet("/games/{id}", (int id) =>
{
   Game? game = games.Find(game => game.Id == id);
   if (game == null)
   {
       return Results.NotFound();
   }
   return Results.Ok(game);
}).WithName(getGameEndPointName);
app.MapPost("/games", (Game game) =>
{
    game.Id = games.Max(game => game.Id) + 1;
    games.Add(game);
    return Results.CreatedAtRoute(getGameEndPointName, new { id = game.Id }, game);
});
app.MapPut("/games/{id}", (Game updateGame, int id) =>
{
    Game? existingGame = games.Find(game => game.Id == id);
    if (existingGame is null)
    {
        return Results.NotFound();
    }
    existingGame.Name = updateGame.Name;
    existingGame.Genre = updateGame.Genre;
    existingGame.ReleaseDate = updateGame.ReleaseDate;
    existingGame.Price = updateGame.Price;
    existingGame.ImageUri = updateGame.ImageUri;
    return Results.NoContent();
});
app.MapDelete("/games/{id}", (int id) =>
{
    Game? game = games.Find(game => game.Id == id);
    if (game is not null)
    {
        games.Remove(game);
        return Results.NotFound();
    }

    return Results.NoContent();
});

app.Run();
