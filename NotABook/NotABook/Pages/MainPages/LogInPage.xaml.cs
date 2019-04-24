using GalaSoft.MvvmLight.Messaging;
using NotABookViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotABook.Pages.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage, IWindow
    {
        public ViewModelCustomBase ViewModel
        {
            get { return BindingContext as ViewModelCustomBase; }
            set { BindingContext = value; }
        }
        public LogInPage(LogInWindowViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            Messenger.Default.Register(this, new Action<string>(ProcessMessage));

        }        

        public void ProcessMessage(string message)
        {
            
        }
    }
}