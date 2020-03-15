using System;

namespace SpreadMagic.Core
{
    public class GamesService : IGamesService
    {
        public Game[] GetFutureGames()
        {
            return new[]
            {
                new Game(1, 10, 11, new DateTime(2020, 4, 11, 8, 0, 0)),
                new Game(2, 12, 13, new DateTime(2020, 4, 11, 9, 0, 0))
            };
        }
    }
}