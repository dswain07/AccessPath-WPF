using System.Windows;
using System.Windows.Controls;
using AccessPath.UI.Services;

namespace AccessPath.UI.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void ApplyThemeButton_Click(object sender, RoutedEventArgs e)
        {
            if (ThemeComboBox.SelectedItem is not ComboBoxItem selectedItem)
            {
                MessageBox.Show("Please select a theme.");
                return;
            }

            string theme = selectedItem.Content.ToString()!;

            ThemeManager.ApplyTheme(theme);
        }
    }
}
