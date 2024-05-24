using Microsoft.Extensions.DependencyInjection;
using MokshenDesktop.Resources.Services;
using MokshenDesktop.View.Windows;
using MokshenDesktop.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Navigation;

namespace MokshenDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Store navigationStore = new Store();
            navigationStore.CurrentViewModel = new LoginViewModel(navigationStore);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
