using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Portfolio_Tracker.Models;

namespace Portfolio_Tracker.ViewModels
{
    public class TransactionsViewModel
    {
        public ObservableCollection<Transaction> Transactions { get; set; }
        public TransactionsViewModel() 
        {
            Transactions = new ObservableCollection<Transaction>
            {
                new Transaction
                {
                    Type = "Купівля",
                    Asset = "AAPL",
                    Quantity = 40,
                    Price = 450,
                    Date = "01.04.2026"
                },
                new Transaction
                {
                    Type = "Продаж",
                    Asset = "BTC",
                    Quantity = 0.4,
                    Price = 44000,
                    Date = "05.04.2026"
                },

                new Transaction
                {
                    Type = "Дивідент",
                    Asset = "TSLA",
                    Quantity = 4,
                    Price = 440,
                    Date = "09.04.2026"
                },
            };
        }
    }
}
