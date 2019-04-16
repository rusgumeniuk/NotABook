using GalaSoft.MvvmLight.Messaging;

using NotABookViewModels;

using System;
using System.Windows;

namespace NotABookWPF.Windows
{
    /// <summary>
    /// Interaction logic for AddEditBookElement.xaml
    /// </summary>
    public partial class AddEditBookElement : Window, IWindow
    {
        public ViewModelCustomBase ViewModel
        {
            get { return DataContext as ViewModelCustomBase; }
            set { DataContext = value; }
        }
        public AddEditBookElement(AddEditBookElementViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            Messenger.Default.Register(this, new Action<string>(ProcessMessage));
        }
        public void ProcessMessage(string message)
        {
            if (message.Equals("NotValidTitle"))
                MessageBox.Show("Please input valid Title");
            else if (message.Equals("BookElemChanged") || message.Equals("CancelBookElement"))
                this.Close();
        }
    }
}
