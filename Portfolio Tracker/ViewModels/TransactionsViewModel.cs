using Portfolio_Tracker.Services;
using Portfolio_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Tracker.ViewModels
{
    public class TransactionsViewModel
    {
        private const string Path = "Data/transactions.json";

        public ObservableCollection<Transaction> Transactions { get; set; }

        public TransactionsViewModel()
        {
            var loaded = JsonService.Load<ObservableCollection<Transaction>>(Path);

            Transactions = loaded ?? new ObservableCollection<Transaction>();

            if (Transactions.Count == 0)
            {
                Transactions.Add(new Transaction
                {
                    Type = "Купівля",
                    Asset = "AAPL",
                    Quantity = 40,
                    Price = 450,
                    Date = "01.04.2026"
                });
            }
        }

        public void Save()
        {
            JsonService.Save(Path, Transactions);
        }
    }
}
