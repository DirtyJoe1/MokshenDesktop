using MokshenDesktop.Resources.Services;
using MokshenDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MokshenDesktop.Resources.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly Store _store;
        private readonly Func<TViewModel> _createViewModel;

        public NavigateCommand(Store store, Func<TViewModel> createViewModel)
        {
            _store = store;
            _createViewModel = createViewModel;
        }

        public override void Execute(object parameter)
        {
            _store.CurrentViewModel = _createViewModel();
        }
    }
}
