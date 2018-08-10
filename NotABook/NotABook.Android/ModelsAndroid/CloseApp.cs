using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NotABook;
using Xamarin.Forms;

using NotABookLibraryStandart.Interfaces;

[assembly: Dependency(typeof(NotABook.Droid.ModelsAndroid.CloseApp))]
namespace NotABook.Droid.ModelsAndroid
{
    public class CloseApp : IClosingApp
    {        
        public void CloseApplication()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}