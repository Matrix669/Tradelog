using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tradelog.Api.Data;
using Tradelog.Api.Dtos;

namespace Tradelog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TradesController(AppDbContext db): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TradeDto>>> GetAll(CancellationToken cancellationToken)
    {
        var trades = await db.Trades
            .OrderByDescending(t => t.ClosedAt)
            .Select(t => new TradeDto(
                Id: t.Id,
                Instrument: t.Instrument,
                InstrumentType: t.InstrumentType,
                Leverage: t.Leverage,
                Direction: t.Direction,
                Quantity: t.Quantity,
                EntryPrice: t.EntryPrice,
                ExitPrice: t.ExitPrice,
                StopLoss: t.StopLoss,
                NetPnl: t.NetPnl,
                RMultiple: t.StopLoss != null && t.StopLoss != t.EntryPrice
                    ? Math.Round(t.NetPnl / ((Math.Abs(t.EntryPrice - t.StopLoss.Value)) * t.Quantity), 2) 
                    : null,
                OpenedAt: t.OpenedAt,
                ClosedAt: t.ClosedAt,
                StrategyName: t.Strategy!.Name,
                Note: t.Note
            ))
            .ToListAsync(cancellationToken);
        return Ok(trades);
    }
}