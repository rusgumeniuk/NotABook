using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotABook.Pages.DetailPages.HelpedPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEditCategoryPage : ContentPage
	{
        Models.Category curCategory = null;

		public AddEditCategoryPage ()
		{
			InitializeComponent ();
		}

        public AddEditCategoryPage(Models.Category category)
        {
            InitializeComponent();
            curCategory = category;
            this.BindingContext = category;                  
        }

        async private void BtnSave_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(entryTitle.Text))
            {
                await DisplayAlert("WTF", "Please, enter some name for category", "ok");
                return;
            }

            if (curCategory != null)
            {
                curCategory.Title = entryTitle.Text;
            }
            else
            {
                Models.Category category = new Models.Category(App.currentBook)
                {
                    Title = entryTitle.Text
                };
            }
            await Navigation.PopAsync();
            //try
            //{
                            
            //}
            //catch(IndexOutOfRangeException ex)
            //{
            //    await DisplayAlert("", ex.StackTrace, "OK");
            //    DependencyService.Get<Interfaces.IClosingApp>()?.CloseApplication();
            //    //await DisplayAlert("", ex.Message, "OK");
            //}
            //catch(Exception ex)
            //{
            //    await DisplayAlert("", ex.Message, "OK");
            //}
            
        }
    }
}