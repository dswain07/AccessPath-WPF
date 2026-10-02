using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AccessPath.UI.Services;

namespace AccessPath.UI
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
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
