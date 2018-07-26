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

        //private void BtnCount_Clicked(object sender, EventArgs e)
        //{
        //    DisplayAlert("Count", NotABook.App.Books.Count.ToString(), "hmmm");
        //}

        //void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    //if(e.SelectedItem is Models.Book book)
        //    //{
        //    //    if(book != NotABook.App.currentBook)
        //    //    {
        //    //        NotABook.App.currentBook = book;
        //    //        Navigation.PushAsync(new ItemsOfBookPage());
        //    //    }
        //    //}
        //}

        public async void BookList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Models.Book selectedBook)
            {
                App.currentBook = selectedBook;                
                await Navigation.PushAsync(new ItemsOfBookPage(selectedBook));
            }
        }

        async private void OnDelete_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(
                  "Delete book",
                  "Do u want to delete item's list?\nAllItems and categories will be deleted too",
                  "Okey",
                  "Cancel"))
                (((MenuItem)sender).CommandParameter as Models.Book).DeleteBook();
        }

        async private void OnClearBook_Clicked(object sender, EventArgs e)
        {            
            if (await DisplayAlert(
                "Clear all items of the book",
                "Do u want to clear item's list?\nAllItems will be deleted", 
                "Okey",
                "Cancel"))
            {
                (((MenuItem)sender).CommandParameter as Models.Book).ItemsOfBook.Clear();                
            }   
        }

        async private void OnEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.DetailPages.HelpedPages.AddEditBookPage(((MenuItem)sender).CommandParameter as Models.Book)); 
        }

        async private void BtnAddNewBook_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.DetailPages.HelpedPages.AddEditBookPage());
        }
    }
}