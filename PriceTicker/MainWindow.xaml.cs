using System.Collections.ObjectModel;
using System.Windows.Data;
using PriceTicker.Core;

namespace PriceTicker;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly IStockTickerService _stockTickerService;
        private readonly ObservableCollection<StockPrice> _stockPrices;
        private readonly Dictionary<string, ObservableCollection<PriceHistory>> _priceHistories;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();

            _stockTickerService = new StockTickerService();
            _stockTickerService.StockPriceChanged += OnStockPriceChanged;

            _stockPrices = new ObservableCollection<StockPrice>();
            _priceHistories = new Dictionary<string?, ObservableCollection<PriceHistory>>();
            foreach (var kvp in _stockTickerService.StockPrices)
            {
                _stockPrices.Add(new() { StockName = kvp.Key, Price = kvp.Value });
                _priceHistories.Add(kvp.Key, []);
                _stockTickerService.Subscribe(kvp.Key);
            }
            
            StockListView.ItemsSource = _stockPrices;
  
            _cancellationTokenSource = new CancellationTokenSource();

            Task.Run(() => _stockTickerService.StartAsync(_cancellationTokenSource.Token));
        }
        
        private void OnStockPriceChanged(object sender, StockPriceChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                StockPropertyChanger.ChangeStockColorAndPrice(e, _stockPrices, _priceHistories);
                // Refresh the UI
                CollectionViewSource.GetDefaultView(StockListView.ItemsSource).Refresh();
            });
        }

        private void StockListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (StockListView.SelectedItem is not StockPrice selectedStock) return;
            var historyWindow = new HistoryWindow(_priceHistories[selectedStock.StockName]);
            historyWindow.Show();
        }
        
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _cancellationTokenSource.Cancel();
        }
}