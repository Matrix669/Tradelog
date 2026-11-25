namespace Tradelog.Api.Models;

public class Strategy
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Trade> Trades { get; set; } = [];
}