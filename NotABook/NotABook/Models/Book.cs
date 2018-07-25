using System;
using System.Collections.ObjectModel;

namespace NotABook.Models
{
    public class Book : BaseClass
    {
        #region Fields

        private ObservableCollection<Item> itemsOfBook = new ObservableCollection<Item>();        

        private ObservableCollection<Category> categoriesOfBook = new ObservableCollection<Category>();

        private ObservableCollection<CategoryInItem> categoriesInItemOfBook = new ObservableCollection<CategoryInItem>();

        #endregion

        #region Propereties

        public static ObservableCollection<Book> Books = new ObservableCollection<Book>();

        public ObservableCollection<Item> ItemsOfBook
        {
            get => itemsOfBook;
            set
            {
                itemsOfBook = value;
                if (IsTestingOff)
                    OnPropertyChanged("ItemsOfBook");
            }
        }

        public ObservableCollection<Category> CategoriesOfBook
        {
            get => categoriesOfBook;
            set
            {
                categoriesOfBook = value;
                if (IsTestingOff)
                    OnPropertyChanged("CategoriesOfBook");
            }
        }

        public ObservableCollection<CategoryInItem> CategoryInItemsOfBook
        {
            get => categoriesInItemOfBook;
            set
            {
                categoriesInItemOfBook = value;
                if (IsTestingOff)
                    OnPropertyChanged("CategoryInItemsOfBook");
            }
        }

        #endregion

        #region Constr

        public Book() : base()
        {
            Books.Add(this);
        }

        public Book(string title) : base(title)
        {
            Books.Add(this);
        }

        #endregion

        #region Methods

        public void DeleteBook()
        {
            if (App.currentBook == this)
                App.currentBook = null;

            ItemsOfBook.Clear();
            CategoriesOfBook.Clear();
            App.Books.Remove(this);
        }

        public int GetIndexOfItemByID(Guid itemId)
        {
            for (int i = 0; i < App.currentBook.ItemsOfBook.Count; ++i)
            {
                if (App.currentBook.ItemsOfBook[i].Id == itemId) return i;
            }

            return -1;
        }

        public int GetIndexOfCategoryByID(Guid categoryId)
        {
            for (int i = 0; i < App.currentBook.CategoriesOfBook.Count; ++i)
            {
                if (App.currentBook.CategoriesOfBook[i].Id == categoryId) return i;
            }

            return -1;
        }

        #endregion
    }
}
