using BetRoulette.Domain.Enums;
using FluentValidation;

namespace BetRoulette.Application.DataTransferObjects.Requests;

public class CreateBetRequest
{
    //[Range(0, 36, ErrorMessage = "Roulete just has 0 to 36 numbers to bet on")]
    public short? Value { get; set; }
    //[Enum(typeof(Color), ErrorMessage = "You can just make a bet between Back and Red")]
    public Color? Color { get; set; } = default;
    //[Required]
    //[Range(1, 10000, ErrorMessage = "You can just make a bet from 1 to 10000 bucks")]
    public int Amount { get; set; }
}


public class CreateBetValidator : AbstractValidator<CreateBetRequest>
{
    public CreateBetValidator()
    {
        RuleFor(x => x.Value).GreaterThanOrEqualTo((short)0).LessThanOrEqualTo((short)36)
            .WithMessage("Roulete just has 0 to 36 numbers to bet on");
        RuleFor(x => x.Amount).NotNull().GreaterThan(0).LessThanOrEqualTo(10000)
            .WithMessage("You can just make a bet from 1 to 10000 bucks");

        RuleFor(x => x.Value)//.Null().When(x => x.Color is not null)
                             .NotNull().When(x => x.Color is null)
                             .WithMessage("Need to bet on a Number or Color");
        RuleFor(x => x.Color).Null().When(x => x.Value is not null)
                             //.NotEmpty().When(m => m.Value is null)
                             .WithMessage("Need to bet just on a Number or Color");
    }
}