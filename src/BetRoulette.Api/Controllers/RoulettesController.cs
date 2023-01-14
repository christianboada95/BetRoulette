using BetRoulette.Api.Filters;
using BetRoulette.Application.DataTransferObjects.Requests;
using BetRoulette.Application.DataTransferObjects.Responses;
using BetRoulette.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BetRoulette.Api.Controllers;

[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(ExceptionFilter))]
public class RoulettesController : ControllerBase
{
    private readonly ILogger<RoulettesController> _logger;
    private readonly IRouletteService _rouletteService;

    public RoulettesController(
        ILogger<RoulettesController> logger,
        IRouletteService rouletteService)
    {
        _logger = logger;
        _rouletteService = rouletteService;
    }

    [HttpPost(Name = "CreateRoulette")]
    public async Task<IActionResult> Post([FromBody] CreateRouletteRequest request)
    {
        var roulette = await _rouletteService.Create(request.RouletteName);
        var actionName = nameof(GetRoulette);
        var routeValues = new { rouletteId = roulette.Id };
        return CreatedAtAction(actionName, routeValues,
            RouletteResponse.Success(roulette, "Roulette created successfully."));
    }

    [HttpGet(Name = "GetRoulettes")]
    public async Task<ActionResult<RouletteListResponse>> Get()
    {
        var values = await _rouletteService.ListAll().ConfigureAwait(false);
        var response = new RouletteListResponse(values
                .Select(r => new RouletteDto(r.Id.ToString(), r.Name, r.State))
                .ToList(), "Roulette List");

        return Ok(response);
    }

    [HttpGet("{rouletteId:guid}")]
    public async Task<ActionResult> GetRoulette([FromRoute] Guid rouletteId)
    {
        var roulette = await _rouletteService.Get(rouletteId.ToString()).ConfigureAwait(false);
        RouletteDto rouletteDto = new(roulette.Id.ToString(), roulette.Name, roulette.State)
        {
            Result = roulette.Result,
            Bets = roulette.Bets!.Select(b => new BetDto(b.Amount, b.User)
            {
                Value = b.Value,
                Color = b.Color,
                State = b.State,
                Profits = (int)b.Profits
            }).ToList()
        };
        return Ok(RouletteResponse.Success(rouletteDto));
    }

    [HttpPost("{rouletteId:guid}/Open")]
    public async Task<IActionResult> OpenRoulette([FromRoute] Guid rouletteId)
    {
        _logger.LogInformation(rouletteId.ToString());
        await _rouletteService.Open(rouletteId.ToString()).ConfigureAwait(false);
        return Ok(RouletteResponse.Success("Roulette open successfully."));
    }
    [HttpPost("{rouletteId:guid}/Close")]
    public async Task<ActionResult<RouletteBetsResponse>> CloseRoulette([FromRoute] Guid rouletteId)
    {
        _logger.LogInformation(rouletteId.ToString());
        var roulette = await _rouletteService.Close(rouletteId.ToString()).ConfigureAwait(false);
        RouletteDto rouletteDto = new(roulette.Id.ToString(), roulette.Name, roulette.State)
        {
            Result = roulette.Result,
            Bets = roulette.Bets!.Select(b => new BetDto(b.Amount, b.User)
            {
                Value = b.Value,
                Color = b.Color,
                State = b.State,
                Profits = (int)b.Profits
            }).ToList()
        };
        var response = new RouletteBetsResponse(rouletteDto, "Roulette close successfully.");

        return Ok(response);
    }
}