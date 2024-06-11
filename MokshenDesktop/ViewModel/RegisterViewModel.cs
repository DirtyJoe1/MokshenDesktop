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

namespace MokshenDesktop.ViewModel
{
    public class RegisterViewModel: ViewModelBase
    {
        private string _username;
        public string RUsername
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(RUsername));
            }
        }
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private string _password;
        public string RPassword
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(RPassword));
            }
        }
        private string _passwordConfirm;
        public string PasswordConfirm
        {
            get => _passwordConfirm;
            set
            {
                _passwordConfirm = value;
                OnPropertyChanged(nameof(PasswordConfirm));
            }
        }
        public ICommand NavigateFinishRegisterCommand { get;}
        public ICommand NavigateLoginCommand { get;}
        public ICommand SwitchLanguageCommand { get; }
        public RegisterViewModel(Store store)
        {
            _store = store;
            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(store, () => new LoginViewModel(store));
            NavigateFinishRegisterCommand = new RelayCommand(() => NavigateFinishRegister(_store));
            SwitchLanguageCommand = new RelayCommand(SwitchLanguage);
        }
        private async void NavigateFinishRegister(Store store)
        {
            if (RPassword == PasswordConfirm)
            {
                var registration = new UserRegistration
                {
                    Username = RUsername,
                    Password = RPassword,
                    Email = Email,
                    Role = "Student"
                };
                HttpResponseMessage response = await store.Repository.PostAuthenticationRegister(registration);
                if (response.IsSuccessStatusCode)
                {
                    store.CurrentViewModel = new LoginViewModel(store);
                    MessageBox.Show(response.StatusCode.ToString());
                }
                else
                {
                    MessageBox.Show(response.StatusCode.ToString());
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
            }
        }
    }
}
