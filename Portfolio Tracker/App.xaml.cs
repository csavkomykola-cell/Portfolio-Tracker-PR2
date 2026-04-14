using System.IO;
using System.Windows;
using System.Windows.Media;

namespace Portfolio_Tracker
{
    public partial class App : Application
    {
        public void UpdateTableTextColorToOpposite()
        {
            if (Current?.Resources == null) return;
            Current.Resources["TableTextColor"] = new SolidColorBrush(Colors.Black);
        }

        public void ApplyTheme(bool dark)
        {
            if (Current?.Resources == null) return;
            var res = Current.Resources;

            if (dark)
            {
                res["MainBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#12121A"));
                res["CardBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E1E2F"));
                res["TextColor"] = new SolidColorBrush(Colors.White);
                res["SidebarHoverBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E2E4F"));
                res["ButtonPrimaryBackground"] = new SolidColorBrush(Colors.White);
                res["ButtonPrimaryForeground"] = new SolidColorBrush(Colors.Black);
                res["ButtonDangerBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4C4C"));
                res["ButtonDangerForeground"] = new SolidColorBrush(Colors.White);
            }
            else
            {
                res["MainBackground"] = new SolidColorBrush(Colors.White);
                res["CardBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5F5F8"));
                res["TextColor"] = new SolidColorBrush(Colors.Black);
                res["SidebarHoverBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0E0F0"));
                res["ButtonPrimaryBackground"] = new SolidColorBrush(Colors.Black);
                res["ButtonPrimaryForeground"] = new SolidColorBrush(Colors.White);
                res["ButtonDangerBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4C4C"));
                res["ButtonDangerForeground"] = new SolidColorBrush(Colors.White);
            }

            res["TableTextColor"] = new SolidColorBrush(Colors.Black);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                var filePath = "Data/settings.txt";
                if (File.Exists(filePath))
                {
                    var data = File.ReadAllText(filePath).Split('/');
                    if (data.Length >= 1)
                    {
                        var theme = data[0]?.Trim();
                        bool isLight = string.Equals(theme, "Світла", System.StringComparison.OrdinalIgnoreCase)
                                       || string.Equals(theme, "Light", System.StringComparison.OrdinalIgnoreCase);
                        ApplyTheme(!isLight);
                    }
                }
            }
            catch
            {
            }

            base.OnStartup(e);
        }
    }
}