using Portfolio_Tracker.Services;
using Portfolio_Tracker.Views;
using System.Windows;
using System.Windows.Media.Animation;

namespace Portfolio_Tracker.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var anim = new DoubleAnimation(0, 1, new System.Windows.Duration(System.TimeSpan.FromSeconds(0.18)));
            this.BeginAnimation(OpacityProperty, anim);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameText.Text?.Trim();
            var password = PasswordBox.Password;

            var (ok, message, user) = AuthService.Login(username, password);
            if (ok)
            {
                var main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show((string)TryFindResource("InvalidCredentials") ?? message, (string)TryFindResource("ValidationError") ?? "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var reg = new RegisterWindow();
            reg.Owner = this;
            reg.ShowDialog();
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            var guest = AuthService.Guest();
            var main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}