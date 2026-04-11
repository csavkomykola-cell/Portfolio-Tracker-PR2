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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Portfolio_Tracker.ViewModels;

namespace Portfolio_Tracker.Views
{
    /// <summary>
    /// Interaction logic for PortfolioPage.xaml
    /// </summary>
    public partial class PortfolioPage : Page
    {
        public PortfolioPage()
        {
            InitializeComponent();
            DataContext = new PortfolioViewModel();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
