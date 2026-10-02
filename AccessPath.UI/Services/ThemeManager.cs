using System;
using System.Windows;
using System.Collections.Generic;
using System.Text;

namespace AccessPath.UI.Services
{
    public class ThemeManager
    {
        public static void ApplyTheme(string theme)
        {
            string themeFile;

            if (theme == "Dark")
            {
                themeFile = "Themes/DarkTheme.xaml";
            }
            else
            {
                themeFile = "Themes/LightTheme.xaml";
            }

            ResourceDictionary newTheme = new ResourceDictionary();

            newTheme.Source = new Uri(themeFile, UriKind.Relative);

            Application.Current.Resources.MergedDictionaries.Clear();

            Application.Current.Resources.MergedDictionaries.Add(newTheme);
        }
    }
}
