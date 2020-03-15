﻿using System;
using System.Collections.Generic;
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
        public void GetFutureGamesReturnsAllFutureGamesFromService()
        {
            var gamesService = Substitute.For<IGamesService>();
            var games = new[]
            {
                new Game(1, 10, 11, new DateTime(2020, 4, 11, 8, 0, 0)),
                new Game(2, 12, 13, new DateTime(2020, 4, 11, 9, 0, 0))
            };

            gamesService.GetFutureGames().Returns(games);
            var controller = GetController(gamesService);

            var result = controller.GetFutureGames();

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

        private GamesController GetController(IGamesService gamesService) => new GamesController(gamesService);
    }
}