using Portfolio_Tracker.Models;
using Portfolio_Tracker.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Portfolio_Tracker.ViewModels
{
    public class AssetsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Asset> Assets { get; set; }
        private Asset _selectedAsset;
        public Asset SelectedAsset
        {
            get => _selectedAsset;
            set
            {
                _selectedAsset = value;
                OnPropertyChanged(nameof(SelectedAsset));
            }
        }

        public AssetsViewModel()
        {
            // Завантаження з JSON
            var loadedAssets =
                JsonService.Load<ObservableCollection<Asset>>("Data/assets.json");

            Assets = loadedAssets ?? new ObservableCollection<Asset>();

            // Якщо файл пустий - додаються стартові дані
            if (Assets.Count == 0)
            {
                Assets.Add(new Asset
                {
                    Symbol = "AAPL",
                    Name = "Apple",
                    AssetType = "Акція",
                    Currency = "USD"
                });

                Assets.Add(new Asset
                {
                    Symbol = "BTC",
                    Name = "Bitcoin",
                    AssetType = "Криптовалюта",
                    Currency = "USD"
                });

                Assets.Add(new Asset
                {
                    Symbol = "TSLA",
                    Name = "Tesla",
                    AssetType = "Акція",
                    Currency = "USD"
                });

                SaveAssets();
            }
        }
        public void SaveAssets()
        {
            var validAssets = Assets
                .Where(a =>
                    !string.IsNullOrWhiteSpace(a.Symbol) &&
                    !string.IsNullOrWhiteSpace(a.Name))
                .ToList();
            Services.JsonService.Save("Data/assets.json", validAssets);
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(name));
        }
    }
}