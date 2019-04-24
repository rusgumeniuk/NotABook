using NotABookLibraryStandart.Models.BookElements;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotABook.Pages.DetailPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllBooksPage : ContentPage
    {
        public AllBooksPage()
        {
            InitializeComponent();
        }

        public async void BookList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Book selectedBook)
            {
                App.currentBook = selectedBook;
                await Navigation.PushAsync(new NotesOfBookPage(selectedBook));
            }
        }

        private async void OnEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HelpedPages.AddEditBookPage(((MenuItem)sender).CommandParameter as Book));
        }

        private async void OnDelete_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(
                  "Delete book",
                  "Do u want to delete item's list?\nAllItems and categories will be deleted too",
                  "Okey",
                  "Cancel"))
                await DisplayAlert("Non implemented","Error", "ok");//await DisplayAlert("Deletin book", (((MenuItem)sender).CommandParameter as Book).Delete().ToString(), "ok");
        }

        private async void OnClearBook_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(
                "Clear all items of the book",
                "Do u want to clear item's list?\nAllItems will be deleted",
                "Okey",
                "Cancel"))
            {
                await DisplayAlert("Non implemented", "Error", "ok");//(((MenuItem)sender).CommandParameter as Book).Notes.Clear();
            }
        }

        private async void OnClearCategoriesList_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(
               "Clear all categories of the book",
               "Do u want to clear categories's list?\nAll categories will be deleted",
               "Okey",
               "Cancel"))
            {
                await DisplayAlert("Non implemented", "Error", "ok");//Book.ClearCaregoriesList(((MenuItem)sender).CommandParameter as Book);
            }
        }

        private async void OnDeleteAllElements_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(
               "Delete all elements of the book",
               "Do u want to delete all elements?\nAll elements will be deleted",
               "Okey",
               "Cancel"))
            {
                await DisplayAlert("Non implemented", "Error", "ok");// Book.RemoveAllElementsOfBook(((MenuItem)sender).CommandParameter as Book);
            }
        }

        private async void BtnAddNewBook_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.DetailPages.HelpedPages.AddEditBookPage());
        }


    }
}