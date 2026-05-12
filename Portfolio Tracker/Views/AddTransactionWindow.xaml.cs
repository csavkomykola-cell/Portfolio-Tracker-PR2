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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System;
using System.Globalization;
using Portfolio_Tracker.Models;

namespace Portfolio_Tracker.Views
{
    /// <summary>
    /// Interaction logic for AddTransactionWindow.xaml
    /// </summary>
    public partial class AddTransactionWindow : Window
    {
        public AddTransactionWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Прочитує та перевіряє введені дані
            var typeItem = TypeCombo.SelectedItem as ComboBoxItem;
            string type = typeItem?.Content?.ToString() ?? string.Empty;
            string asset = AssetText.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(asset))
            {
                MessageBox.Show("Заповніть тип та актив.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(QuantityText.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double quantity))
            {
                MessageBox.Show("Невірна кількість.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(PriceText.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price))
            {
                MessageBox.Show("Невірна ціна.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string dateStr;
            if (DatePicker.SelectedDate.HasValue)
            {
                dateStr = DatePicker.SelectedDate.Value.ToString("dd.MM.yyyy");
            }
            else
            {
                dateStr = DateTime.Now.ToString("dd.MM.yyyy");
            }

            Transaction = new Transaction
            {
                Type = type,
                Asset = asset,
                Quantity = quantity,
                Price = price,
                Date = dateStr
            };

            DialogResult = true;
            Close();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.2)
            };

            this.BeginAnimation(OpacityProperty, animation);
        }

        public Transaction Transaction { get; set; }
    }
}
