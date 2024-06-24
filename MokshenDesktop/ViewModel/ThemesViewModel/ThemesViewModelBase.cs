using CommunityToolkit.Mvvm.Input;
using MokshenDesktop.Model;
using MokshenDesktop.Resources.Commands;
using MokshenDesktop.Resources.Services;
using MokshenDesktop.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MokshenDesktop.ViewModel.ThemesViewModel
{
    abstract class ThemesViewModelBase : ViewModelBase
    {
        public string CategoryName;
        private ObservableCollection<Exercise> _exercises;
        public ObservableCollection<Exercise> Exercises
        {
            get => _exercises;
            set
            {
                _exercises = value;
                OnPropertyChanged(nameof(Exercises));
            }
        }
        private ObservableCollection<string> _selectedAnswers = [];
        public ObservableCollection<string> SelectedAnswers
        {
            get => _selectedAnswers;
            set
            {
                _selectedAnswers = value;
                OnPropertyChanged(nameof(SelectedAnswers));
            }
        }
        private string _percentage;
        public string Percentage
        {
            get => _percentage;
            set
            {
                _percentage = value;
                OnPropertyChanged(nameof(Percentage));
            }
        }
        public ICommand SelectAnswerCommand { get; }
        public ThemesViewModelBase()
        {
            SelectAnswerCommand = new RelayCommand<object>(SelectAnswer);
        }
        private void SelectAnswer(object? parameter)
        {
            var answer = parameter as Answer;
            if (answer != null)
            {
                if (answer.IsSelected)
                {
                    SelectedAnswers.Add(answer.Content);
                }
                else
                {
                    SelectedAnswers.Remove(answer.Content);
                }
            }
        }
        public void NavigateFinishExercise(Store store)
        {
            SendExercisesAsync().ContinueWith(t => new CustomMessageBox(Percentage).Show(), TaskScheduler.FromCurrentSynchronizationContext());
            store.CurrentViewModel = new ThemeViewModel(store);
        }
        public async Task GetExercises()
        {
            Exercises = await _store.Repository.GetExercisesByCategoryAsync(CategoryName);
            foreach (var exercise in Exercises)
            {
                exercise.GroupIndex = Exercises.IndexOf(exercise).ToString();
            }
        }
        public async Task CheckExercises()
        {
            Percentage = await _store.Repository.CheckExercises(SelectedAnswers, CategoryName).Result.ReadAsStringAsync();
        }
        public async Task GetExercisesAsync()
        {
            await Task.Run(() => GetExercises());
        }
        public async Task SendExercisesAsync()
        {
            await Task.Run(() => CheckExercises());
        }
    }
}
