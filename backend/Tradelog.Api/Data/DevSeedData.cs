using Microsoft.EntityFrameworkCore;
using Tradelog.Api.Models;

namespace Tradelog.Api.Data;

/// <summary>
/// Dane testowe dla środowiska Development. Wstawiane tylko do pustej bazy.
/// </summary>
public static class DevSeedData
{
    public static void Seed(DbContext context)
    {
        if (context.Set<Trade>().Any())
            return;

        context.Set<Trade>().AddRange(BuildTrades());
        context.SaveChanges();
    }

    public static async Task SeedAsync(DbContext context, CancellationToken cancellationToken)
    {
        if (await context.Set<Trade>().AnyAsync(cancellationToken))
            return;

        context.Set<Trade>().AddRange(BuildTrades());
        await context.SaveChangesAsync(cancellationToken);
    }

    private static List<Trade> BuildTrades()
    {
        var breakout = new Strategy { Name = "Wybicie z konsolidacji" };
        var resistance = new Strategy { Name = "Odbicie od oporu" };
        var pullback = new Strategy { Name = "Pullback do EMA" };
        var range = new Strategy { Name = "Gra w zakresie" };
        var trend = new Strategy { Name = "Podążanie za trendem" };

        // Daty liczone od dzisiaj, żeby dane zawsze wyglądały na świeże. .Date zachowuje Kind = Utc.
        var today = DateTime.UtcNow.Date;
        DateTime At(int daysAgo, int hour, int minute) =>
            today.AddDays(-daysAgo).AddHours(hour).AddMinutes(minute);

        return
        [
            Create("BTC-USDT-SWAP", InstrumentType.Perpetual, leverage: 10, TradeDirection.Long,
                quantity: 0.15m, entry: 66_863.5m, exit: 67_436.7m, fee: 6.39m, stopLoss: 66_420m,
                openedAt: At(1, 9, 14), duration: new TimeSpan(3, 12, 0), strategy: breakout,
                note: "Czekałem na zamknięcie świecy 1h nad oporem. Wejście zgodnie z planem."),

            Create("ETH-USDT-SWAP", InstrumentType.Perpetual, leverage: 5, TradeDirection.Short,
                quantity: 2.4m, entry: 2_612.45m, exit: 2_638.10m, fee: 1.71m, stopLoss: 2_638m,
                openedAt: At(2, 18, 2), duration: new TimeSpan(1, 38, 0), strategy: resistance,
                note: "Wszedłem bez potwierdzenia, zaraz po poprzedniej stracie. Klasyczne FOMO."),

            Create("SOL-USDT", InstrumentType.Spot, leverage: null, TradeDirection.Long,
                quantity: 40m, entry: 171.32m, exit: 176.05m, fee: 4.18m, stopLoss: 169.30m,
                openedAt: At(2, 10, 21), duration: new TimeSpan(4, 42, 0), strategy: pullback,
                note: "Ładne odbicie od EMA 21 na 1h. Częściowa realizacja za wcześnie."),

            Create("BTC-USDT-SWAP", InstrumentType.Perpetual, leverage: 10, TradeDirection.Short,
                quantity: 0.10m, entry: 67_410.0m, exit: 67_028.5m, fee: 2.03m, stopLoss: 67_810m,
                openedAt: At(3, 22, 47), duration: new TimeSpan(2, 28, 0), strategy: range,
                note: null),

            Create("DOGE-USDT-SWAP", InstrumentType.Perpetual, leverage: 5, TradeDirection.Long,
                quantity: 12_000m, entry: 0.19840m, exit: 0.19610m, fee: 1.14m, stopLoss: 0.19500m,
                openedAt: At(3, 14, 5), duration: new TimeSpan(0, 47, 0), strategy: null,
                note: null),

            Create("ETH-USDT", InstrumentType.Spot, leverage: null, TradeDirection.Long,
                quantity: 1.5m, entry: 2_548.90m, exit: 2_601.30m, fee: 1.77m, stopLoss: 2_510m,
                openedAt: At(4, 8, 30), duration: new TimeSpan(12, 40, 0), strategy: trend,
                note: "Trzymałem do końca sesji US, bez przesuwania SL."),

            Create("BTC-USDT-SWAP", InstrumentType.Perpetual, leverage: 10, TradeDirection.Long,
                quantity: 0.20m, entry: 66_102.7m, exit: 65_880.0m, fee: 1.67m, stopLoss: 65_880m,
                openedAt: At(5, 16, 12), duration: new TimeSpan(0, 46, 0), strategy: breakout,
                note: "Fałszywe wybicie tuż przed danymi z USA. Nie wchodzić przed newsami."),

            Create("XRP-USDT-SWAP", InstrumentType.Perpetual, leverage: 5, TradeDirection.Short,
                quantity: 3_000m, entry: 0.5412m, exit: 0.5338m, fee: 0.72m, stopLoss: 0.5470m,
                openedAt: At(5, 9, 40), duration: new TimeSpan(3, 42, 0), strategy: null,
                note: null),

            Create("SOL-USDT-SWAP", InstrumentType.Perpetual, leverage: 10, TradeDirection.Short,
                quantity: 25m, entry: 178.40m, exit: 181.10m, fee: 1.35m, stopLoss: null,
                openedAt: At(7, 13, 5), duration: new TimeSpan(1, 55, 0), strategy: null,
                note: "Bez stop lossa, zamknięte ręcznie w panice. Nie powtarzać."),

            Create("BTC-USDT", InstrumentType.Spot, leverage: null, TradeDirection.Long,
                quantity: 0.05m, entry: 64_980m, exit: 66_210m, fee: 3.12m, stopLoss: 64_200m,
                openedAt: At(9, 7, 50), duration: new TimeSpan(26, 15, 0), strategy: trend,
                note: "Pozycja na dłużej, wyjście na poprzednim szczycie."),

            Create("ETH-USDT-SWAP", InstrumentType.Perpetual, leverage: 10, TradeDirection.Long,
                quantity: 3m, entry: 2_480.20m, exit: 2_512.60m, fee: 2.99m, stopLoss: 2_455m,
                openedAt: At(11, 11, 15), duration: new TimeSpan(2, 5, 0), strategy: pullback,
                note: "Podręcznikowy setup. Więcej takich."),

            Create("DOGE-USDT-SWAP", InstrumentType.Perpetual, leverage: 5, TradeDirection.Short,
                quantity: 20_000m, entry: 0.2051m, exit: 0.2088m, fee: 2.07m, stopLoss: 0.2088m,
                openedAt: At(13, 20, 30), duration: new TimeSpan(0, 34, 0), strategy: resistance,
                note: "Opór nie wytrzymał. SL zadziałał, tak miało być."),

            Create("BTC-USDT-SWAP", InstrumentType.Perpetual, leverage: 5, TradeDirection.Long,
                quantity: 0.12m, entry: 65_410m, exit: 65_390m, fee: 3.14m, stopLoss: 64_900m,
                openedAt: At(16, 15, 0), duration: new TimeSpan(5, 20, 0), strategy: range,
                note: null),

            Create("XRP-USDT", InstrumentType.Spot, leverage: null, TradeDirection.Long,
                quantity: 2_000m, entry: 0.5120m, exit: 0.5296m, fee: 1.04m, stopLoss: null,
                openedAt: At(20, 12, 10), duration: new TimeSpan(8, 0, 0), strategy: null,
                note: null),
        ];
    }

    /// <summary>
    /// Tworzy trade i wylicza wynik netto z cen, żeby liczby w seedzie zawsze się zgadzały.
    /// </summary>
    private static Trade Create(
        string instrument, InstrumentType instrumentType, int? leverage, TradeDirection direction,
        decimal quantity, decimal entry, decimal exit, decimal fee, decimal? stopLoss,
        DateTime openedAt, TimeSpan duration, Strategy? strategy, string? note)
    {
        var priceMove = direction == TradeDirection.Long ? exit - entry : entry - exit;

        return new Trade
        {
            Instrument = instrument,
            InstrumentType = instrumentType,
            Leverage = leverage,
            Direction = direction,
            Quantity = quantity,
            EntryPrice = entry,
            ExitPrice = exit,
            StopLoss = stopLoss,
            Fee = fee,
            NetPnl = priceMove * quantity - fee,
            OpenedAt = openedAt,
            ClosedAt = openedAt + duration,
            Strategy = strategy,
            Note = note,
        };
    }
}
