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
            NotABookLibraryStandart.Models.Base.ProjectType = NotABookLibraryStandart.Models.ProjectType.WPF;

            var window = new Windows.MainWindow();
            window.Show();
        }
    }
}
