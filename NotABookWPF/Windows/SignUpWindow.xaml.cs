using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

using NotABookViewModels;

using System;
using System.Windows;

namespace NotABookWPF.Windows
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window, IWindow
    {
        public ViewModelBase ViewModel
        {
            get => DataContext as ViewModelCustomBase;
            set => DataContext = value;
        }
        public SignUpWindow(SignUpWindowViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            Messenger.Default.Register(this, new Action<string>(ProcessMessage));
        }
        public void ProcessMessage(string message)
        {
            if (message.Equals("registered"))
            {
                MessageBox.Show("Congratulations with success sign up!");
                this.Close();
            }
        }
    }
}
