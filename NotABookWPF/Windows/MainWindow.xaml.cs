using GalaSoft.MvvmLight.Messaging;

using NotABookViewModels;

using System;
using System.Security.Permissions;
using System.Windows;

namespace NotABookWPF.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [PrincipalPermission(SecurityAction.Demand, Role = "Administrators")]
    [PrincipalPermission(SecurityAction.Demand, Role = "Users")]
    public partial class MainWindow : Window, IWindow
    {
        public ViewModelCustomBase ViewModel
        {
            get { return DataContext as ViewModelCustomBase; }
            set { DataContext = value; }
        }
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            var newFrame = new System.Windows.Controls.Frame();
            newFrame.Navigate(new NotePage(new NotePageViewModel(ViewModel.Service,
            (ViewModel as MainWindowViewModel).CurrentBook,
            (ViewModel as MainWindowViewModel).CurrentNote)));

            (ViewModel as MainWindowViewModel).NoteFrame = newFrame;

            //= new NotePage(new NotePageViewModel(ViewModel.Service,
            //(ViewModel as MainWindowViewModel).CurrentBook,
            //(ViewModel as MainWindowViewModel).CurrentNote));
            Messenger.Default.Register(this, new Action<string>(ProcessMessage));
        }

        public void ProcessMessage(string message)
        {
            if (message.Equals("AddEditNote"))
            {
                new AddEditINoteWindow(
                    new AddEditNoteWindowViewModel(
                        ViewModel.Service,
                        (ViewModel as MainWindowViewModel).CurrentBook,
                        (ViewModel as MainWindowViewModel).CurrentNote)
                    ).Show();
            }
            else if (message.Equals("AddEditCategory"))
            {
                new AddEditBookElement(
                                   new AddEditBookElementViewModel(
                                       ViewModel.Service,
                                       (ViewModel as MainWindowViewModel).SelectedCategory
                                       )).Show();
            }
            else if (message.Equals("AddEditBook"))
            {
                new AddEditBookElement(
                   new AddEditBookElementViewModel(
                       ViewModel.Service,
                       (ViewModel as MainWindowViewModel).CurrentBook
                       )).Show();
            }
            else if (message.Equals("AboutPage"))
                MessageBox.Show("Hello! \n I Ruslan Humeniuk.\n I am KPI student and this is my first WPF project");
            else if (message.Equals("NoteAlreadyMarked"))
                MessageBox.Show("Note already marked by this category!");
            else if (message.Equals("WrongFile"))
                MessageBox.Show("Please select jpg or pgn file!");
            else if (message.Equals("UpdateMain"))
                this.InitializeComponent();
            else if (message.Equals("UpdateNoteFrame"))
            {
                var newFrame = new System.Windows.Controls.Frame();
                newFrame.Navigate(new NotePage(new NotePageViewModel(
                    ViewModel.Service,
                    (ViewModel as MainWindowViewModel).CurrentBook,
                    (ViewModel as MainWindowViewModel).CurrentNote
                    )));
                (ViewModel as MainWindowViewModel).NoteFrame = newFrame;
            }
        }
    }
}