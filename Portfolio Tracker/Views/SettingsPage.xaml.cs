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
using System.IO;
using System.Windows.Threading;

namespace Portfolio_Tracker.Views
{
    public partial class SettingsPage : Page
    {
        private string filePath = "Data/settings.txt";

        public SettingsPage()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string theme = (ThemeBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string lang = (LanguageBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            Directory.CreateDirectory("Data");
            File.WriteAllText(filePath, theme + "/" + lang);

            ApplyTheme(theme);

            Dispatcher.Invoke(() => { }, DispatcherPriority.Render);

            MessageBox.Show("Налаштування збережено");
        }

        private void LoadSettings()
        {
            if (!File.Exists(filePath)) return;

            var data = File.ReadAllText(filePath).Split('/');

            if (data.Length == 2)
            {
                SetComboBox(ThemeBox, data[0]);
                SetComboBox(LanguageBox, data[1]);

                ApplyTheme(data[0]);
            }
        }

        private void SetComboBox(ComboBox box, string value)
        {
            foreach (ComboBoxItem item in box.Items)
            {
                if (item.Content.ToString() == value)
                {
                    box.SelectedItem = item;
                    break;
                }
            }
        }

        private void ApplyTheme(string theme)
        {
            if (theme == "Світла")
            {
                Application.Current.Resources["MainBackground"] = new SolidColorBrush(Colors.White);
                Application.Current.Resources["CardBackground"] = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                Application.Current.Resources["TextColor"] = new SolidColorBrush(Colors.Black);
            }
            else
            {
                Application.Current.Resources["MainBackground"] = new SolidColorBrush(Color.FromRgb(18, 18, 26));
                Application.Current.Resources["CardBackground"] = new SolidColorBrush(Color.FromRgb(30, 30, 47));
                Application.Current.Resources["TextColor"] = new SolidColorBrush(Colors.White);
            }

            ((App)Application.Current).UpdateTableTextColorToOpposite();
        }
    }
}
