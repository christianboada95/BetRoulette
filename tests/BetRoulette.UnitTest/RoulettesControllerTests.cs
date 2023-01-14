using BetRoulette.Api.Controllers;
using BetRoulette.Application.DataTransferObjects;
using BetRoulette.Application.DataTransferObjects.Requests;
using BetRoulette.Application.DataTransferObjects.Responses;
using BetRoulette.Application.Interfaces;
using BetRoulette.Domain.Entities;
using BetRoulette.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BetRoulette.UnitTest
{
    public class RoulettesControllerTests
    {
        private readonly RoulettesController _controller;

        private readonly Mock<ILogger<RoulettesController>> _loggerMock = new();
        private readonly Mock<IRouletteService> _rouletteService = new();

        public RoulettesControllerTests()
        {
            _controller = new RoulettesController(
                _loggerMock.Object, _rouletteService.Object);
        }

        [Fact]
        public async Task Post_ShouldReturnRoulette_WhenCreateNewRouletteAsync()
        {
            // Arrange
            var request = new CreateRouletteRequest()
            {
                RouletteName = "Bonne fortune"
            };
            var roulette = new Roulette(request.RouletteName);
            _rouletteService.Setup(x => x.Create(It.IsAny<string>()))
                            .ReturnsAsync(roulette);

            // Act
            var result = await _controller.Post(request);

            // Assert
            result.Should().NotBeNull();
            var objectResult = Assert.IsType<CreatedAtActionResult>(result);
            objectResult.Value.As<Response>().Message.Should().Be("Roulette created successfully.");
            objectResult.Value.As<Response>().Data.Should().NotBeNull();
            var dataResult = Assert.IsType<Roulette>(objectResult.Value.As<Response>().Data);
            dataResult.Id.Should().NotBeEmpty();
            dataResult.Name.Should().Be(request.RouletteName);
            dataResult.State.Should().Be(RouletteState.Close);
        }

        [Fact]
        public async Task Get_ShouldReturnRouletteList_WhenGettingFromDatabase()
        {
            // Arrange
            var values = new List<Roulette>()
            {
                new Roulette("ruleta 1"),
                new Roulette("ruleta 2")
            };
            _rouletteService.Setup(x => x.ListAll()).ReturnsAsync(values);

            // Act
            var act = await _controller.Get();

            // Assert
            act.Should().NotBeNull();
            var objectResult = Assert.IsType<OkObjectResult>(act.Result);
            objectResult.Value.As<RouletteListResponse>().Data.Should().NotBeNull();
            var dataResult = Assert.IsType<List<RouletteDto>>(objectResult.Value.As<RouletteListResponse>().Data);
            foreach(var x in dataResult) {
                x.Id.Should().NotBeEmpty();
                x.State.Should().BeDefined();
            };
        }

        [Fact]
        public async Task OpenRoulette_ShouldReturnSuccessfullMessage_WhenOpenRoulette()
        {
            // Act
            var result = await _controller.OpenRoulette(Guid.NewGuid());

            // Assert
            result.Should().NotBeNull();
            var objectResult = Assert.IsType<OkObjectResult>(result);
            objectResult.Value.As<Response>().Message.Should().Be("Roulette open successfully.");
        }

        [Fact]
        public async Task CloseRoulette_ShouldRouletteWithBetsResults_WhencloseRoulette()
        {
            // Arrange
            var roulette = new Roulette("Fortune");
            roulette.Bets.Add(new Bet(10000, Guid.NewGuid().ToString()));
            roulette.Bets.Add(new Bet(1000, Guid.NewGuid().ToString()));
            _rouletteService.Setup(x => x.Close(It.IsAny<string>()))
                            .ReturnsAsync(roulette);

            // Act
            var act = await _controller.CloseRoulette(Guid.NewGuid());

            // Assert
            act.Should().NotBeNull();
            var objectResult = Assert.IsType<OkObjectResult>(act.Result);
            objectResult.Value.As<RouletteBetsResponse>().Message.Should().Be("Roulette close successfully.");
            objectResult.Value.As<RouletteBetsResponse>().Data.Should().NotBeNull();
            var dataResult = Assert.IsType<RouletteDto>(objectResult.Value.As<RouletteBetsResponse>().Data);
            dataResult.Id.Should().NotBeEmpty();
            dataResult.Bets.ForEach(x => {
                x.User.Should().NotBeEmpty();
                x.State.Should().BeDefined();
            });
        }

    }
}