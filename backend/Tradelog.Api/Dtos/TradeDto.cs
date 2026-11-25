using Tradelog.Api.Models;

namespace Tradelog.Api.Dtos;

public record TradeDto(
    int Id, 
    string Instrument, 
    InstrumentType InstrumentType, 
    int? Leverage, 
    TradeDirection Direction, 
    decimal Quantity, 
    decimal EntryPrice, 
    decimal ExitPrice, 
    decimal? StopLoss, 
    decimal NetPnl, 
    decimal? RMultiple, 
    DateTime OpenedAt, 
    DateTime ClosedAt, 
    string? StrategyName, 
    string? Note);