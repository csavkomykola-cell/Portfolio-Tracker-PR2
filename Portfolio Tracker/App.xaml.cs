using System.Windows;
using System.Windows.Media;

namespace Portfolio_Tracker
{
    public partial class App : Application
    {
        public void UpdateTableTextColorToOpposite()
        {
            if (Current?.Resources == null) return;

            var res = Current.Resources;
            if (!(res["TextColor"] is SolidColorBrush textBrush)) return;
            res["TableTextColor"] = new SolidColorBrush(textBrush.Color);
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
                res["ButtonDangerBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4C4C"));
            }
            else
            {
                res["MainBackground"] = new SolidColorBrush(Colors.White);
                res["CardBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5F5F8"));
                res["TextColor"] = new SolidColorBrush(Colors.Black);
                res["SidebarHoverBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0E0F0"));
                res["ButtonPrimaryBackground"] = new SolidColorBrush(Colors.Black);
                res["ButtonDangerBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4C4C"));
            }

            UpdateTableTextColorToOpposite();
        }
    }
}