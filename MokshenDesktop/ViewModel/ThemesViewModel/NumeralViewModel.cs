using CommunityToolkit.Mvvm.Input;
using MokshenDesktop.Resources.Commands;
using MokshenDesktop.Resources.Services;
using MokshenDesktop.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MokshenDesktop.ViewModel.ThemesViewModel
{
    class NumeralViewModel : ThemesViewModelBase
    {
        #region [TextBoxesContent]
        private string _textBoxContent1;
        public string TextBoxContent1
        {
            get => _textBoxContent1;
            set
            {
                _textBoxContent1 = value;
                OnPropertyChanged(nameof(_textBoxContent1));
            }
        }

        private string _textBoxContent2;
        public string TextBoxContent2
        {
            get => _textBoxContent2;
            set
            {
                _textBoxContent2 = value;
                OnPropertyChanged(nameof(_textBoxContent2));
            }
        }

        private string _textBoxContent3;
        public string TextBoxContent3
        {
            get => _textBoxContent3;
            set
            {
                _textBoxContent3 = value;
                OnPropertyChanged(nameof(_textBoxContent3));
            }
        }

        private string _textBoxContent4;
        public string TextBoxContent4
        {
            get => _textBoxContent4;
            set
            {
                _textBoxContent4 = value;
                OnPropertyChanged(nameof(_textBoxContent4));
            }
        }
        private string _textBoxContent5;
        public string TextBoxContent5
        {
            get => _textBoxContent5;
            set
            {
                _textBoxContent5 = value;
                OnPropertyChanged(nameof(_textBoxContent5));
            }
        }
        private string _textBoxContent6;
        public string TextBoxContent6
        {
            get => _textBoxContent6;
            set
            {
                _textBoxContent6 = value;
                OnPropertyChanged(nameof(_textBoxContent6));
            }
        }

        private string _textBoxContent7;
        public string TextBoxContent7
        {
            get => _textBoxContent7;
            set
            {
                _textBoxContent7 = value;
                OnPropertyChanged(nameof(_textBoxContent7));
            }
        }

        private string _textBoxContent8;
        public string TextBoxContent8
        {
            get => _textBoxContent8;
            set
            {
                _textBoxContent8 = value;
                OnPropertyChanged(nameof(_textBoxContent8));
            }
        }
        #endregion
        public ICommand NavigateFinishExerciseCommand { get; }
        public NumeralViewModel(Store store)
        {
            CategoryName = "Числительнай";
            SelectedAnswers = ["", "", "", "", "", "", "", ""];
            _store = store;
            GetExercisesAsync().ContinueWith(t => t, TaskScheduler.FromCurrentSynchronizationContext());
            NavigateFinishExerciseCommand = new RelayCommand(() =>
            {
                AddAnswers();
                if (SelectedAnswers.Any(string.IsNullOrEmpty))
                {
                    new CustomMessageBox("Какие-то поля - пусты").Show();
                    return;
                }
                NavigateFinishExercise(_store);
            });
            GoBackCommand = new NavigateCommand<ThemeViewModel>(store, () => new ThemeViewModel(store));
        }
        public void AddAnswers()
        {
            SelectedAnswers[0] = TextBoxContent1;
            SelectedAnswers[1] = TextBoxContent2;
            SelectedAnswers[2] = TextBoxContent3;
            SelectedAnswers[3] = TextBoxContent4;
            SelectedAnswers[4] = TextBoxContent5;
            SelectedAnswers[5] = TextBoxContent6;
            SelectedAnswers[6] = TextBoxContent7;
            SelectedAnswers[7] = TextBoxContent8;
        }
    }
}
