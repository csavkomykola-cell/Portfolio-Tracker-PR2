using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio_Tracker.Models;
using System.Collections.ObjectModel;

namespace Portfolio_Tracker.ViewModels
{
    public class AssetsViewModel
    {
        public ObservableCollection<Asset> Assets { get; set; }
        public Asset SelectedAsset { get; set; }
        public AssetsViewModel()
        {
            Assets = new ObservableCollection<Asset>
            {
                new Asset { Symbol = "AAPL", Name = "Apple", AssetType = "Акція", Currency = "USD" },
                new Asset { Symbol = "BTC", Name = "Bitcoin", AssetType = "Криптовалюта", Currency = "USD" },
                new Asset { Symbol = "TSLA", Name = "Tesla", AssetType = "Акція", Currency = "USD" },
            };
        }
    }
}
