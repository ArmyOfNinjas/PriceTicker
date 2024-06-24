using PriceTicker.Core;

namespace StockTickerServiceTests;

public class StockTickerServiceTests
{
    private readonly StockTickerService _service = new();

    [Fact]
    public void Subscribe_AddsSubscription()
    {
        _service.Subscribe("Stock1");
        Assert.True(_service.StockPrices.ContainsKey("Stock1"));
    }

    [Fact]
    public async Task StartAsync_TriggersPriceChange()
    {
        var eventTriggered = false;
        _service.StockPriceChanged += (sender, args) => eventTriggered = true;
        
        var cts = new CancellationTokenSource();
        _service.StartAsync(cts.Token);

        await Task.Delay(1100); // Wait for one price change interval
        await cts.CancelAsync();

        Assert.True(eventTriggered);
    }
}