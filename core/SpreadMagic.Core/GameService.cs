using System;
using System.Linq;
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

        public Game[] GetFutureGames()
        {
            var games = _gamesContext.Games.Where(game => game.DateAndTime > _dateProvider.UtcNow)
                                     .Select(game =>
                                         new Game(game.Id, game.HomeTeamId, game.AwayTeamId, game.DateAndTime))
                                     .ToArray();
            return games;
        }
    }
}