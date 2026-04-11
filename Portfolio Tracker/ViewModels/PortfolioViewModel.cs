using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio_Tracker.Models;
using System.Collections.ObjectModel;

namespace Portfolio_Tracker.ViewModels
{
    public class PortfolioViewModel
    {
        public ObservableCollection<PortfolioItem> PortfolioItems { get; set; }

        public PortfolioViewModel()
        {
            PortfolioItems = new ObservableCollection<PortfolioItem>
            {
                new PortfolioItem
                {
                    Symbol="AAPL",
                    Name="Apple",
                    Quantity=10,
                    AvgPrice=150,
                    CurrentPrice=180
                },
                new PortfolioItem
                {
                    Symbol="BTC",
                    Name="Bitcoin",
                    Quantity=0.5,
                    AvgPrice=50000,
                    CurrentPrice=60000
                },
                new PortfolioItem
                {
                    Symbol="TSLA",
                    Name="Tesla",
                    Quantity=5,
                    AvgPrice=220,
                    CurrentPrice=250
                }
            };
        }
    }
}