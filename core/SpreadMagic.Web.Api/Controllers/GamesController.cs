using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SpreadMagic.Core;
using SpreadMagic.Web.Api.Models;

namespace SpreadMagic.Web.Api.Controllers
{
    [ApiController]
    [Route("games")]
    public class GamesController : ControllerBase
    {
        private readonly IGamesService _gamesService;

        public GamesController(IGamesService gamesService) => _gamesService = gamesService;

        [HttpGet]
        [Route("future")]
        public IActionResult GetFutureGames()
        {
            var games = _gamesService.GetFutureGames();

            var gameModels = games.Select(game => new GameModel
            {
                Id = game.Id,
                AwayTeamId = game.AwayTeamId,
                HomeTeamId = game.HomeTeamId,
                DateAndTime = game.DateAndTime
            });

            return Ok(gameModels.ToArray());
        }
    }
}