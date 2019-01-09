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
using System.Windows.Shapes;

namespace NotABookWPF.Windows
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void CheckBoxIsVisiblePassword_Checked(object sender, RoutedEventArgs e)
        {
            PasswordBoxPass.OpacityMask = null;
        }

        private void CheckBoxIsVisiblePassword_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
