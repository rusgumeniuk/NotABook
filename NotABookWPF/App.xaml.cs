using NotABookLibraryStandart.Models.Roles;
using NotABookViewModels;
using System;
using System.Windows;

namespace NotABookWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {      
        protected override void OnStartup(StartupEventArgs e)
        {
            NotABookLibraryStandart.Models.Base.ProjectType = NotABookLibraryStandart.Models.TypeOfRunningProject.WPF;
            Principal principal = new Principal();
            AppDomain.CurrentDomain.SetThreadPrincipal(principal);

            base.OnStartup(e);

            LogInWindowViewModel viewModel = new LogInWindowViewModel(new AuthenticationService());           
            var window = new Windows.LogInWindow(viewModel);
            window.Show();
        }
    }
}
