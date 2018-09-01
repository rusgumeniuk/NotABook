using System;
using System.Collections.Generic;
using System.Text;
using NotABookLibraryStandart.Exceptions;
using System.Collections.ObjectModel;

namespace NotABookLibraryStandart.Models
{
    /// <summary>
    /// Represents a book, set of that's forms a notebook
    /// </summary>
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

        public ObservableCollection<CategoryInItem> CategoryInItemsOfBook
        {
            get => categoriesInItemOfBook;
            set
            {
                categoriesInItemOfBook = value;
                OnPropertyChanged("CategoryInItemsOfBook");
            }
        }

        #endregion

        #region Constr

        private Book() : base()
        {
            Books.Add(this);
        }

        public Book(string title) : base(title)
        {
            Books.Add(this);
        }

        #endregion

        #region Methods

        public bool DeleteBook()
        {
            if(IsBookIsNotNullAndInBooks(this))
            {
                CategoryInItemsOfBook.Clear();
                ItemsOfBook.Clear();
                CategoriesOfBook.Clear();
                Book.Books.Remove(this);

                return !IsBooksContainsThisBook(this);
            }
            return false;
        }
        public static bool DeleteBook(Book book)
        {
            if (IsBookIsNotNullAndInBooks(book))
            {
                book.CategoryInItemsOfBook.Clear();
                book.ItemsOfBook.Clear();
                book.CategoriesOfBook.Clear();
                Book.Books.Remove(book);

                return !IsBooksContainsThisBook(book);
            }
            return false;
        }


        public static bool AddBookToCollection(Book book)
        {
            if (IsBookIsNotNullAndInBooks(book))
            {
                Books.Add(book);
                return IsBooksContainsThisBook(book);
            }
            return false;
          
        }


        public static bool IsBookIsNotNull(Book book)
        {
            return book != null ? true : (IsXamarinProjectDeploying ? false : throw new BookNullException());
        }

        public static bool IsBookIsNotNullAndInBooks(Book book)
        {
            return IsBookIsNotNull(book) && IsBooksContainsThisBook(book);
        }

        public static bool IsBooksContainsThisBook(Book book)
        {
            if (Book.IsBookIsNotNull(book))
            {
                foreach (Book boook in Book.Books)
                {
                    if (book.Id == boook.Id) return true;
                }
            }
            
            return false;
        }
        public static bool IsBooksContainsThisBook(Guid bookId)
        {
            if (IsGuidIsNotEmpty(bookId))
            {
                foreach (Book boook in Book.Books)
                {
                    if (bookId == boook.Id) return true;
                }
            }           
            return false;
        }

        public static bool IsBookContainsItem(Book book, Item item)
        {
            return GetIndexOfItemByID(book, item.Id) > -1;
        }
        public static bool IsBookContainsItem(Book book, Guid itemId)
        {
            return GetIndexOfItemByID(book, itemId) > -1;
        }

        public static bool IsBookContainsCategory(Book book, Category category)
        {
            return GetIndexOfCategoryByID(book, category.Id) > -1;
        }
        public static bool IsBookContainsCategory(Book book, Guid categoryId)
        {
            return GetIndexOfCategoryByID(book, categoryId) > -1;
        }

        public ObservableCollection<Item> FindItems(string partOfItem)
        {
            if (BaseClass.IsXamarinProjectDeploying)
            {
                if (partOfItem == null)
                    throw new ArgumentNullException();
                if (String.IsNullOrWhiteSpace(partOfItem))
                    throw new ArgumentException();
                if (ItemsOfBook.Count < 1)
                    throw new ElementIsNotInCollectionException();
            }
            else
            {
                if (partOfItem == null || 
                    String.IsNullOrWhiteSpace(partOfItem) || 
                    ItemsOfBook.Count < 1)
                        return null;
            }

            List<Item> items = new List<Item>();
            foreach(Item item in ItemsOfBook)
            {
                if (item.IsItemContainsWord(partOfItem))
                    items.Add(item);
            }
            return new ObservableCollection<Item>(items);

        }

        public int GetIndexOfItemByID(Guid itemId)
        {
            if (IsGuidIsNotEmpty(itemId))
            {
                for (int i = 0; i < ItemsOfBook.Count; ++i)
                {
                    if (ItemsOfBook[i].Id == itemId) return i;
                }
            }            
            return -1;
        }
        public static int GetIndexOfItemByID(Book book, Guid itemId)
        {
            if(Book.IsBookIsNotNull(book) && IsGuidIsNotEmpty(itemId))
            {
                for (int i = 0; i < book.ItemsOfBook.Count; ++i)
                {
                    if (book.ItemsOfBook[i].Id == itemId) return i;
                }
            }
            return -1;
        }

        public int GetIndexOfCategoryByID(Guid categoryId)
        {
            if (IsGuidIsNotEmpty(categoryId))
            {
                for (int i = 0; i < CategoriesOfBook.Count; ++i)
                {
                    if (CategoriesOfBook[i].Id == categoryId) return i;
                }
            }
            return -1;
        }
        public static int GetIndexOfCategoryByID(Book book, Guid categoryId)
        {
            if (Book.IsBookIsNotNull(book) && IsGuidIsNotEmpty(categoryId))
            {
                for (int i = 0; i < book.CategoriesOfBook.Count; ++i)
                {
                    if (book.CategoriesOfBook[i].Id == categoryId) return i;
                }
            }            
            return -1;
        }


        public int GetIndexOfCatrgoryInItemyByID(Guid pairId)
        {
            if(IsGuidIsNotEmpty(pairId))
            {
                for (int i = 0; i < CategoryInItemsOfBook.Count; ++i)
                {
                    if (CategoryInItemsOfBook[i].Id == pairId) return i;
                }
            }

            return -1;
        }
        public static int GetIndexOfCategoryInItemByID(Book book, Guid pairId)
        {
            if (Book.IsBookIsNotNull(book) && IsGuidIsNotEmpty(pairId))
            {
                for (int i = 0; i < book.CategoryInItemsOfBook.Count; ++i)
                {
                    if (book.CategoryInItemsOfBook[i].Id == pairId) return i;
                }
            }           
            return -1;
        }


        public bool DeleteItem(Item item)
        {
            if (Item.IsItemIsNotNull(item) && IsBookContainsItem(this, item))////////////////
            {
                item.DeleteItem();
                return !IsBookContainsItem(this, item);
            }
            return false;
        }
        public bool DeleteItem(Guid itemId)
        {
            if (IsGuidIsNotEmpty(itemId) && IsBookContainsItem(this, itemId))
            {
                this.ItemsOfBook[Book.GetIndexOfItemByID(this, itemId)].DeleteItem();
                return !IsBookContainsItem(this, itemId);
            }
            return false;           
        }

        public static bool DeleteItem(Book book, Item item)
        {
            if(Book.IsBookIsNotNull(book) && Item.IsItemIsNotNull(item) && IsBookContainsItem(book, item))
            {
                item.DeleteItem();
                return !IsBookContainsItem(book, item);
            }
            return false;
        }
        public static bool DeleteItem(Book book, Guid itemId)
        {
            if (Book.IsBookIsNotNull(book) && IsGuidIsNotEmpty(itemId) && IsBookContainsItem(book, itemId))
            {
                book.ItemsOfBook[Book.GetIndexOfItemByID(book, itemId)].DeleteItem();
                return !IsBookContainsItem(book, itemId);
            }
            return false;           
        }


        public bool DeleteCategory(Category category)
        {
            if (Category.IsCategoryIsNotNull(category) && IsBookContainsCategory(this, category))
            {
                category.DeleteCategory();
                return !IsBookContainsCategory(this, category);
            }
            return false;
        }
        public bool DeleteCategory(Guid categoryId)
        {
            if (IsGuidIsNotEmpty(categoryId) && IsBookContainsCategory(this, categoryId))
            {
                this.CategoriesOfBook[Book.GetIndexOfCategoryByID(this, categoryId)].DeleteCategory();
                return !IsBookContainsCategory(this, categoryId);
            }
            return false;            
        }

        public static bool DeleteCategory(Book book, Category category)
        {
            if (Book.IsBookIsNotNull(book) && Category.IsCategoryIsNotNull(category) && IsBookContainsCategory(book, category))
            {
                category.DeleteCategory();
                return !IsBookContainsCategory(book, category);
            }
            return false;            
        }
        public static bool DeleteCategory(Book book, Guid categoryId)
        {
            if (Book.IsBookIsNotNull(book) && IsGuidIsNotEmpty(categoryId) && IsBookContainsCategory(book, categoryId))
            {
                book.CategoriesOfBook[Book.GetIndexOfCategoryByID(book, categoryId)].DeleteCategory();
                return !IsBookContainsCategory(book, categoryId);
            }
            return false;          
        }


        public static bool ClearItemsList(Book book)
        {
            if (Book.IsBookIsNotNull(book))
            {
                while (book.ItemsOfBook.Count > 0)
                {
                    if (!book.ItemsOfBook[0].DeleteItem())
                        return false;
                }
            }           
            return book.ItemsOfBook.Count == 0;
        }
        public static bool ClearCaregoriesList(Book book)
        {
            if (Book.IsBookIsNotNull(book))
            {
                while (book.CategoriesOfBook.Count > 0)
                {
                    if (!book.CategoriesOfBook[0].DeleteCategory())
                        return false;
                }
            }           
            return book.CategoriesOfBook.Count == 0;
        }

        public static bool RemoveAllElementsOfBook(Book book)
        {
            return Book.IsBookIsNotNull(book) ? ClearItemsList(book) && ClearCaregoriesList(book) : false;
        }
        #endregion
    }
}
