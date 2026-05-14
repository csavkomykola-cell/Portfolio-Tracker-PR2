using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Portfolio_Tracker.Models
{
    public class PortfolioItem : INotifyPropertyChanged
    {
        private string _symbol;
        public string Symbol
        {
            get => _symbol;
            set => SetProperty(ref _symbol, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private double _quantity;
        public double Quantity
        {
            get => _quantity;
            set
            {
                if (SetProperty(ref _quantity, value))
                    OnPropertyChanged(nameof(CurrentValue));
            }
        }

        private decimal _averagePrice;
        public decimal AveragePrice
        {
            get => _averagePrice;
            set
            {
                if (SetProperty(ref _averagePrice, value))
                    OnPropertyChanged(nameof(CurrentValue));
            }
        }

        private decimal _currentPrice;
        public decimal CurrentPrice
        {
            get => _currentPrice;
            set
            {
                if (SetProperty(ref _currentPrice, value))
                    OnPropertyChanged(nameof(CurrentValue));
            }
        }

        public decimal CurrentValue => (decimal)Quantity * CurrentPrice;

        public decimal ProfitLoss => CurrentValue - (decimal)Quantity * AveragePrice;

        private string _currency;
        public string Currency
        {
            get => _currency;
            set => SetProperty(ref _currency, value);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(backingField, value))
                return false;
            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}