using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using MokshenDesktop.Resources.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokshenDesktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel => _store.CurrentViewModel;
        public MainViewModel(Store navigationStore)
        {
            _store = navigationStore;
            _store.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
