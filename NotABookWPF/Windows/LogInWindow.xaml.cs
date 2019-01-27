using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

using NotABookLibraryStandart.Models.Roles;

using NotABookViewModels;

using System;
using System.Windows;

namespace NotABookWPF.Windows
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window, IWindow
    {
        public ViewModelCustomBase ViewModel
        {
            get { return DataContext as ViewModelCustomBase; }
            set { DataContext = value; }
        }
        public LogInWindow(LogInWindowViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            Messenger.Default.Register(this, new Action<string>(ProcessMessage));
        }

        public void ProcessMessage(string message)
        {
            if (message == "logged")
            {
                var window = new MainWindow();
                window.Show();
                this.Close();
            }
            else if (message.Equals("SignUp"))
            {
                var window = new SignUpWindow(new SignUpWindowViewModel(ViewModel.Service));
                window.ShowDialog();
            }
            else if (message.Contains("Error"))
                MessageBox.Show(message);
        }

        private void CheckBoxIsVisiblePassword_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void CheckBoxIsVisiblePassword_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
