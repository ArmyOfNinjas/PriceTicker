using System.Collections.ObjectModel;
using System.Windows.Media;
using PriceTicker;
using PriceTicker.Core;

public class StockPropertyChangerTests
{
    [Fact]
    public void ChangeStockColorAndPrice_UpdatesStockPrice()
    {
        // Arrange
        var stockPrices = new ObservableCollection<StockPrice>
        {
            new() { StockName = "Stock1", Price = 100 }
        };
        var priceHistories = new Dictionary<string, ObservableCollection<PriceHistory>>
        {
            { "Stock1", new ObservableCollection<PriceHistory>() }
        };
        var eventArgs = new StockPriceChangedEventArgs
        {
            StockName = "Stock1",
            NewPrice = 110,
            Time = DateTime.Now
        };

        // Act
        StockPropertyChanger.ChangeStockColorAndPrice(eventArgs, stockPrices, priceHistories);

        // Assert
        var stock = stockPrices.First(s => s.StockName == "Stock1");
        Assert.Equal(110, stock.Price);
        Assert.Equal(10, stock.PriceChange);
        Assert.Equal(Brushes.Green, stock.PriceChangeColor);
    }

    [Fact]
    public void ChangeStockColorAndPrice_UpdatesPriceChangeColor_Red()
    {
        // Arrange
        var stockPrices = new ObservableCollection<StockPrice>
        {
            new() { StockName = "Stock1", Price = 100 }
        };
        var priceHistories = new Dictionary<string, ObservableCollection<PriceHistory>>
        {
            { "Stock1", new ObservableCollection<PriceHistory>() }
        };
        var eventArgs = new StockPriceChangedEventArgs
        {
            StockName = "Stock1",
            NewPrice = 90,
            Time = DateTime.Now
        };

        // Act
        StockPropertyChanger.ChangeStockColorAndPrice(eventArgs, stockPrices, priceHistories);

        // Assert
        var stock = stockPrices.First(s => s.StockName == "Stock1");
        Assert.Equal(90, stock.Price);
        Assert.Equal(-10, stock.PriceChange);
        Assert.Equal(Brushes.Red, stock.PriceChangeColor);
    }

    [Fact]
    public void ChangeStockColorAndPrice_UpdatesPriceChangeColor_Gray()
    {
        // Arrange
        var stockPrices = new ObservableCollection<StockPrice>
        {
            new() { StockName = "Stock1", Price = 100 }
        };
        var priceHistories = new Dictionary<string, ObservableCollection<PriceHistory>>
        {
            { "Stock1", new ObservableCollection<PriceHistory>() }
        };
        var eventArgs = new StockPriceChangedEventArgs
        {
            StockName = "Stock1",
            NewPrice = 100,
            Time = DateTime.Now
        };

        // Act
        StockPropertyChanger.ChangeStockColorAndPrice(eventArgs, stockPrices, priceHistories);

        // Assert
        var stock = stockPrices.First(s => s.StockName == "Stock1");
        Assert.Equal(100, stock.Price);
        Assert.Equal(0, stock.PriceChange);
        Assert.Equal(Brushes.Gray, stock.PriceChangeColor);
    }

    [Fact]
    public void ChangeStockColorAndPrice_AddsPriceHistory()
    {
        // Arrange
        var stockPrices = new ObservableCollection<StockPrice>
        {
            new() { StockName = "Stock1", Price = 100 }
        };
        var priceHistories = new Dictionary<string, ObservableCollection<PriceHistory>>
        {
            { "Stock1", new ObservableCollection<PriceHistory>() }
        };
        var eventArgs = new StockPriceChangedEventArgs
        {
            StockName = "Stock1",
            NewPrice = 110,
            Time = DateTime.Now
        };

        // Act
        StockPropertyChanger.ChangeStockColorAndPrice(eventArgs, stockPrices, priceHistories);

        // Assert
        var history = priceHistories["Stock1"];
        Assert.Single(history);
        Assert.Equal(110, history.First().Price);
        Assert.Equal(eventArgs.Time, history.First().DateTime);
    }
}
