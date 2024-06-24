namespace PriceTicker;

public record PriceHistory
{
    public DateTime DateTime { get; set; }
    public decimal Price { get; set; }
}