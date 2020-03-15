using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpreadMagic.Core;
using SpreadMagic.Web.Api.Models;

namespace SpreadMagic.Web.Api.Controllers
{
    [ApiController]
    [Route("games")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService) => _gameService = gameService;

        [HttpGet]
        [Route("future")]
        public async Task<IActionResult> GetFutureGames()
        {
            var games = await _gameService.GetFutureGamesAsync();

            var gameModels = games.Select(game => new GameModel
            {
                Id = game.Id,
                AwayTeamId = game.AwayTeamId,
                HomeTeamId = game.HomeTeamId,
                DateAndTime = game.DateAndTime,
                Spread = game.Spread
            });

            return Ok(gameModels.ToArray());
        }
    }
}