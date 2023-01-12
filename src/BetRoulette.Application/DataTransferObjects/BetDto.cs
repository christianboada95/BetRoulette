using BetRoulette.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BetRoulette.Application.DataTransferObjects;
public class BetDto
{
    public short Value { get; set; }
    public BetColors Color { get; set; }
    [Required]
    public int Amount { get; set; }
}
