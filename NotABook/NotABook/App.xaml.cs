using NotABookDataAccess;

using NotABookLibraryStandart.DB;
using NotABookLibraryStandart.Models;
using NotABookLibraryStandart.Models.BookElements;
using NotABookViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NotABook
{
    public partial class App : Application
    {
        public static Book currentBook = null;
        public App()
        {
            Base.ProjectType = TypeOfRunningProject.Xamarin;
            InitializeComponent();            

            LogInWindowViewModel viewModel = new LogInWindowViewModel(new Service(new Repository(new DataBaseContext())));
            var window = new Pages.MainPages.LogInPage(viewModel);
        }

        #region StandartFunctions
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion
    }
}
