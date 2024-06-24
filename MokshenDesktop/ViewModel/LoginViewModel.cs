using CommunityToolkit.Mvvm.Input;
using MokshenDesktop.Model;
using MokshenDesktop.Resources.Commands;
using MokshenDesktop.Resources.Services;
using MokshenDesktop.View;
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
                bool isEmpty = ValidationService.IsEmpty(LUsername);
                LoginValidationViolation = !isEmpty && !ValidationService.ValidateLogin(LUsername);
                OnPropertyChanged(nameof(LoginValidationViolation));
                LoginIsEmpty = isEmpty;
                OnPropertyChanged(nameof(LoginIsEmpty));
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
                bool isEmpty = ValidationService.IsEmpty(LPassword);
                PasswordValidationViolation = !isEmpty && !ValidationService.ValidatePassword(LPassword);
                OnPropertyChanged(nameof(PasswordValidationViolation));
                PasswordIsEmpty = isEmpty;
                OnPropertyChanged(nameof(PasswordIsEmpty));
            }
        }

        private bool _loginValidationViolation;
        public bool LoginValidationViolation
        {
            get => _loginValidationViolation;
            set
            {
                _loginValidationViolation = value;
                OnPropertyChanged(nameof(_loginValidationViolation));
            }
        }
        private bool _loginIsEmpty;
        public bool LoginIsEmpty
        {
            get => _loginIsEmpty;
            set
            {
                _loginIsEmpty = value;
                OnPropertyChanged(nameof(_loginIsEmpty));
            }
        }

        private bool _passwordValidationViolation;
        public bool PasswordValidationViolation
        {
            get => _passwordValidationViolation;
            set
            {
                _passwordValidationViolation = value;
                OnPropertyChanged(nameof(_passwordValidationViolation));
            }
        }
        private bool _passwordIsEmpty;
        public bool PasswordIsEmpty
        {
            get => _passwordIsEmpty;
            set
            {
                _passwordIsEmpty = value;
                OnPropertyChanged(nameof(_passwordIsEmpty));
            }
        }
        public ICommand NavigateRegisterCommand { get; }
        public ICommand NavigateThemesCommand { get; }
        public ICommand TestVisibilityCommand { get; }

        public LoginViewModel(Store store) 
        {
            _store = store;
            NavigateRegisterCommand = new NavigateCommand<RegisterViewModel>(store, () => new RegisterViewModel(store));
            NavigateThemesCommand = new RelayCommand(() => NavigateThemes(_store));
        }
        public async void NavigateThemes(Store store)
        {
            if (ValidationService.IsAnyEmpty(LUsername, LPassword))
            {
                string message = "Обнаружены ошибки заполнения при входе";
                new CustomMessageBox(message).Show();
                return;
            }
            var login = new UserLogin
            {
                Username = LUsername,
                Password = LPassword
            };
            HttpResponseMessage response = await store.Repository.PostAuthenticationLogin(login);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)  
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
            else if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                new CustomMessageBox("Пользователь с таким сочетанием логина и пароля не найден").Show();
            }
        }
        
    }
}
