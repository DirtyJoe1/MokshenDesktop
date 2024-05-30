using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using MokshenDesktop.Resources.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MokshenDesktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel => _store.CurrentViewModel;
        public ICommand SwitchLanguageCommand { get; }
        public ICommand SwitchThemeCommand { get; }
        public ICommand ExitCommand { get; }
        public MainViewModel(Store navigationStore)
        {
            _store = navigationStore;
            _store.CurrentViewModelChanged += OnCurrentViewModelChanged;
            SwitchLanguageCommand = new RelayCommand(SwitchLanguage);
            SwitchThemeCommand = new RelayCommand(SwitchTheme);
            ExitCommand = new RelayCommand(Exit);
        }
        public void Exit()
        {
            if(_store.CurrentViewModel is LoginViewModel)
            {
                Application.Current.Shutdown();
            }
            else
            {
                _store.CurrentViewModel = new LoginViewModel(_store);
            }
        }
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
