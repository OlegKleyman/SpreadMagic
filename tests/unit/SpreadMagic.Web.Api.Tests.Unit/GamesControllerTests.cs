using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SpreadMagic.Core;
using SpreadMagic.Web.Api.Controllers;
using SpreadMagic.Web.Api.Models;
using Xunit;

namespace SpreadMagic.Web.Api.Tests.Unit
{
    public class GamesControllerTests
    {
        [Fact]
        public async Task GetFutureGamesReturnsAllFutureGamesFromService()
        {
            var gamesService = Substitute.For<IGameService>();
            var games = new[]
            {
                new Game(1, 10, 11, new DateTime(2020, 4, 11, 8, 0, 0),-1.1m),
                new Game(2, 12, 13, new DateTime(2020, 4, 11, 9, 0, 0), 3.2m)
            };

            gamesService.GetFutureGamesAsync().Returns(games);
            var controller = GetController(gamesService);

            var result = await controller.GetFutureGames();

            result.Should()
                  .BeOfType<OkObjectResult>()
                  .Which.Value.Should()
                  .BeOfType<GameModel[]>()
                  .Which.Should()
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
        public async Task GetAllGameDetailsReturnsAllGamesFromDateBasedOnGameDateFromService()
        {
            var gamesService = Substitute.For<IGameService>();
            
            var gameDetails = new[]
            { 
                new GameDetails(2, 12, 13, new DateTime(2020, 4, 11, 9, 0, 0), 3.2m, 3.5m, 80, 85),
                new GameDetails(3, 12, 13, new DateTime(2020, 5, 11, 9, 0, 0), 3.2m, 7.5m, 89, 85)
            };

            gamesService.GetAllGamesAsync(Arg.Is<DetailsFilter>(x => x.StartDateAndTime == new DateTime(2020, 4, 11))).Returns(gameDetails);
            var controller = GetController(gamesService);
            var filter = new GameDetailFilterModel();
            filter.StartDateAndTime = new DateTime(2020, 4, 11);
            var result = await controller.GetAllGames(filter);

            result.Should()
                  .BeOfType<OkObjectResult>()
                  .Which.Value.Should()
                  .BeOfType<GameDetailModel[]>()
                  .Which.Should()
                  .BeEquivalentTo(new
                  {
                      Id = 2,
                      HomeTeamId = 12,
                      AwayTeamId = 13,
                      DateAndTime = new DateTime(2020, 4, 11, 9, 0, 0),
                      Spread = 3.2m,
                      ModelPrediction = 3.5m,
                      HomeScore = 80,
                      VisitorScore = 85
                  },
                      new
                      {
                          Id = 3,
                          HomeTeamId = 12,
                          AwayTeamId = 13,
                          DateAndTime = new DateTime(2020, 5, 11, 9, 0, 0),
                          Spread = 3.2m,
                          ModelPrediction = 7.5m,
                          HomeScore = 89,
                          VisitorScore = 85
                      }) ;
            ;
        }

        private GamesController GetController(IGameService gameService) => new GamesController(gameService);
    }
}
