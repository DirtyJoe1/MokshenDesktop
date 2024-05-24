using MokshenDesktop.Resources.Commands;
using MokshenDesktop.Resources.Services;
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
        public ICommand NavigateRootCommand { get; }
        public ThemeViewModel(Store store)
        {
            _store = store;
            NavigateRootCommand = new NavigateCommand<RootViewModel>(store, () => new RootViewModel(store));
        }
    }
}
