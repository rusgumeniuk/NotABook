﻿using NotABook.Models;
using NotABook.Pages.ItemPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotABook.Pages.DetailPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsOfBookPage : ContentPage
	{
        Book book = null;
        Category category = null;
        public ObservableCollection<Item> Items { get; set; }

        public ItemsOfBookPage()
        {
            InitializeComponent();

            book = NotABook.App.currentBook;
            if (NotABook.App.ItemsList.Count < 1)
                lblIsEmpty.Text = "No one item!";
            
            Items = book.ItemsList;

            this.BindingContext = this;
        }

        public ItemsOfBookPage(Models.Book currentBook)
        {
            InitializeComponent();


            book = currentBook;
            if (book.ItemsList.Count < 1)
                lblIsEmpty.Text = "No one item!";
            
            Items = book.ItemsList;

            this.BindingContext = this;
        }

        public ItemsOfBookPage(Models.Category currentCategory)
        {
            InitializeComponent();

            category = currentCategory;
            if (category.ItemsWithThisCategory == null) lblIsEmpty.Text = "No one item!";
            Items = category.ItemsWithThisCategory;

            this.BindingContext = this;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
          //await  DisplayAlert("te", "ok", "asd");
            
        }

        private async void btnAddNewItem_Clicked(object sender, EventArgs e)
        {
            //Pages.AddEditItemPage();    
            ItemPages.AddEditItemPage page = new ItemPages.AddEditItemPage();
            await Navigation.PushAsync(page);
        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            
        }

        async private void listOfItems_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Models.Item item)
            {
                listOfItems.SelectedItem = null;

                if (book != null)
                    await Navigation.PushAsync(new ItemCarouselPage(book));
                else if (category != null)
                    await Navigation.PushAsync(new ItemCarouselPage(category));
                else
                    await Navigation.PushAsync(new ItemCarouselPage(item));
            }
        }
    }
}