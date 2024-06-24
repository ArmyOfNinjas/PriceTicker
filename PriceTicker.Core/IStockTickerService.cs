namespace PriceTicker.Core;

public interface IStockTickerService
{
    Dictionary<string, decimal> StockPrices { get; }
    event EventHandler<StockPriceChangedEventArgs> StockPriceChanged;
    Task StartAsync(CancellationToken cancellationToken);
    void Subscribe(string stockName);
    void Unsubscribe(string stockName); // For future.
}