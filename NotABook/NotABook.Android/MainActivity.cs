using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.IO;

namespace NotABook.Droid
{
    [Activity(Label = "NotABook", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            //AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            //TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
        }

        //#region Error handling
        //private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        //{
        //    var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);
        //    LogUnhandledException(newExc);
        //}

        //private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        //{
        //    var newExc = new Exception("CurrentDomainOnUnhandledException", unhandledExceptionEventArgs.ExceptionObject as Exception);
        //    LogUnhandledException(newExc);
        //}

        //internal static void LogUnhandledException(Exception exception)
        //{
        //    try
        //    {
        //        const string errorFileName = "Fatal.log";
        //        var libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // iOS: Environment.SpecialFolder.Resources
        //        var errorFilePath = Path.Combine(libraryPath, errorFileName);
        //        var errorMessage = String.Format("Time: {0}\r\nError: Unhandled Exception\r\n{1}",
        //        DateTime.Now, exception.ToString());
        //        File.WriteAllText(errorFilePath, errorMessage);

        //        // Log to Android Device Logging.
        //        Android.Util.Log.Error("Crash Report", errorMessage);
        //    }
        //    catch
        //    {
        //        // just suppress any error logging exceptions
        //    }
        //}
        //#endregion
    }
}

