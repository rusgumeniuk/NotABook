using NotABook.Pages.ItemPages;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotABookLibraryStandart.Models;
using NotABookLibraryStandart.Models.BookElements;

namespace NotABook.Pages.DetailPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotesOfBookPage : ContentPage
	{
        Book book = null;
        Category category = null;
        //public ObservableCollection<Item> Items
        //{
        //    get
        //    {
        //        if (category != null)
        //            return category.ItemsWithThisCategory;
        //        if (book != null)
        //            return book.Notes;
        //        return null;
        //    }            
        //}

        public NotesOfBookPage()
        {
            InitializeComponent();

            book = App.currentBook;
            if (App.currentBook.Notes.Count < 1)
                LblIsEmpty.Text = "No one item!";                       

            this.BindingContext = this;
        }

        public NotesOfBookPage(Book currentBook)
        {
            InitializeComponent();


            book = currentBook;
            if (book.Notes.Count < 1)
                LblIsEmpty.Text = "No one item!";                       

            this.BindingContext = this;
        }

        public NotesOfBookPage(Category currentCategory)
        {
            InitializeComponent();

            category = currentCategory;
            DisplayAlert("Non implemented", "Error", "ok");
            //if (category.ItemsWithThisCategory == null)
            //    LblIsEmpty.Text = "No one item!";

            this.BindingContext = this;
        }


        private async void BtnAddNewItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemPages.AddEditNotePage());
        }

        async private void ListOfItems_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await DisplayAlert("Non implemented", "Error", "ok");
            //if (e.Item is  Item item)
            //{
            //    ListOfItems.SelectedItem = null;

            //    if (book != null)
            //        await Navigation.PushAsync(new ItemCarouselPage(book));
            //    else if (category != null)
            //        await Navigation.PushAsync(new ItemCarouselPage(App.currentBook, category));
            //    else
            //        await Navigation.PushAsync(new ItemCarouselPage(App.currentBook, item));
            //}
        }


        async public void OnDelete_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (await DisplayAlert(
                    "Delete item",
                    "Do u want to delete this item?",
                    "Yes", "NO"))
                    await DisplayAlert("Non implemented", "Error", "ok");// (((MenuItem)sender).CommandParameter as Item).Delete(App.currentBook);
            }
            catch(Exception ex)
            {
                await DisplayAlert("EXC", ex.Message + "\n\n" + ex.StackTrace, "OK");
            }         
        }

        async private void OnDeleteConnetions_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(
               "Delete item",
               "Do u want to delete all connections with item?",
               "Yes", "NO"))
            {
                await DisplayAlert("Non implemented", "Error", "ok");//CategoryInItem.DeleteAllConnectionWithItem(App.currentBook, ((MenuItem)sender).CommandParameter as Item);             
            }               
           
        }

        async private void OnEdit_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Non implemented", "Error", "ok");//await Navigation.PushAsync(new AddEditItemPage(((MenuItem)sender).CommandParameter as Item));
        }
    }
}