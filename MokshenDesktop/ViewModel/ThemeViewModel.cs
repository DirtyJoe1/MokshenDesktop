using CommunityToolkit.Mvvm.Input;
using MokshenDesktop.Resources.Commands;
using MokshenDesktop.Resources.Services;
using MokshenDesktop.ViewModel.ThemesViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MokshenDesktop.ViewModel
{
    public class ThemeViewModel : ViewModelBase
    {
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public ICommand NavigateRootCommand { get; }
        public ThemeViewModel(Store store)
        {
            _store = store;
            Username = TokenStorage.Username;
            NavigateRootCommand = new NavigateCommand<RootViewModel>(store, () => new RootViewModel(store));
            GoBackCommand = new NavigateCommand<LoginViewModel>(store, () => new LoginViewModel(store));
        }
    }
}
