using CommunityToolkit.Mvvm.Input;
using MokshenDesktop.Model;
using MokshenDesktop.Resources.Commands;
using MokshenDesktop.Resources.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace MokshenDesktop.ViewModel
{
    public class LoginViewModel: ViewModelBase
    {
        private string _username;
        public string LUsername
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(LUsername));
            }
        }

        private string _password;
        public string LPassword
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(LPassword));
            }
        }
        public ICommand NavigateRegisterCommand { get; }
        public ICommand NavigateThemesCommand { get; }
        public LoginViewModel(Store store) 
        {
            _store = store;
            NavigateRegisterCommand = new NavigateCommand<RegisterViewModel>(store, () => new RegisterViewModel(store));
            NavigateThemesCommand = new RelayCommand(() => NavigateThemes(_store));
        }
        public async void NavigateThemes(Store store)
        {
            var login = new UserLogin
            {
                Username = LUsername,
                Password = LPassword
            };
            HttpResponseMessage response = await store.Repository.PostAuthenticationLogin(login);
            if (response.IsSuccessStatusCode)  
            {
                TokenStorage.Username = login.Username;
                if (TokenStorage.Role == "Admin")
                {
                    store.CurrentViewModel = new ExerciseCreationViewModel(store);
                }
                else
                {
                    store.CurrentViewModel = new ThemeViewModel(store);
                }
            }
            else
            {
                MessageBox.Show(await response.Content.ReadAsStringAsync());
            }
        }
        
    }
}
