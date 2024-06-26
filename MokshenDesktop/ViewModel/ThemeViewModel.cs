﻿using CommunityToolkit.Mvvm.Input;
using MokshenDesktop.Resources.Commands;
using MokshenDesktop.Resources.Services;
using MokshenDesktop.View;
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
        public ICommand NavigateCasesCommand { get; }
        public ICommand NavigateNumeralCommand { get; }
        public ICommand NavigateVerbCommand { get; }
        public ICommand InDevelepmentCommand { get; }


        public ThemeViewModel(Store store)
        {
            _store = store;
            Username = TokenStorage.Username;
            NavigateRootCommand = new NavigateCommand<RootViewModel>(store, () => new RootViewModel(store));
            NavigateCasesCommand = new NavigateCommand<CasesViewModel>(store, () => new CasesViewModel(store));
            NavigateNumeralCommand = new NavigateCommand<NumeralViewModel>(store, () => new NumeralViewModel(store));
            NavigateVerbCommand = new NavigateCommand<VerbViewModel>(store, () => new VerbViewModel(store));
            GoBackCommand = new NavigateCommand<LoginViewModel>(store, () => new LoginViewModel(store));
            InDevelepmentCommand = new RelayCommand(() => new CustomMessageBox("В разработке").Show()) ;
        }
    }
}
