using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotABook.Pages.MainPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartPage : MasterDetailPage
	{
		public StartPage ()
		{
			InitializeComponent ();
            masterPage.listView.ItemSelected += OnItemSelected;
            masterPage.downList.ItemSelected += DownList_ItemSelected;

            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }            
        }

        private void DownList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Pages.MainPages.MasterPageItem item)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.downList.SelectedItem = null;
                IsPresented = false;
            }
        }

        private void BtnExit_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<Interfaces.IClosingApp>()?.CloseApplication();
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Pages.MainPages.MasterPageItem item)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.listView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}