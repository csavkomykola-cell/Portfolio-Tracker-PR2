using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Tracker.ViewModels
{
    public class DashboardViewModel
    {
        public string TotalValue { get; set; }
        public string Profit { get; set; }
        public int AssetCount { get; set; }

        public ObservableCollection<dynamic> Assets { get; set; }

        public DashboardViewModel()
        {
            TotalValue = "140,000 $";
            Profit = "+4,400 $";
            AssetCount = 4;

            Assets = new ObservableCollection<dynamic>
            {
                new { Symbol="AAPL", Name="Apple", CurrentPrice=400 },
                new { Symbol="BTC", Name="Bitcoin", CurrentPrice=40000 },
                new { Symbol="TSLA", Name="Tesla", CurrentPrice=440 }
            };
        }
    }
}