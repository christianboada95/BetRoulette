namespace BetRoulette.Domain.Entities;

public class Roulette
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool State { get; set; }
}
