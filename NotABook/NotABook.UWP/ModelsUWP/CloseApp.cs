using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

[assembly: Xamarin.Forms.Dependency(typeof(NotABook.UWP.ModelsUWP.CloseApp))]
namespace NotABook.UWP.ModelsUWP
{
    public class CloseApp : Interfaces.IClosingApp
    {
        public void CloseApplication()
        {
            Application.Current.Exit();
        }
    }
}
