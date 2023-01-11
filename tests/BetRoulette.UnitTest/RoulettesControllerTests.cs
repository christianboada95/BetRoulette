using BetRoulette.Api.Controllers;
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
        public void Test1()
        {

        }
    }
}