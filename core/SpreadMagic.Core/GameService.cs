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