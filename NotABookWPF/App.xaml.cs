using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NotABookWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
       public App()
        {
            NotABookLibraryStandart.Models.BaseClass.IsTesingProjectRunning = false;

            var window = new Windows.MainWindow();
            window.Show();
        }
    }
}
