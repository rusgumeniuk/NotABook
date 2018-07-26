using System;
using System.Collections.ObjectModel;
using NotABook.Models.Exceptions;

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
        //
        public bool DeleteBook()
        {
            if (this == null)
                throw new BookNullException();
            if (!IsBooksContainsThisBook(this))
                throw new ElementIsNotInCollectionException();
            CategoryInItemsOfBook.Clear();
            ItemsOfBook.Clear();
            CategoriesOfBook.Clear();            
            Book.Books.Remove(this);

            return !IsBooksContainsThisBook(this);
        }
        public static bool DeleteBook(Book book)
        {
            if (book == null)
                throw new BookNullException();
            if (!IsBooksContainsThisBook(book))
                throw new ElementIsNotInCollectionException();

            book.CategoryInItemsOfBook.Clear();
            book.ItemsOfBook.Clear();
            book.CategoriesOfBook.Clear();
            Book.Books.Remove(book);

            return !IsBooksContainsThisBook(book);
        }

        //
        public static bool AddBookToCollection(Book book)
        {
            if (book == null)
                throw new BookNullException();
            if (IsBooksContainsThisBook(book))
                throw new ElementAlreadyExistException();

            Books.Add(book);

            return IsBooksContainsThisBook(book);
        }

        //
        public static bool IsBooksContainsThisBook(Book book)
        {
            if (book == null)
                throw new BookNullException();
            foreach(Book boook in Book.Books)
            {
                if (book.Id == boook.Id) return true;
            }
            return false;
        }
        public static bool IsBooksContainsThisBook(Guid bookId)
        {
            if (bookId == null)
                throw new BookNullException();
            if (bookId == Guid.Empty)
                throw new EmptyGuidException();

            foreach (Book boook in Book.Books)
            {
                if (bookId == boook.Id) return true;
            }
            return false;
        }
        //
        public static bool IsBookContainsItem(Book book, Item item)
        {
            return GetIndexOfItemByID(book, item.Id) != -1;
        }
        public static bool IsBookContainsItem(Book book, Guid itemId)
        {
            return GetIndexOfItemByID(book, itemId) != -1;
        }
        //
        public static bool IsBookContainsCategory(Book book, Category category)
        {
            return GetIndexOfCategoryByID(book, category.Id) != -1;
        }
        public static bool IsBookContainsCategory(Book book, Guid categoryId)
        {
            return GetIndexOfCategoryByID(book, categoryId) != -1;
        }

        //
        public int GetIndexOfItemByID(Guid itemId)
        {
            if (itemId == null)
                throw new ItemNullException();
            if (itemId == Guid.Empty)
                throw new EmptyGuidException();
            for (int i = 0; i < ItemsOfBook.Count; ++i)
            {
                if (ItemsOfBook[i].Id == itemId) return i;
            }

            return -1;
        }
        public static int GetIndexOfItemByID(Book book, Guid itemId)
        {
            if (book == null )
                throw new BookNullException();
            if (itemId == null)
                throw new ItemNullException();
            if (itemId == Guid.Empty)
                throw new EmptyGuidException();
            for (int i = 0; i < book.ItemsOfBook.Count; ++i)
            {
                if (book.ItemsOfBook[i].Id == itemId) return i;
            }

            return -1;
        }
        //
        public int GetIndexOfCategoryByID(Guid categoryId)
        {
            if (categoryId == null)
                throw new CategoryNullException();
            if (categoryId == Guid.Empty)
                throw new EmptyGuidException();
            for (int i = 0; i < CategoriesOfBook.Count; ++i)
            {
                if (CategoriesOfBook[i].Id == categoryId) return i;
            }

            return -1;
        }
        public static int GetIndexOfCategoryByID(Book book, Guid categoryId)
        {
            if (book == null)
                throw new BookNullException();
            if (categoryId == null)
                throw new CategoryNullException();
            if (categoryId == Guid.Empty)
                throw new EmptyGuidException();

            for (int i = 0; i < book.CategoriesOfBook.Count; ++i)
            {
                if (book.CategoriesOfBook[i].Id == categoryId) return i;
            }
            return -1;
        }

        //
        public bool DeleteItem(Item item)
        {
            if (item == null)
                throw new ItemNullException();
            if (!IsBookContainsItem(this, item))
                throw new ElementIsNotInCollectionException();

            item.DeleteItem();
            return !IsBookContainsItem(this, item);
            
        }
        public bool DeleteItem(Guid itemId)
        {
            if (itemId == Guid.Empty)
                throw new EmptyGuidException();
            if(!IsBookContainsItem(this, itemId))
                throw new ElementIsNotInCollectionException();

            this.ItemsOfBook[Book.GetIndexOfItemByID(this, itemId)].DeleteItem();
            return !IsBookContainsItem(this, itemId);
        }
        //
        public static bool DeleteItem(Book book, Item item)
        {
            if (book == null)
                throw new BookNullException();
            if (item == null)
                throw new ItemNullException();
            if(!IsBookContainsItem(book, item))
                throw new ElementIsNotInCollectionException();

            item.DeleteItem();
            return !IsBookContainsItem(book, item);
        }
        public static bool DeleteItem(Book book, Guid itemId)
        {
            if (book == null)
                throw new BookNullException();
            if (itemId == Guid.Empty)
                throw new EmptyGuidException();
            if(!IsBookContainsItem(book, itemId))
                throw new ElementIsNotInCollectionException();

            book.ItemsOfBook[Book.GetIndexOfItemByID(book, itemId)].DeleteItem();
            return !IsBookContainsItem(book, itemId);
        }

        //
        public bool DeleteCategory(Category category)
        {
            if (category == null)
                throw new CategoryNullException();
            if (!IsBookContainsCategory(this, category))
                throw new ElementIsNotInCollectionException();

            category.DeleteCategory();
            return !IsBookContainsCategory(this, category);

        }
        public bool DeleteCategory(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
                throw new EmptyGuidException();
            if (!IsBookContainsCategory(this, categoryId))
                throw new ElementIsNotInCollectionException();

            this.CategoriesOfBook[Book.GetIndexOfCategoryByID(this, categoryId)].DeleteCategory();
            return !IsBookContainsCategory(this, categoryId);
        }
        //
        public static bool DeleteCategory(Book book, Category category)
        {
            if (book == null)
                throw new BookNullException();
            if (category == null)
                throw new CategoryNullException();
            if(!IsBookContainsCategory(book, category))
                throw new ElementIsNotInCollectionException();

            category.DeleteCategory();
            return !IsBookContainsCategory(book, category);
        }
        public static bool DeleteCategory(Book book, Guid categoryId)
        {
            if (book == null)
                throw new BookNullException();
            if (categoryId == Guid.Empty)
                throw new EmptyGuidException();
            if (!IsBookContainsCategory(book, categoryId)) 
                throw new ElementIsNotInCollectionException();

            book.CategoriesOfBook[Book.GetIndexOfCategoryByID(book, categoryId)].DeleteCategory();
            return !IsBookContainsCategory(book, categoryId);
        }

        
        #endregion
    }
}
