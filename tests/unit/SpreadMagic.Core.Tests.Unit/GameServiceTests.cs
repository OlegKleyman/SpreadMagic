using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MockQueryable.NSubstitute;
using NSubstitute;
using SpreadMagic.Data;
using SpreadMagic.Data.Contexts;
using Xunit;
using GameEntity = SpreadMagic.Data.Entities.Game;
using GameDetailsEntity = SpreadMagic.Data.Contexts.GameDetails;

namespace SpreadMagic.Core.Tests.Unit
{
    public class GameServiceTests
    {
        [Fact]
        public async Task GetFutureGamesAsyncReturnsFutureGamesFromDataStore()
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
            var result = await service.GetFutureGamesAsync();

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

        [Fact]
        public async Task GetAllGameDetailsReturnsAllGamesFromDateBasedOnGameDateFromDataStore()
        {
            var gamesContext = new GameContext(Substitute.For<IConfigurer>());
            var dateProvider = Substitute.For<IDateProvider>();
            var detailsEntity = new[] { new GameDetailsEntity 
                { 
                    Id = 1,
                    HomeTeamId = 10,
                    AwayTeamId = 11,
                    DateAndTime = new DateTime(2020, 4, 11, 8, 0, 0),
                    Spread = 3.2m,
                    ModelPrediction = 5.5m,
                    HomeScore = 80,
                    VisitorScore = 89
                },
                new GameDetailsEntity
                {
                    Id = 2,
                    HomeTeamId = 10,
                    AwayTeamId = 11,
                    DateAndTime = new DateTime(2020, 5, 11, 8, 0, 0),
                    Spread = 3.2m,
                    ModelPrediction = 5.5m,
                    HomeScore = 80,
                    VisitorScore = 89
                },
                new GameDetailsEntity
                {
                    Id = 3,
                    HomeTeamId = 10,
                    AwayTeamId = 11,
                    DateAndTime = new DateTime(2020, 3, 11, 8, 0, 0),
                    Spread = 3.2m,
                    ModelPrediction = 5.5m,
                    HomeScore = 80,
                    VisitorScore = 89
                }
            };

            gamesContext.GameDetails = detailsEntity.AsQueryable().BuildMockDbSet();

            var service = GetService(gamesContext, dateProvider);
            var filter = new DetailsFilter();
            filter.StartDateAndTime = new DateTime(2020, 4, 11);
            var details = await service.GetAllGamesAsync(filter);
            details.Should().BeEquivalentTo(new
            {
                Id = 1,
                HomeTeamId = 10,
                AwayTeamId = 11,
                DateAndTime = new DateTime(2020, 4, 11, 8, 0, 0),
                Spread = 3.2m,
                ModelPrediction = 5.5m,
                HomeScore = 80,
                VisitorScore = 89
            },
            new
            {
                Id = 2,
                HomeTeamId = 10,
                AwayTeamId = 11,
                DateAndTime = new DateTime(2020, 5, 11, 8, 0, 0),
                Spread = 3.2m,
                ModelPrediction = 5.5m,
                HomeScore = 80,
                VisitorScore = 89
            });

        }

        private GameService GetService(GameContext gamesContext, IDateProvider dateProvider)
        {
            return new GameService(gamesContext, dateProvider);
        }
    }
}
