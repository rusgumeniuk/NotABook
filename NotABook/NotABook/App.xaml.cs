using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections;
using System.Linq;

using NotABook.Models;
using System.Collections.ObjectModel;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace NotABook
{
	public partial class App : Application
	{
        #region Lists etc
        public static Book currentBook = null;

        public static ObservableCollection<Book> Books
        {
            get => Book.Books;
            set => Book.Books = value;
        }           
       
        public static ObservableCollection<Item> ItemsList => currentBook?.ItemsOfBook;
        public static ObservableCollection<Category> CategoriesList => currentBook?.CategoriesOfBook;
        public static ObservableCollection<CategoryInItem> CategoryInItemsList => currentBook?.CategoryInItemsOfBook;        

        #endregion

        public App ()
		{
            InitializeComponent();

            {
                currentBook = new Book("coolBook");

                Book book = new Book("Second");
                Book book1 = new Book("Third");

                Category appleCategory = new Category(currentBook, "apple");
                Category saltCateg = new Category(currentBook, "salt");
                Category meatCategory = new Category(currentBook, "meat");

                Item bisc = new Item(currentBook, "Biscuit", "very good", new ObservableCollection<Category>() { appleCategory });
                Item salat = new Item(currentBook, "Salat", "very healthy", new ObservableCollection<Category>() { saltCateg });
                Item meat = new Item(currentBook, "Meeeat", "so cool", new ObservableCollection<Category>() { saltCateg, meatCategory });
            }                      

			MainPage = new Pages.MainPages.StartPage();                                  
		}

        #region StandartFunctions
        protected override void OnStart ()
		{                      
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
        #endregion
    }
}
