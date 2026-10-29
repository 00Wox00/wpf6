using System;
using System.Windows;

namespace WpfApp6
{
    public static class ThemeHelper
    {
        private static string[] themePaths =
        {
            "Styles/Colors/LightTheme.xaml",
            "Styles/Colors/DarkTheme.xaml"
        };

        public static string Current
        {
            get
            {
                string saved = Properties.Settings.Default.ThemePath;
                if (string.IsNullOrEmpty(saved))
                {
                    return themePaths[0];
                }
                return saved;
            }
            set
            {
                Properties.Settings.Default.ThemePath = value;
                Properties.Settings.Default.Save();
            }
        }

        public static void Apply(string themePath)
        {
            ResourceDictionary newTheme = new ResourceDictionary();
            newTheme.Source = new Uri(themePath, UriKind.Relative);
            
            ResourceDictionary oldTheme = null;
            foreach (ResourceDictionary dict in Application.Current.Resources.MergedDictionaries)
            {
                if (dict.Source == null)
                {
                    continue;
                }
                foreach (string path in themePaths)
                {
                    if (dict.Source.OriginalString.EndsWith(path, StringComparison.OrdinalIgnoreCase))
                    {
                        oldTheme = dict;
                    }
                }
            }

            if (oldTheme != null)
            {
                int index = Application.Current.Resources.MergedDictionaries.IndexOf(oldTheme);
                Application.Current.Resources.MergedDictionaries[index] = newTheme;
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Insert(0, newTheme);
            }

            Current = themePath;
        }

        public static void ApplySaved()
        {
            Apply(Current);
        }

        public static void Toggle()
        {
            if (Current == themePaths[0])
            {
                Apply(themePaths[1]);
            }
            else
            {
                Apply(themePaths[0]);
            }
        }
    }
}