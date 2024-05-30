using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MokshenDesktop.Resources.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MokshenDesktop.ViewModel
{
    public class ViewModelBase : ObservableObject
    {
        public Store _store;
        private bool _isCurrentLanguageIsRussian = false;
        private bool _isCurrentThemeIsLight = true;
        public ICommand GoBackCommand { get; set; }
        public bool IsCurrentLanguageIsRussian
        {
            get => _isCurrentLanguageIsRussian;
            set
            {
                _isCurrentLanguageIsRussian = value;
                OnPropertyChanged(nameof(IsCurrentLanguageIsRussian));
            }
        }
        public bool IsCurrentThemeIsLight
        {
            get => _isCurrentThemeIsLight;
            set
            {
                _isCurrentThemeIsLight = value;
                OnPropertyChanged(nameof(IsCurrentThemeIsLight));
            }
        }
        public void SwitchLanguage()
        {
            if (IsCurrentLanguageIsRussian)
            {
                IsCurrentLanguageIsRussian = false;
                foreach (var dict in Application.Current.Resources.MergedDictionaries.ToList())
                {
                    if (dict.Source == new Uri("/Resources/Localization/MokshenLanguage.xaml", UriKind.Relative))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(dict);
                        break;
                    }
                }
                var mokshenResourceDictionary = new ResourceDictionary { Source = new Uri("/Resources/Localization/MokshenLanguage.xaml", UriKind.Relative) };
                Application.Current.Resources.MergedDictionaries.Add(mokshenResourceDictionary);
            }
            else
            {
                IsCurrentLanguageIsRussian = true;
                foreach (var dict in Application.Current.Resources.MergedDictionaries.ToList())
                {
                    if (dict.Source == new Uri("/Resources/Localization/RussianLanguage.xaml", UriKind.Relative))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(dict);
                        break;
                    }
                }
                var russianResourceDictionary = new ResourceDictionary { Source = new Uri("/Resources/Localization/RussianLanguage.xaml", UriKind.Relative) };
                Application.Current.Resources.MergedDictionaries.Add(russianResourceDictionary);
            }
        }
        public void SwitchTheme()
        {
            if (IsCurrentThemeIsLight)
            {
                IsCurrentThemeIsLight = false;
                foreach (var dict in Application.Current.Resources.MergedDictionaries.ToList())
                {
                    if (dict.Source == new Uri("/Resources/Styles/LightMode.xaml", UriKind.Relative))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(dict);
                        break;
                    }
                }
                var darkModeResourceDictionary = new ResourceDictionary { Source = new Uri("/Resources/Styles/DarkMode.xaml", UriKind.Relative) };
                Application.Current.Resources.MergedDictionaries.Add(darkModeResourceDictionary);
            }
            else
            {
                IsCurrentThemeIsLight = true;
                foreach (var dict in Application.Current.Resources.MergedDictionaries.ToList())
                {
                    if (dict.Source == new Uri("/Resources/Styles/DarkMode.xaml", UriKind.Relative))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(dict);
                        break;
                    }
                }
                var lightModeResourceDictionary = new ResourceDictionary { Source = new Uri("/Resources/Styles/LightMode.xaml", UriKind.Relative) };
                Application.Current.Resources.MergedDictionaries.Add(lightModeResourceDictionary);
            }
        }
    }
}
