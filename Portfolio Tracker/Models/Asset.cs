using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Tracker.Models
{
    public class Asset
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string AssetType { get; set; }
        public string Currency { get; set; }
    }
}
