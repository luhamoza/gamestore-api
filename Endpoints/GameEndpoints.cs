using GameStore.api.Entities;
namespace GameStore.api.Endpoints;

public static class GameEndpoints
{
   static List<Game>  _games = new List<Game>()
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

   const string GetGameEndPointName = "Get Game";
   public static RouteGroupBuilder MapGamesEndpoint(this IEndpointRouteBuilder routes)
   {
      var group = routes.MapGroup("/games").WithParameterValidation();

      group.MapGet("/", () => _games);
      group.MapGet("/{id}", (int id) =>
      {
         Game? game = _games.Find(game => game.Id == id);
         if (game == null)
         {
            return Results.NotFound();
         }
         return Results.Ok(game);
      }).WithName(GetGameEndPointName);
      group.MapPost("/", (Game game) =>
      {
         game.Id = _games.Max(game => game.Id) + 1;
         _games.Add(game);
         return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
      });
      group.MapPut("/{id}", (Game updateGame, int id) =>
      {
         Game? existingGame = _games.Find(game => game.Id == id);
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
      group.MapDelete("/{id}", (int id) =>
      {
         Game? game = _games.Find(game => game.Id == id);
         if (game is not null)
         {
            _games.Remove(game);
            return Results.NoContent();
         }
         return Results.NotFound();
      });
      return group;
   } 
}