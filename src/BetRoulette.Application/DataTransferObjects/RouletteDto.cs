using System.ComponentModel.DataAnnotations;

namespace BetRoulette.Application.DataTransferObjects;
public record RouletteDto
{
    [Required]
    public string Name { get; init; }
}
