using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotABookLibraryStandart.Models;
using NotABookLibraryStandart.Models.BookElements;

namespace NotABook.Pages.DetailPages.HelpedPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEditCategoryPage : ContentPage
	{
        Category curCategory = null;

		public AddEditCategoryPage ()
		{
			InitializeComponent ();
		}

        public AddEditCategoryPage(Category category)
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
                await DisplayAlert("Non implemented", "Error", "ok");
                //Category category = new Category()
                //{
                //    Title = entryTitle.Text
                //};
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