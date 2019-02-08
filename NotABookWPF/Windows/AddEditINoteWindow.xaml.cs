using GalaSoft.MvvmLight.Messaging;

using NotABookViewModels;

using System;
using System.Windows;

namespace NotABookWPF.Windows
{
    /// <summary>
    /// Interaction logic for AddEditItemWindow.xaml
    /// </summary>
    public partial class AddEditINoteWindow : Window, IWindow
    {
        public ViewModelCustomBase ViewModel
        {
            get { return DataContext as ViewModelCustomBase; }
            set { DataContext = value; }
        }

        public AddEditINoteWindow(NotePageViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            NoteFrame.Navigate(new NotePage(ViewModel as NotePageViewModel));
            Messenger.Default.Register(this, new Action<string>(ProcessMessage));
        }

        public void ProcessMessage(string message)
        {
            if (message.Equals("NoteEditFinish"))
                this.Close();
        }
    }
}
