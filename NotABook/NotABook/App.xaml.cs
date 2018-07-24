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

        public static ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();

       
        public static ObservableCollection<Item> ItemsList => currentBook?.ItemsOfBook;
        public static ObservableCollection<Category> CategoriesList => currentBook?.CategoriesOfBook;
        public static ObservableCollection<CategoryInItem> CategoryInItemsList => currentBook?.CategoryInItemsOfBook;

        public static ObservableCollection<Book> CurrentBook => new ObservableCollection<Book>() { currentBook };
        #endregion

        public App ()
		{
            InitializeComponent();

            {
                currentBook = new Book("coolBook");

                Book book = new Book("Second");
                Book book1 = new Book("Third");

                Category appleCategory = new Category("apple");
                Category saltCateg = new Category("salt");
                Category meatCategory = new Category("meat");

                Item bisc = new Item("Biscuit", "very good", new ObservableCollection<Category>() { appleCategory });
                Item salat = new Item("Salat", "very healthy", new ObservableCollection<Category>() { saltCateg });
                Item meat = new Item("Meeeat", "so cool", new ObservableCollection<Category>() { saltCateg, meatCategory });
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
