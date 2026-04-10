using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Portfolio_Tracker.Views;

namespace Portfolio_Tracker.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new DashboardPage());
        }
        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DashboardPage());
        }

        private void Portfolio_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Портфель (в розробці)");
        }

        private void Transactions_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Транзакції (в розробці)");
        }

        private void Assets_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Активи (в розробці)");
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Налаштування (в розробці)");
        }
    }
}