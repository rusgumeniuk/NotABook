using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using NotABookLibraryStandart.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(NotABook.iOS.ModelsIos.CloseApp))]
namespace NotABook.iOS.ModelsIos
{
    public class CloseApp : IClosingApp
    {
        public void CloseApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}