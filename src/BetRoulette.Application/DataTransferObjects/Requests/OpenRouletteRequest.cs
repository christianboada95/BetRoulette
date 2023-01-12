using System.ComponentModel.DataAnnotations;

namespace BetRoulette.Application.DataTransferObjects.Requests;

public class OpenRouletteRequest
{
    [Required]
    public string RouletteId { get; init; }
}
