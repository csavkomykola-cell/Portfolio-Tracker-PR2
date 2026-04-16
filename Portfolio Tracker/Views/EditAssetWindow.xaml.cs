using Portfolio_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Portfolio_Tracker.Views
{
    public partial class EditAssetWindow : Window
    {
        public Asset Asset { get; set; }

        public EditAssetWindow(Asset asset)
        {
            InitializeComponent();
            Asset = asset;
            DataContext = Asset;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}