using System.Collections.ObjectModel;
using System.Windows.Media;
using PriceTicker.Core;

namespace PriceTicker;

public static class StockPropertyChanger
{
    public static void ChangeStockColorAndPrice(
        StockPriceChangedEventArgs e,
        ObservableCollection<StockPrice> stockPrices,
        Dictionary<string, ObservableCollection<PriceHistory>> priceHistories)
    {
        var stock = stockPrices.FirstOrDefault(s => s.StockName == e.StockName);
        if (stock != null)
        {
            var oldPrice = stock.Price;
            stock.Price = e.NewPrice;
            
            stock.PriceChange = e.NewPrice - oldPrice;
            stock.PriceColor = Brushes.Black;
            if (stock.PriceChange > 0)
            {
                stock.PriceChangeColor = Brushes.Green;
            }
            else if (stock.PriceChange < 0)
            {
                stock.PriceChangeColor = Brushes.Red;
            }
            else
            {
                stock.PriceChangeColor = Brushes.Gray;
            }
            
            priceHistories[e.StockName].Add(new PriceHistory
            {
                DateTime = e.Time,
                Price = e.NewPrice
            });
        }
    }
}