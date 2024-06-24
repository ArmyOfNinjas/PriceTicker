namespace PriceTicker.Core;

public class StockPriceChangedEventArgs : EventArgs
{
    public string? StockName { get; init; }
    public decimal NewPrice { get; init; }
    public DateTime Time { get; init; }
}