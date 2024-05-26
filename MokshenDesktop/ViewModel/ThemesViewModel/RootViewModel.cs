using MokshenDesktop.Resources.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using MokshenDesktop.Model;
using System.Windows.Media.Media3D;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Controls;
using MokshenDesktop.Resources.Commands;

namespace MokshenDesktop.ViewModel.ThemesViewModel
{
    class RootViewModel : ThemesViewModelBase
    {
        public ICommand NavigateFinishExerciseCommand { get; }
        public RootViewModel(Store store)
        {
            CategoryName = "Валъюр";
            _store = store;
            NavigateFinishExerciseCommand = new RelayCommand(() => NavigateFinishExercise(_store));
            GetExercisesAsync().ContinueWith(t => t, TaskScheduler.FromCurrentSynchronizationContext());
            GoBackCommand = new NavigateCommand<ThemeViewModel>(store, () => new ThemeViewModel(store));
        }
    }

}
