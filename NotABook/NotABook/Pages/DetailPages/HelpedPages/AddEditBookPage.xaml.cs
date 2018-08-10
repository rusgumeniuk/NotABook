using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotABookLibraryStandart.Models;

namespace NotABook.Pages.DetailPages.HelpedPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEditBookPage : ContentPage
	{
        Book curBook = null;

		public AddEditBookPage ()
		{
			InitializeComponent ();
		}

        public AddEditBookPage(Book book)
        {
            InitializeComponent();
            curBook = book;
            this.BindingContext = curBook;
        }

        async private void BtnSave_Clicked(object sender, EventArgs e)
        {
            if (!IsRequiredFieldsIsFillIn())
            {
                await DisplayAlert("Error", "This item want to has Title", "ok");
                return;
            }

            if (curBook != null)
            {
                curBook.Title = entryTitle.Text;
            }
            else
            {
                Book book = new Book()
                {
                    Title = entryTitle.Text
                };
            }
            await Navigation.PopAsync(true);
        }

        private bool IsRequiredFieldsIsFillIn()
        {
            return !String.IsNullOrWhiteSpace(entryTitle.Text);
        }
    }
}