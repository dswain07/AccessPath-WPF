using System.Windows;
using System.Windows.Controls;
using AccessPath.Data.Models;
using AccessPath.Data.Repositories;

namespace AccessPath.UI.Views
{
    /// <summary>
    /// Interaction logic for AdminLoginView.xaml
    /// </summary>
    public partial class AdminLoginView : UserControl
    {
        private readonly UserRepository userRepository;

        public event EventHandler? LoginSucceeded;
        public AdminLoginView()
        {
            InitializeComponent();

            userRepository = new UserRepository();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();

            string password = PasswordInput.Password;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter your username.");

                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your password.");

                return;
            }

            User? user =
                userRepository.AuthenticateUser(username, password);

            if (user == null)
            {
                MessageBox.Show("Invalid administrator credentials.");

                PasswordInput.Clear();

                return;
            }

            if (!string.Equals(user.UserRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("This account does not have administrator access.");

                PasswordInput.Clear();

                return;
            }

            LoginSucceeded?.Invoke(this, EventArgs.Empty);
        }
    }
}
