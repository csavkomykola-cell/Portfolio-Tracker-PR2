using Portfolio_Tracker.Services;
using System.Windows;
using System.Windows.Media.Animation;

namespace Portfolio_Tracker.Views
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var anim = new DoubleAnimation(0, 1, new System.Windows.Duration(System.TimeSpan.FromSeconds(0.18)));
            this.BeginAnimation(OpacityProperty, anim);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameText.Text?.Trim();
            var fullName = FullNameText.Text?.Trim();
            var password = PasswordBox.Password;
            var confirm = ConfirmBox.Password;

            if (password != confirm)
            {
                MessageBox.Show((string)TryFindResource("PasswordsDoNotMatch") ?? "Passwords do not match.", (string)TryFindResource("ValidationError") ?? "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var (ok, message, user) = AuthService.Register(username, password, fullName);
            if (ok)
            {
                MessageBox.Show((string)TryFindResource("RegistrationSuccess") ?? "Registered", (string)TryFindResource("Success") ?? "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(message, (string)TryFindResource("ValidationError") ?? "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}