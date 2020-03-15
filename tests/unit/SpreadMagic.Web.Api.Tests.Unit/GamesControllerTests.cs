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

        private GamesController GetController(IGameService gameService) => new GamesController(gameService);
    }
}
