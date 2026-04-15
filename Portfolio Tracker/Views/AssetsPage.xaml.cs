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
using Portfolio_Tracker.Models;
using Portfolio_Tracker.ViewModels;

namespace Portfolio_Tracker.Views
{
    /// <summary>
    /// Interaction logic for AssetsPage.xaml
    /// </summary>
    public partial class AssetsPage : Page
    {
        private AssetsViewModel vm;
        public AssetsPage()
        {
            InitializeComponent();
            vm = new AssetsViewModel();
            DataContext = vm;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            vm.Assets.Add(new Asset
            {
                Symbol = "—",
                Name = "—",
                AssetType = "—",
                Currency = "—"
            });
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Редагування в розробці");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedAsset != null)
            {
                vm.Assets.Remove(vm.SelectedAsset);
            }
            else
            {
                MessageBox.Show("Оберіть актив");
            }
        }
    }
}
