using System;
using System.Collections.Generic;
using BetRoulette.Application.DataTransferObjects;
using BetRoulette.Application.Services;
using BetRoulette.Domain.Entities;
using BetRoulette.Domain.Exceptions;
using BetRoulette.Domain.Interfaces;

namespace BetRoulette.UnitTest
{
	public class RouletteServiceTests
	{
		private readonly RouletteService _service;

		private readonly Mock<IRepository<Roulette>> _repository = new();

		public RouletteServiceTests()
		{
			_service = new RouletteService(_repository.Object);
		}

		[Fact]
		public async Task Get_ShouldThrowNotFoundRouletteException_WhenNotRouletteInDatabase()
		{
			// Arrange
			_repository.Setup(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
					   .ReturnsAsync(() => null);

			// Act
			Task act = _service.Get(Guid.NewGuid().ToString());

            // Assert
            await Assert.ThrowsAsync<NotFoundRouletteException>(() => act);
        }

        [Fact]
        public async Task ListAll_ShouldThrowNotFoundRouletteException_WhenNotRoulettesInDatabase()
        {
			// Arrange
			var list = new List<Roulette>() { new Roulette("fortune") };
            _repository.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
					   .ReturnsAsync(() => null);

            // Act
            Task act = _service.ListAll();

            // Assert
            await Assert.ThrowsAsync<NotFoundRouletteException>(() => act);
        }

        [Fact]
        public async Task Open_ShouldThrowConflictOpenRouletteException_WhenRouletteIsAlreadyOpen()
        {
            // Arrange
            var roulette = new Roulette("fortune");
            roulette.Open();
            _repository.Setup(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(() => roulette);
            // Act
            Task act = _service.Open(Guid.NewGuid().ToString());

            // Assert
            await Assert.ThrowsAsync<ConflictOpenRouletteException>(() => act);
        }

        [Fact]
        public async Task Close_ShouldThrowConflictOpenRouletteException_WhenRouletteIsAlreadyClose()
        {
            // Arrange
            var roulette = new Roulette("fortune");
            roulette.Close();
            _repository.Setup(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(() => roulette);
            // Act
            Task act = _service.Close(Guid.NewGuid().ToString());

            // Assert
            await Assert.ThrowsAsync<ConflictOpenRouletteException>(() => act);
        }
    }
}

