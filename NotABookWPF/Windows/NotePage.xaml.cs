using GalaSoft.MvvmLight.Messaging;

using NotABookLibraryStandart.Models.BookElements;

using NotABookViewModels;

using System;
using System.Windows;
using System.Windows.Controls;

namespace NotABookWPF.Windows
{
    /// <summary>
    /// Interaction logic for NotePage.xaml
    /// </summary>
    public partial class NotePage : Page, IWindow
    {
        public ViewModelCustomBase ViewModel
        {
            get { return DataContext as ViewModelCustomBase; }
            set { DataContext = value; }
        }
        public NotePage(NotePageViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            Messenger.Default.Register(this, new Action<string>(ProcessMessage));
            this.ShowsNavigationUI = false;
        }
        public void ProcessMessage(string message)
        {
            if (message.Equals("NoteAlreadyMarked"))
                MessageBox.Show("Note already marked by this category!");
            else if (message.Equals("WrongFile"))
                MessageBox.Show("Please select jpg or pgn file!");
            else if (message.Equals("NewCategory"))
                new AddEditBookElement(new AddEditBookElementViewModel(ViewModel.Service, new Category(String.Empty))).ShowDialog();
        }
    }
}
