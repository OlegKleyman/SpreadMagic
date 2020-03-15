using System;
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
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetAllGames([FromQuery]GameDetailFilterModel gameDetailFilterModel)
        {
            var details = await _gameService.GetAllGamesAsync(new DetailsFilter { StartDateAndTime = gameDetailFilterModel.StartDateAndTime });
            var gameDetailModels =
                details.Select(x => new GameDetailModel { Id = x.Id, 
                    AwayTeamId = x.AwayTeamId, 
                    HomeTeamId = x.HomeTeamId, 
                    ModelPrediction = x.ModelPrediction, 
                    Spread = x.Spread, 
                    DateAndTime = x.
                    DateAndTime, 
                    HomeScore = x.HomeScore, 
                    VisitorScore = x.VisitorScore });
            return Ok(gameDetailModels.ToArray());
        }
    }
}