using Portfolio_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Assets = new ObservableCollection<Asset>
            {
                new Asset { Symbol = "AAPL", Name = "Apple", AssetType = "Акція", Currency = "USD" },
                new Asset { Symbol = "BTC", Name = "Bitcoin", AssetType = "Криптовалюта", Currency = "USD" },
                new Asset { Symbol = "TSLA", Name = "Tesla", AssetType = "Акція", Currency = "USD" },
            };
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}