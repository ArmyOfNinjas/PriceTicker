using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace PriceTicker;

public class StockPrice : INotifyPropertyChanged
{
    private decimal _price;
    private decimal _priceChange;
    private Brush _priceColor;
    private Brush _priceChangeColor;

    public string StockName { get; set; }

    public decimal Price
    {
        get => _price;
        set
        {
            if (_price != value)
            {
                _price = value;
                OnPropertyChanged();
            }
        }
    }

    public decimal PriceChange
    {
        get => _priceChange;
        set
        {
            if (_priceChange != value)
            {
                _priceChange = value;
                OnPropertyChanged();
            }
        }
    }

    public Brush PriceColor
    {
        get => _priceColor;
        set
        {
            if (_priceColor != value)
            {
                _priceColor = value;
                OnPropertyChanged();
            }
        }
    }

    public Brush PriceChangeColor
    {
        get => _priceChangeColor;
        set
        {
            if (_priceChangeColor != value)
            {
                _priceChangeColor = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}