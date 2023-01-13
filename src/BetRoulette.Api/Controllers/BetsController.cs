using BetRoulette.Api.Filters;
using BetRoulette.Application.DataTransferObjects.Requests;
using BetRoulette.Application.DataTransferObjects.Responses;
using BetRoulette.Application.Interfaces;
using BetRoulette.Domain.Enums;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BetRoulette.Api.Controllers;

[Route("[controller]")]
[ApiController]
[TypeFilter(typeof(ExceptionFilter))]
public class BetsController : ControllerBase
{
    private readonly ILogger<BetsController> _logger;
    private readonly IBetService _betService;

    public BetsController(
        ILogger<BetsController> logger,
        IBetService betService)
    {
        _logger = logger;
        _betService = betService;
    }

    [HttpPost(Name = "CreateBet")]
    public async Task<IActionResult> Post(CreateBetRequest request, [FromHeader] string userId)
    {
        _logger.LogInformation($"Usuario: {userId}");

        var validator = new CreateBetValidator();
        // Execute the validator.
        ValidationResult results = validator.Validate(request);

        // Inspect any validation failures.
        if (!results.IsValid)
        {
            List<ValidationFailure> failures = results.Errors;
            return BadRequest(failures.Select(x =>
                ErrorResponse.Failure(AppStatusCode.ModelValidationError, x.ErrorMessage)));
        }

        var bet = new BetDto()
        {
            Amount = request.Amount,
            Color = request.Color,
            Value = request.Value,
            User = userId
        };
        await _betService.ToBet(bet).ConfigureAwait(false);
        return Accepted(new { message = "bet accepted successfully." });
    }

}
