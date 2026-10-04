using System.Windows;
using System.Windows.Controls;
using AccessPath.Data.Models;
using AccessPath.Data.Repositories;
using AccessPath.UI.Views;

namespace AccessPath.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Display Buildings when AccessPath starts.
            MainContent.Content = new BuildingsView();
        }

        private void BuildingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new BuildingsView();
        }

        private void RoutesButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new RoutesView();
        }

        private void AccessibilityButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new AccessibilityView();
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            AdminLoginView loginView = new AdminLoginView();

            loginView.LoginSucceeded += AdminLoginView_LoginSucceeded;

            MainContent.Content = loginView;
        }

        private void AdminLoginView_LoginSucceeded(object? sender, EventArgs e)
        {
            MainContent.Content = new AdministrationView();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new SettingsView();
        }
    }
}