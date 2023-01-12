using System.ComponentModel.DataAnnotations;

namespace BetRoulette.Application.DataTransferObjects.Requests;

public class CreateRouletteRequest
{
    [Required]
    public string RouletteName { get; init; }
}
