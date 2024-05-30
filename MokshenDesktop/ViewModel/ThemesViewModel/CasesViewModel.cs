using MokshenDesktop.Resources.Commands;
using MokshenDesktop.Resources.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokshenDesktop.ViewModel.ThemesViewModel
{
    class CasesViewModel : ThemesViewModelBase
    {
        public CasesViewModel(Store store)
        {
            GoBackCommand = new NavigateCommand<ThemeViewModel>(store, () => new ThemeViewModel(store));
        }
    }
}
