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

namespace Portfolio_Tracker.Views
{
    /// <summary>
    /// Interaction logic for TransactionsPage.xaml
    /// </summary>
    public partial class TransactionsPage : Page
    {
        public TransactionsPage()
        {
            InitializeComponent();
            DataContext = new ViewModels.TransactionsViewModel();
        }

        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Форма додавання транзакції в розробці");
        }
    }
}
