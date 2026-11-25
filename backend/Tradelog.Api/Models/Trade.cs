namespace Tradelog.Api.Models;

public class Trade
{
    public int Id { get; set; }
    public required string Instrument { get; set; }
    public InstrumentType InstrumentType { get; set; }
    public int? Leverage { get; set; }
    public TradeDirection Direction { get; set; }
    public decimal Quantity { get; set; }
    public decimal EntryPrice { get; set; }
    public decimal ExitPrice { get; set; }
    public decimal? StopLoss { get; set; }
    public decimal NetPnl { get; set; }
    public decimal Fee { get; set; }
    public DateTime OpenedAt  { get; set; }
    public DateTime ClosedAt { get; set; }
    public string? Note { get; set; }
    public Strategy? Strategy { get; set; }
    public int? StrategyId { get; set; }
}