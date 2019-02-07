using GalaSoft.MvvmLight.Messaging;

using NotABookLibraryStandart.Models.BookElements;

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

        public AddEditINoteWindow(AddEditNoteWindowViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            Messenger.Default.Register(this, new Action<string>(ProcessMessage));
        }

        public void ProcessMessage(string message)
        {
            if (message.Equals("NewCateg"))
                new AddEditBookElement(new AddEditBookElementViewModel(ViewModel.Service, new Category(String.Empty))).ShowDialog();
            else if (message.Equals("NoteAlreadyMarked"))
                MessageBox.Show("Note already marked by this category!");
            else if (message.Equals("WrongFile"))
                MessageBox.Show("Please select jpg or pgn file!");
            else if (message.Equals("NoteEditFinish"))
                this.Close();

        }
    }
}
