using CommunityToolkit.Mvvm.Input;
using MokshenDesktop.Model;
using MokshenDesktop.Resources.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MokshenDesktop.ViewModel
{
    public class ExerciseCreationViewModel : ViewModelBase
    {
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
        private Exercise _selectedExercise;
        public Exercise SelectedExercise
        {
            get => _selectedExercise;
            set
            {
                _selectedExercise = value;
                OnPropertyChanged(nameof(SelectedExercise));
            }
        }
        private ObservableCollection<Answer> _answersForCreation = [];
        public ObservableCollection<Answer> AnswersForCreation
        {
            get => _answersForCreation;
            set
            {
                _answersForCreation = value;
                OnPropertyChanged(nameof(AnswersForCreation));
            }
        }
        private string _categoryForCreation;
        public string CategoryForCreation
        {
            get => _categoryForCreation;
            set
            {
                _categoryForCreation = value;
                OnPropertyChanged(nameof(CategoryForCreation));
            }
        }
        private string _trueAnswerForCreation;
        public string TrueAnswerForCreation
        {
            get => _trueAnswerForCreation;
            set
            {
                _trueAnswerForCreation = value;
                OnPropertyChanged(nameof(TrueAnswerForCreation));
            }
        }
        private bool _isEditingMode;
        public bool IsEditingMode
        {
            get => _isEditingMode;
            set
            {
                _isEditingMode = value;
                OnPropertyChanged(nameof(IsEditingMode));
            }
        }
        public ICommand DeleteSelectedExerciseCommand {  get; }
        public ICommand AddRowCommand {  get; }
        public ICommand RemoveRowCommand { get; }
        public ICommand CreateOrEditExerciseCommand { get; }
        public ICommand ToggleModeCommand {  get; }

        public ExerciseCreationViewModel(Store store)
        {
            _store = store;
            GetExercisesAsync().ContinueWith(t => t, TaskScheduler.FromCurrentSynchronizationContext());
            DeleteSelectedExerciseCommand = new RelayCommand(() => DelExercise(SelectedExercise));
            CreateOrEditExerciseCommand = new RelayCommand(CreateOrEditExercise);
            AddRowCommand = new RelayCommand(() =>
            {
                AnswersForCreation.Add(new Answer { Content = "Введите содержимое ответа" });
            });
            RemoveRowCommand = new RelayCommand<Answer>((Answer answer) =>
            {
                try
                {
                    AnswersForCreation.Remove(answer);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
            ToggleModeCommand = new RelayCommand(()=>
            {
                IsEditingMode = !IsEditingMode;
                if (!IsEditingMode)
                {
                    ClearFields();
                }
            });

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(SelectedExercise))
                {
                    UpdateFieldsFromSelectedExercise();
                }
            };
        }
        public void CreateOrEditExercise()
        {
            if (IsEditingMode)
            {
                EditExercise();
            }
            else
            {
                CreateExercise();
            }
        }
        public async void CreateExercise()
        {
            var exercise = new Exercise
            {
                Answers = AnswersForCreation.ToList(),
                TrueAnswer = TrueAnswerForCreation,
                Category = CategoryForCreation
            };
            try
            {
                var response = await _store.Repository.CreateExercise(exercise);
                if (response.IsSuccessStatusCode)
                {
                    await GetExercisesAsync();
                    MessageBox.Show("Успешно созданно");
                }
                else
                {
                    MessageBox.Show(response.StatusCode.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public async void EditExercise()
        {
            var exercise = new Exercise
            {
                Id = SelectedExercise.Id,
                Answers = AnswersForCreation.ToList(),
                TrueAnswer = TrueAnswerForCreation,
                Category = CategoryForCreation
            };
            try
            {
                var response = await _store.Repository.EditExercise(exercise);
                if (response.IsSuccessStatusCode)
                {
                    await GetExercisesAsync();
                    MessageBox.Show("Успешно отредактировано");
                }
                else
                {
                    MessageBox.Show(response.StatusCode.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateFieldsFromSelectedExercise()
        {
            if (SelectedExercise != null)
            {
                CategoryForCreation = SelectedExercise.Category;
                TrueAnswerForCreation = SelectedExercise.TrueAnswer;
                AnswersForCreation.Clear();
                foreach (var answer in SelectedExercise.Answers)
                {
                    AnswersForCreation.Add(new Answer { Content = answer.Content });
                }
                IsEditingMode = true;
            }
            else
            {
                ClearFields();
                IsEditingMode = false;
            }
        }

        private void ClearFields()
        {
            CategoryForCreation = "";
            TrueAnswerForCreation = "";
            AnswersForCreation.Clear();
        }
        public async void DelExercise(Exercise SelectedExercise)
        {
            try
            {
                var response = await _store.Repository.DeleteExercise(SelectedExercise.Id);
                if (response.IsSuccessStatusCode)
                {
                    await GetExercisesAsync();
                    MessageBox.Show("Успешно удалено");
                }
                else
                {
                    MessageBox.Show(response.StatusCode.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public async Task GetExercisesAsync()
        {
            Exercises = await _store.Repository.GetExercisesAsync();
        }
    }
}
