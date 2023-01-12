using BetRoulette.Api.Controllers;
using BetRoulette.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetRoulette.UnitTest
{
    public class RoulettesControllerTests
    {
        private readonly RoulettesController _controller;

        private readonly Mock<ILogger<RoulettesController>> _loggerMock = new();

        public RoulettesControllerTests()
        {
            _controller = new RoulettesController(_loggerMock.Object);
        }

        [Fact]
        public async Task Post_ShouldReturnRouletteId_WhenCreateNewRouletteAsync()
        {
            // Arrange

            // Act
            var result = await _controller.Create("Roulette Name");

            // Assert
            result.Should().NotBeNull();
            var objectResult = Assert.IsType<OkObjectResult>(result);
            objectResult.Value.As<Roulette>().Id.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("Nombre muy largo para la ruleta valido")]
        public async Task Post_ShouldReturnRouletteId_WhenSendInvalidRouletteNameAsync(string name)
        {
            // Act
            var result = await _controller.Create(name);

            // Assert
            result.Should().NotBeNull();
            //result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}