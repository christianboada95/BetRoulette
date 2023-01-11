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
    public async Task<IActionResult> Post(RouletteDto rouletteDto)
    {
        var roulette = new Roulette()
        {
            Id = Guid.NewGuid(),
            Name = rouletteDto.Name,
            Description = "Lorem impsum dolor sit ammet",
            State = true
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
            State = true
        }).ToArray();

        return Ok(values);
    }
}