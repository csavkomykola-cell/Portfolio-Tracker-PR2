using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Tracker.Models
{
    public class Transaction
    {
        public string Type { get; set; }
        public string Asset { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public string Date { get; set; }
    }
}
