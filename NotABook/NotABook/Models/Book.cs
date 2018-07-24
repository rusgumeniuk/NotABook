using System.Collections.ObjectModel;

namespace NotABook.Models
{
    public class Book : BaseClass
    {
        #region Fields

        private ObservableCollection<Item> itemsOfBook = new ObservableCollection<Item>();        

        private ObservableCollection<Category> categoriesOfBook = new ObservableCollection<Category>();

        #endregion

        #region Propereties

        public ObservableCollection<Item> ItemsOfBook
        {
            get => itemsOfBook;
            set
            {
                itemsOfBook = value;
                OnPropertyChanged("ItemsOfBook");
            }
        }

        public ObservableCollection<Category> CategoriesOfBook
        {
            get => categoriesOfBook;
            set
            {
                categoriesOfBook = value;
                OnPropertyChanged("CategoriesOfBook");
            }
        }

        #endregion

        #region Constr

        public Book() : base()
        {
            NotABook.App.Books.Add(this);
        }

        public Book(string title) : base(title)
        {
            NotABook.App.Books.Add(this);
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

        #endregion
    }
}
