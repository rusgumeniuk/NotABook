using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotABook.Pages.DetailPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllBooksPage : ContentPage
	{
		public AllBooksPage ()
		{
			InitializeComponent ();
		}

        private void BtnCount_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Count", NotABook.App.Books.Count.ToString(), "hmmm");
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //if(e.SelectedItem is Models.Book book)
            //{
            //    if(book != NotABook.App.currentBook)
            //    {
            //        NotABook.App.currentBook = book;
            //        Navigation.PushAsync(new ItemsOfBookPage());
            //    }
            //}
        }

        public async void BookList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Models.Book selectedBook)
            {
                App.currentBook = selectedBook;
                //ItemsOfBookPage page = new ItemsOfBookPage(selectedBook);
                await Navigation.PushAsync(new ItemsOfBookPage(selectedBook));
            }
        }
    }
}