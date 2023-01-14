using System;
using BetRoulette.Application.DataTransferObjects;
using BetRoulette.Application.Services;
using BetRoulette.Domain.Entities;
using BetRoulette.Domain.Exceptions;
using BetRoulette.Domain.Interfaces;

namespace BetRoulette.UnitTest
{
	public class BetServiceTests
	{
        private readonly BetService _service;

        private readonly Mock<IRepository<Roulette>> _repository = new();

        public BetServiceTests()
        {
            _service = new BetService(_repository.Object);
        }

        [Fact]
        public async Task ToBet_ShouldThrowNotFoundRouletteException_WhenNotRouletteInDatabase()
        {
            // Arrange
            var bet = new BetDto(100, Guid.NewGuid().ToString());
            _repository.Setup(x => x.ListAsync(default)).ReturnsAsync(() => null);

            // Act
            Task act = _service.ToBet(bet);

            // Assert
            await Assert.ThrowsAsync<NotFoundRouletteException>(() => act);
        }

        [Fact]
        public async Task ToBet_ShouldThrowConflictOpenRouletteException_WhenNotRouletteIsOpen()
        {
            // Arrange
            var list = new List<Roulette>() { new Roulette("fortune") };
            var bet = new BetDto(100, Guid.NewGuid().ToString());
            _repository.Setup(x => x.ListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(list);

            // Act
            Task act = _service.ToBet(bet);

            // Assert
            await Assert.ThrowsAsync<ConflictOpenRouletteException>(() => act);
        }
    }
}

