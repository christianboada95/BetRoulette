using BetRoulette.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BetRoulette.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RoulettesController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<RoulettesController> _logger;

    public RoulettesController(ILogger<RoulettesController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "CreateRoulette")]
    public async Task<IActionResult> Create([FromBody] string rouletteName)
    {
        var roulette = new Roulette()
        {
            Id = Guid.NewGuid(),
            Name = rouletteName,
            State = RouletteStates.Open
        };
        return Ok(roulette);
        //return CreatedAtAction("CreateRoulette", rullete);
    }

    [HttpGet(Name = "GetRoulettes")]
    public async Task<IActionResult> Get()
    {
        var values = Enumerable.Range(1, 5).Select(index => new Roulette()
        {
            Id = Guid.NewGuid(),
            Name = Summaries[Random.Shared.Next(Summaries.Length)],
            State = RouletteStates.Open
        }).ToArray();

        return Ok(values);
    }

    [HttpPatch(Name = "OpenRoulette")]
    public async Task<IActionResult> Open([FromBody] string rouletteId)
    {
        _logger.LogInformation(rouletteId);
        return Ok("Succesfuly Open Roulette");
    }
}