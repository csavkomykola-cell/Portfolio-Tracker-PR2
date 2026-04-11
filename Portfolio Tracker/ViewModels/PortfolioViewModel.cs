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
                    Quantity=40,
                    AvgPrice=450,
                    CurrentPrice=480
                },
                new PortfolioItem
                {
                    Symbol="BTC",
                    Name="Bitcoin",
                    Quantity=0.4,
                    AvgPrice=40000,
                    CurrentPrice=44000
                },
                new PortfolioItem
                {
                    Symbol="TSLA",
                    Name="Tesla",
                    Quantity=4,
                    AvgPrice=440,
                    CurrentPrice=4400
                }
            };
        }
    }
}