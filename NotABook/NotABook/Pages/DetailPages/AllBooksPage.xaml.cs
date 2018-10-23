using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotABookLibraryStandart.Models;

namespace NotABook.Pages.DetailPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllBooksPage : ContentPage
	{
		public AllBooksPage ()
		{
			InitializeComponent();
            //LBLTEst.Text = ModelsLibrary.Class1.Method();
            //LBLTEst.Text = 
            
		}            

        public async void BookList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Book selectedBook)
            {
                App.currentBook = selectedBook;                
                await Navigation.PushAsync(new ItemsOfBookPage(selectedBook));
            }
        }

        async private void OnEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HelpedPages.AddEditBookPage(((MenuItem)sender).CommandParameter as Book));
        }

        async private void OnDelete_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(
                  "Delete book",
                  "Do u want to delete item's list?\nAllItems and categories will be deleted too",
                  "Okey",
                  "Cancel"))
                    await DisplayAlert("Deletin book", (((MenuItem)sender).CommandParameter as Book).Delete().ToString(), "ok");
        }

        async private void OnClearBook_Clicked(object sender, EventArgs e)
        {            
            if (await DisplayAlert(
                "Clear all items of the book",
                "Do u want to clear item's list?\nAllItems will be deleted", 
                "Okey",
                "Cancel"))
            {
                Book.ClearItemsList(((MenuItem)sender).CommandParameter as Book);
            }   
        }

        async private void OnClearCategoriesList_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(
               "Clear all categories of the book",
               "Do u want to clear categories's list?\nAll categories will be deleted",
               "Okey",
               "Cancel"))
            {
                Book.ClearCaregoriesList(((MenuItem)sender).CommandParameter as Book);
            }
        }

        async private void OnDeleteAllElements_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(
               "Delete all elements of the book",
               "Do u want to delete all elements?\nAll elements will be deleted",
               "Okey",
               "Cancel"))
            {
                Book.RemoveAllElementsOfBook(((MenuItem)sender).CommandParameter as Book);
            }
        }

        async private void BtnAddNewBook_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.DetailPages.HelpedPages.AddEditBookPage());
        }

       
    }
}