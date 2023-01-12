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
        return CreatedAtAction(actionName, routeValues, roulette);
    }

    [HttpGet(Name = "GetRoulettes")]
    public async Task<ActionResult<RouletteListResponse>> Get()
    {
        //var values = Enumerable.Range(1, 5).Select(index => new Roulette()
        //{
        //    Id = Guid.NewGuid(),
        //    Name = Summaries[Random.Shared.Next(Summaries.Length)],
        //    State = RouletteStates.Open
        //}).ToArray();

        var values = await _rouletteService.ListAll();
        var response = new RouletteListResponse
        {
            Roulettes = values
                .Select(r => new RouletteDto(r.Id.ToString(), r.Name, r.State))
                .ToList(),
        };

        return Ok(response);
    }

    [HttpGet("{rouletteId:guid}")]
    public async Task<ActionResult> GetRoulette([FromRoute] Guid rouletteId)
    {
        //var values = Enumerable.Range(1, 5).Select(index => new Roulette()
        //{
        //    Id = Guid.NewGuid(),
        //    Name = Summaries[Random.Shared.Next(Summaries.Length)],
        //    State = RouletteStates.Open
        //}).ToArray();

        return Ok(rouletteId);
    }

    [HttpPatch(Name = "OpenRoulette")]
    public async Task<IActionResult> Patch([FromBody] OpenRouletteRequest request)
    {
        _logger.LogInformation(request.RouletteId);
        return Ok("Succesfuly Open Roulette");
    }
}