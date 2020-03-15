using System;
using System.Linq;
using FluentAssertions;
using MockQueryable.NSubstitute;
using NSubstitute;
using SpreadMagic.Data;
using SpreadMagic.Data.Contexts;
using Xunit;
using GameEntity = SpreadMagic.Data.Entities.Game;

namespace SpreadMagic.Core.Tests.Unit
{
    public class GameServiceTests
    {
        [Fact]
        public void GetFutureGamesReturnsFutureGamesFromDataStore()
        {
            var gamesContext = new GameContext(Substitute.For<IConfigurer>());
            var dateProvider = Substitute.For<IDateProvider>();

            var games = new[]
            {
                new GameEntity
                {
                    Id = 1,
                    HomeTeamId = 10,
                    AwayTeamId = 11,
                    DateAndTime = new DateTime(2020, 4, 11, 8, 0, 0)
                },
                new GameEntity
                {
                    Id = 2,
                    HomeTeamId = 12,
                    AwayTeamId = 13,
                    DateAndTime = new DateTime(2020, 4, 11, 9, 0, 0)
                },

                new GameEntity
                {
                    Id = 2,
                    HomeTeamId = 12,
                    AwayTeamId = 13,
                    DateAndTime = DateTime.MinValue
                }
            };

            gamesContext.Games = games.AsQueryable().BuildMockDbSet();

            dateProvider.UtcNow.Returns(DateTime.MinValue);

            var service = GetService(gamesContext, dateProvider);
            var result = service.GetFutureGames();

            result.Should()
                  .BeEquivalentTo(new
                      {
                          Id = 1,
                          HomeTeamId = 10,
                          AwayTeamId = 11,
                          DateAndTime = new DateTime(2020, 4, 11, 8, 0, 0)
                      },
                      new
                      {
                          Id = 2,
                          HomeTeamId = 12,
                          AwayTeamId = 13,
                          DateAndTime = new DateTime(2020, 4, 11, 9, 0, 0)
                      });
        }

        private GameService GetService(GameContext gamesContext, IDateProvider dateProvider)
        {
            return new GameService(gamesContext, dateProvider);
        }
    }
}
