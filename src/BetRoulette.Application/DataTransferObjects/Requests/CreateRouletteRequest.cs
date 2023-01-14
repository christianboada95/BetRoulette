using System.ComponentModel.DataAnnotations;

namespace BetRoulette.Application.DataTransferObjects.Requests;

public class CreateRouletteRequest
{
    [Required]
    [StringLength(50)]
    public string RouletteName { get; init; }
}
