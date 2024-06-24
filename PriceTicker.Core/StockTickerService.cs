namespace PriceTicker.Core;

using System;
using System.Collections.Generic;

public class StockTickerService : IStockTickerService
{
    private readonly Dictionary<string, bool> _subscriptions = new ();
    private readonly Random _random;

    public Dictionary<string, decimal> StockPrices { get; }
    public event EventHandler<StockPriceChangedEventArgs>? StockPriceChanged;
    
    public StockTickerService()
    {
        StockPrices = new Dictionary<string, decimal>
        {
            { "Stock1", 0 },
            { "Stock2", 0 }
        };
        _random = new Random();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            foreach (var stockName in StockPrices.Keys)
            {
                if (_subscriptions.ContainsKey(stockName) && !_subscriptions[stockName])
                {
                    continue;
                }

                ChangePrice(stockName);
            }

            await Task.Delay(1000, cancellationToken);
        }
    }

    public void Subscribe(string stockName)
    {
        _subscriptions[stockName] = true;
    }

    public void Unsubscribe(string stockName)
    {
        _subscriptions[stockName] = false;
    }

    private void ChangePrice(string stockName)
    {
        var newPrice = stockName == "Stock1" ? _random.Next(24000, 27000) / 100m : _random.Next(18000, 21000) / 100m;

        StockPrices[stockName] = newPrice;

        StockPriceChanged?.Invoke(this, new StockPriceChangedEventArgs
        {
            StockName = stockName,
            NewPrice = newPrice,
            Time = DateTime.Now
        });
    }
}