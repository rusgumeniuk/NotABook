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

        public static Dictionary<Guid, Book> Books { get; set; } = new Dictionary<Guid, Book>();

        public static ObservableCollection<Book> BooksList => new ObservableCollection<Book>(Books.Values.ToList());
        public static ObservableCollection<Item> ItemsList => new ObservableCollection<Item>(currentBook?.ItemsOfBook.Values.ToList());
        public static ObservableCollection<Category> CategoriesList => new ObservableCollection<Category>(currentBook?.CategoriesOfBook.Values.ToList());
        public static ObservableCollection<Book> CurrentBook => new ObservableCollection<Book>() { currentBook };
        #endregion

        public App ()
		{
            //#if DEBUG            
            ////LiveReload.Init();
            //#endif


            InitializeComponent();

            {
                currentBook = new Book("coolBook");
                //Books.Add(currentBook.Id, currentBook);
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
