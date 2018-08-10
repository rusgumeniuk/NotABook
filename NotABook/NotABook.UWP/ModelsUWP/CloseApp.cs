using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml;

using NotABookLibraryStandart.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(NotABook.UWP.ModelsUWP.CloseApp))]
namespace NotABook.UWP.ModelsUWP
{
    public class CloseApp : IClosingApp
    {
        public void CloseApplication()
        {
            Application.Current.Exit();
        }
    }
}
