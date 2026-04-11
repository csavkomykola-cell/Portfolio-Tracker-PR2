using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Tracker.Models
{
    public class PortfolioItem
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal TotalValue => (decimal)Quantity * CurrentPrice;
        public decimal Profit => TotalValue - ((decimal)Quantity * AvgPrice);
    }
}
