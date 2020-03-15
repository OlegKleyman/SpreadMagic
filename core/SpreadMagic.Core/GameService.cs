using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpreadMagic.Data.Contexts;

namespace SpreadMagic.Core
{
    public class GameService : IGameService
    {
        private readonly GameContext _gamesContext;
        private readonly IDateProvider _dateProvider;

        public GameService(GameContext gamesContext, IDateProvider dateProvider)
        {
            _gamesContext = gamesContext;
            _dateProvider = dateProvider;
        }

        public async Task<GameDetails[]> GetAllGamesAsync(DetailsFilter detailsFilter)
        {
            var games = await _gamesContext.GameDetails.Where(gd => gd.DateAndTime >= detailsFilter.StartDateAndTime)
                                     .Select(gd =>
                                         new GameDetails(gd.Id, gd.HomeTeamId, gd.AwayTeamId, gd.DateAndTime, gd.Spread, gd.ModelPrediction, gd.HomeScore, gd.VisitorScore))
                                     .ToArrayAsync();
            return games;
        }

        public async Task<Game[]> GetFutureGamesAsync()
        {
            var games = await _gamesContext.Games.Where(game => game.DateAndTime > _dateProvider.UtcNow)
                                     .Select(game =>
                                         new Game(game.Id, game.HomeTeamId, game.AwayTeamId, game.DateAndTime, game.Spread))
                                     .ToArrayAsync();
            return games;
        }
    }
}