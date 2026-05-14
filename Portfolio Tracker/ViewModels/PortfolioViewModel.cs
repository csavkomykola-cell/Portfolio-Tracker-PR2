using Portfolio_Tracker.Models;
using Portfolio_Tracker.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace Portfolio_Tracker.ViewModels
{
    public class PortfolioViewModel : INotifyPropertyChanged, IDisposable
    {
        private const string TransactionsPath = "Data/transactions.json";
        private const string AssetsPath = "Data/assets.json";

        public ObservableCollection<PortfolioItem> Portfolio { get; set; } = new ObservableCollection<PortfolioItem>();

        private decimal _totalValue;
        public decimal TotalValue
        {
            get => _totalValue;
            private set { _totalValue = value; OnPropertyChanged(nameof(TotalValue)); }
        }

        private decimal _totalProfitLoss;
        public decimal TotalProfitLoss
        {
            get => _totalProfitLoss;
            private set { _totalProfitLoss = value; OnPropertyChanged(nameof(TotalProfitLoss)); }
        }

        private int _assetCount;
        public int AssetCount
        {
            get => _assetCount;
            private set { _assetCount = value; OnPropertyChanged(nameof(AssetCount)); }
        }

        private FileSystemWatcher _watcher;
        private PriceService _priceService;

        public bool IsPriceLoading => _priceService?.IsLoading ?? false;

        public PortfolioViewModel()
        {
            _priceService = new PriceService();
            _priceService.PricesUpdated += OnPricesUpdated;
            _priceService.PricesUpdateFailed += ex => {
                // Показати лаконічне сповіщення або мовчки ігнорувати — підтримувати чуйність інтерфейсу
                Application.Current?.Dispatcher?.BeginInvoke(new Action(() =>
                {
                }), DispatcherPriority.Background);
            };

            Recalculate();

            // Відстежувати файл транзакцій, щоб зміни (збереження) автоматично оновлювали портфель
            try
            {
                var dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
                if (!Directory.Exists(dataDir)) Directory.CreateDirectory(dataDir);

                _watcher = new FileSystemWatcher(dataDir, Path.GetFileName(TransactionsPath))
                {
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
                };
                _watcher.Changed += OnTransactionsFileChanged;
                _watcher.EnableRaisingEvents = true;
            }
            catch
            {
                // ігнорувати помилки спостерігача
            }
        }

        private void OnTransactionsFileChanged(object sender, FileSystemEventArgs e)
        {
            // файл може бути тимчасово заблокований; затримка та виклик у UI-потоці
            Application.Current?.Dispatcher?.BeginInvoke(new Action(() =>
            {
                try
                {
                    Recalculate();
                }
                catch
                {
                    // ігнорувати тимчасові помилки читання
                }
            }), DispatcherPriority.Background);
        }

        private void OnPricesUpdated(System.Collections.Generic.Dictionary<string, decimal> prices)
        {
            if (prices == null || prices.Count == 0) return;

            Application.Current?.Dispatcher?.BeginInvoke(new Action(() =>
            {
                // Оновити відповідні елементи портфеля та перерахувати підсумки
                var updated = false;
                foreach (var p in Portfolio)
                {
                    if (prices.TryGetValue(p.Symbol.ToUpperInvariant(), out var price))
                    {
                        p.CurrentPrice = price;
                        updated = true;
                    }
                }

                if (updated)
                    UpdateSummaries();
            }), DispatcherPriority.Background);
        }

        public void Recalculate()
        {
            // Завантажити транзакції та активи
            var transactions = JsonService.LoadTransactions<ObservableCollection<Transaction>>() ?? new ObservableCollection<Transaction>();
            var assets = JsonService.LoadAssets<ObservableCollection<Asset>>() ?? new ObservableCollection<Asset>();

            // Групувати транзакції за символом активу
            var groups = transactions
                .GroupBy(t => (t.Asset ?? string.Empty).Trim().ToUpperInvariant())
                .Where(g => !string.IsNullOrEmpty(g.Key));

            var newPortfolio = groups.Select(g =>
            {
                var symbol = g.Key;
                var name = g.FirstOrDefault()?.Asset ?? symbol;

                // Підсумувати покупки та продажі окремо
                double buyQty = g.Where(t => string.Equals(t.Type, "Buy", StringComparison.OrdinalIgnoreCase)).Sum(t => t.Quantity);
                decimal buyCost = g.Where(t => string.Equals(t.Type, "Buy", StringComparison.OrdinalIgnoreCase))
                                    .Sum(t => (decimal)t.Quantity * t.Price + t.Fees);

                double sellQty = g.Where(t => string.Equals(t.Type, "Sell", StringComparison.OrdinalIgnoreCase)).Sum(t => t.Quantity);

                double netQuantity = buyQty - sellQty;

                decimal avgPrice = 0m;
                if (buyQty > 0)
                    avgPrice = buyCost / (decimal)buyQty;

                var asset = assets.FirstOrDefault(a => string.Equals(a.Symbol, symbol, StringComparison.OrdinalIgnoreCase));
                decimal currentPrice = asset?.CurrentPrice ?? 0m;
                string currency = asset?.Currency ?? "USD";

                return new PortfolioItem
                {
                    Symbol = symbol,
                    Name = asset?.Name ?? name,
                    Quantity = netQuantity,
                    AveragePrice = avgPrice,
                    CurrentPrice = currentPrice,
                    Currency = currency
                };
            })
            // Зберігати лише позитивні активи
            .Where(p => p.Quantity > 0)
            .OrderBy(p => p.Symbol)
            .ToList();

            // Оновити спостережувану колекцію
            Portfolio.Clear();
            foreach (var item in newPortfolio)
                Portfolio.Add(item);

            // Запустити автоматичне оновлення цін для криптовалютних символів, які ми маємо
            var symbols = Portfolio.Select(p => p.Symbol).ToList();
            _priceService?.StartAutoRefresh(symbols);

            UpdateSummaries();
        }

        private void UpdateSummaries()
        {
            TotalValue = Portfolio.Sum(p => p.CurrentValue);
            TotalProfitLoss = Portfolio.Sum(p => p.ProfitLoss);
            AssetCount = Portfolio.Count;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public void Dispose()
        {
            if (_watcher != null)
            {
                _watcher.EnableRaisingEvents = false;
                _watcher.Dispose();
                _watcher = null;
            }

            _priceService?.Dispose();
            _priceService = null;
        }
    }
}