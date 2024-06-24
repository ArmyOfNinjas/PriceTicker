using System.Collections.ObjectModel;

namespace PriceTicker;

public partial class HistoryWindow
{
    private ObservableCollection<PriceHistory> _priceHistories;
    public HistoryWindow(ObservableCollection<PriceHistory> priceHistories)
    {
        InitializeComponent();
        _priceHistories = priceHistories;
        HistoryListView.ItemsSource = priceHistories;
    }
}