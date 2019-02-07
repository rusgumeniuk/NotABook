using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using NotABookLibraryStandart.Models.BookElements;
using NotABookViewModels;

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
            if (message.Equals("NewCateg"))
                new AddEditBookElement(new AddEditBookElementViewModel(ViewModel.Service, new Category(String.Empty))).ShowDialog();
            else if (message.Equals("NoteAlreadyMarked"))
                MessageBox.Show("Note already marked by this category!");
            else if (message.Equals("WrongFile"))
                MessageBox.Show("Please select jpg or pgn file!");
            //else if (message.Equals("NoteEditFinish"))
                //this.Close();
        }
    }
}
