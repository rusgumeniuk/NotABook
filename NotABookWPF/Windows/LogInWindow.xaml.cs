using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

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
        public ViewModelBase ViewModel
        {
            get { return DataContext as ViewModelBase; }
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
            if (message == "unknown")
            {
                MessageBox.Show("Wrong username or password!");
            }
            else if (message == "user")
            {
                var window = new MainWindow();
                window.Show();
                this.Close();
            }
            else MessageBox.Show("IDK");
        }

        private void CheckBoxIsVisiblePassword_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void CheckBoxIsVisiblePassword_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
