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
            if (curCategory != null)
            {
                curCategory.Title = entryTitle.Text;
            }
            else
            {
                Models.Category category = new Models.Category()
                {
                    Title = entryTitle.Text
                };
            }
            await Navigation.PopAsync(true);
        }

        //private void BtnTest_Clicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DisplayAlert("title", curCategory.Title, "OK");
        //    }
        //    catch(Exception ex)
        //    {
        //        DisplayAlert("title", ex.Message, "OK");
        //    }
            
        //}
    }
}