using NotABook.Pages.ItemPages;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotABookLibraryStandart.Models;

namespace NotABook.Pages.DetailPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsOfBookPage : ContentPage
	{
        Book book = null;
        Category category = null;
        public ObservableCollection<Item> Items
        {
            get
            {
                if (category != null)
                    return category.ItemsWithThisCategory;
                if (book != null)
                    return book.ItemsOfBook;
                return null;
            }            
        }

        public ItemsOfBookPage()
        {
            InitializeComponent();

            book = App.currentBook;
            if (App.ItemsList.Count < 1)
                LblIsEmpty.Text = "No one item!";                       

            this.BindingContext = this;
        }

        public ItemsOfBookPage(Book currentBook)
        {
            InitializeComponent();


            book = currentBook;
            if (book.ItemsOfBook.Count < 1)
                LblIsEmpty.Text = "No one item!";                       

            this.BindingContext = this;
        }

        public ItemsOfBookPage(Category currentCategory)
        {
            InitializeComponent();

            category = currentCategory;
            if (category.ItemsWithThisCategory == null)
                LblIsEmpty.Text = "No one item!";

            this.BindingContext = this;
        }


        private async void BtnAddNewItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemPages.AddEditItemPage());
        }

        async private void ListOfItems_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is  Item item)
            {
                ListOfItems.SelectedItem = null;

                if (book != null)
                    await Navigation.PushAsync(new ItemCarouselPage(book));
                else if (category != null)
                    await Navigation.PushAsync(new ItemCarouselPage(category));
                else
                    await Navigation.PushAsync(new ItemCarouselPage(item));
            }
        }


        async public void OnDelete_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (await DisplayAlert(
                    "Delete item",
                    "Do u want to delete this item?",
                    "Yes", "NO"))
                    (((MenuItem)sender).CommandParameter as Item).Delete();
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
                CategoryInItem.DeleteAllConnectionWithItem(((MenuItem)sender).CommandParameter as Item);             
            }               
           
        }

        async private void OnEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEditItemPage(((MenuItem)sender).CommandParameter as Item));
        }
    }
}