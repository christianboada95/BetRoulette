using Microsoft.AspNetCore.Mvc;

namespace BetRoulette.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BetsController : ControllerBase
{
    private readonly ILogger<BetsController> _logger;

    public BetsController(ILogger<BetsController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "CreateBet")]
    public async Task<IActionResult> Create(BetDto betDto)
    {
        var bet = new Bet();
        return Ok(bet);
        //return CreatedAtAction("CreateRoulette", rullete);
    }

}
