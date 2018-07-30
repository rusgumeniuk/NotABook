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
        
        public bool DeleteBook()
        {
            if (BaseClass.IsTestingOff)
            {
                if (this == null || !IsBooksContainsThisBook(this))
                    return false;
            }
            else
            {
                if (this == null)
                    throw new BookNullException();
                if (!IsBooksContainsThisBook(this))
                    throw new ElementIsNotInCollectionException();
            }
           
            CategoryInItemsOfBook.Clear();
            ItemsOfBook.Clear();
            CategoriesOfBook.Clear();            
            Book.Books.Remove(this);

            return !IsBooksContainsThisBook(this);
        }
        public static bool DeleteBook(Book book)
        {
            if (BaseClass.IsTestingOff)
            {
                if (book == null || !IsBooksContainsThisBook(book))
                    return false;
            }
            else
            {
                if (book == null)
                    throw new BookNullException();
                if (!IsBooksContainsThisBook(book))
                    throw new ElementIsNotInCollectionException();
            }

            book.CategoryInItemsOfBook.Clear();
            book.ItemsOfBook.Clear();
            book.CategoriesOfBook.Clear();
            Book.Books.Remove(book);

            return !IsBooksContainsThisBook(book);
        }

        
        public static bool AddBookToCollection(Book book)
        {
            if (BaseClass.IsTestingOff)
            {
                if (book == null || IsBooksContainsThisBook(book))
                    return false;
            }
            else
            {
                if (book == null)
                    throw new BookNullException();
                if (IsBooksContainsThisBook(book))
                    throw new ElementAlreadyExistException();
            }          

            Books.Add(book);

            return IsBooksContainsThisBook(book);
        }

        
        public static bool IsBooksContainsThisBook(Book book)
        {
            if (BaseClass.IsTestingOff)
            {
                if (book == null)
                    return false;
            }
            else
            {
                if (book == null)
                    throw new BookNullException();
            }
           
            foreach(Book boook in Book.Books)
            {
                if (book.Id == boook.Id) return true;
            }
            return false;
        }
        public static bool IsBooksContainsThisBook(Guid bookId)
        {
            if (BaseClass.IsTestingOff)
            {
                if (bookId == null || bookId == Guid.Empty)
                    return false;
            }
            else
            {
                if (bookId == null)
                    throw new BookNullException();
                if (bookId == Guid.Empty)
                    throw new EmptyGuidException();
            }
           

            foreach (Book boook in Book.Books)
            {
                if (bookId == boook.Id) return true;
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

        
        public int GetIndexOfItemByID(Guid itemId)
        {
            if (BaseClass.IsTestingOff)
            {
                if (itemId == null || itemId == Guid.Empty)
                    return -2;
            }
            else
            {
                if (itemId == null)
                    throw new ItemNullException();
                if (itemId == Guid.Empty)
                    throw new EmptyGuidException();
            }
          
            for (int i = 0; i < ItemsOfBook.Count; ++i)
            {
                if (ItemsOfBook[i].Id == itemId) return i;
            }

            return -1;
        }
        public static int GetIndexOfItemByID(Book book, Guid itemId)
        {
            if (BaseClass.IsTestingOff)
            {
                if (book == null)
                    return -3;
                if (itemId == null || itemId == Guid.Empty)
                    return -2;
            }
            else
            {
                if (book == null)
                    throw new BookNullException();
                if (itemId == null)
                    throw new ItemNullException();
                if (itemId == Guid.Empty)
                    throw new EmptyGuidException();
            }
            
            for (int i = 0; i < book.ItemsOfBook.Count; ++i)
            {
                if (book.ItemsOfBook[i].Id == itemId) return i;
            }

            return -1;
        }
        
        public int GetIndexOfCategoryByID(Guid categoryId)
        {
            if (BaseClass.IsTestingOff)
            {
                if (categoryId == null || categoryId == Guid.Empty)
                    return -2;
            }
            else
            {
                if (categoryId == null)
                    throw new CategoryNullException();
                if (categoryId == Guid.Empty)
                    throw new EmptyGuidException();
            }
         
            for (int i = 0; i < CategoriesOfBook.Count; ++i)
            {
                if (CategoriesOfBook[i].Id == categoryId) return i;
            }

            return -1;
        }
        public static int GetIndexOfCategoryByID(Book book, Guid categoryId)
        {
            if (BaseClass.IsTestingOff)
            {
                if (book == null)
                    return -3;
                if (categoryId == null || categoryId == Guid.Empty)
                    return -2;
            }
            else
            {
                if (book == null)
                    throw new BookNullException();
                if (categoryId == null)
                    throw new CategoryNullException();
                if (categoryId == Guid.Empty)
                    throw new EmptyGuidException();
            }
            

            for (int i = 0; i < book.CategoriesOfBook.Count; ++i)
            {
                if (book.CategoriesOfBook[i].Id == categoryId) return i;
            }
            return -1;
        }


        public int GetIndexOfCatrgoryInItemyByID(Guid pairId)
        {
            if (BaseClass.IsTestingOff)
            {
                if (pairId == null || pairId == Guid.Empty)
                    return -2;
            }
            else
            {
                if (pairId == null)
                    throw new CategoryNullException();
                if (pairId == Guid.Empty)
                    throw new EmptyGuidException();
            }
           
            for (int i = 0; i < CategoryInItemsOfBook.Count; ++i)
            {
                if (CategoryInItemsOfBook[i].Id == pairId) return i;
            }

            return -1;
        }
        public static int GetIndexOfCategoryInItemByID(Book book, Guid pairId)
        {
            if (BaseClass.IsTestingOff)
            {
                if (book == null)
                    return -3;
                if (pairId == null || pairId == Guid.Empty)
                    return -2;
            }
            else
            {
                if (book == null)
                    throw new BookNullException();
                if (pairId == null)
                    throw new CategoryNullException();
                if (pairId == Guid.Empty)
                    throw new EmptyGuidException();
            }
          

            for (int i = 0; i < book.CategoryInItemsOfBook.Count; ++i)
            {
                if (book.CategoryInItemsOfBook[i].Id == pairId) return i;
            }
            return -1;
        }
        
        public bool DeleteItem(Item item)
        {
            if (BaseClass.IsTestingOff)
            {
                if (item == null || !IsBookContainsItem(this, item))
                    return false;
            }
            else
            {
                if (item == null)
                    throw new ItemNullException();
                if (!IsBookContainsItem(this, item))
                    throw new ElementIsNotInCollectionException();
            }           

            item.DeleteItem();                       
            return !IsBookContainsItem(this, item);
            
        }
        public bool DeleteItem(Guid itemId)
        {
            if (BaseClass.IsTestingOff)
            {
                if (itemId == Guid.Empty || !IsBookContainsItem(this, itemId))
                    return false;
            }
            else
            {
                if (itemId == Guid.Empty)
                    throw new EmptyGuidException();
                if (!IsBookContainsItem(this, itemId))
                    throw new ElementIsNotInCollectionException();
            }         

            this.ItemsOfBook[Book.GetIndexOfItemByID(this, itemId)].DeleteItem();
            return !IsBookContainsItem(this, itemId);
        }
        
        public static bool DeleteItem(Book book, Item item)
        {
            if (BaseClass.IsTestingOff)
            {
                if (book == null || item == null || !IsBookContainsItem(book, item))
                    return false;
            }
            else
            {
                if (book == null)
                    throw new BookNullException();
                if (item == null)
                    throw new ItemNullException();
                if (!IsBookContainsItem(book, item))
                    throw new ElementIsNotInCollectionException();
            }
          

            item.DeleteItem();
            return !IsBookContainsItem(book, item);
        }
        public static bool DeleteItem(Book book, Guid itemId)
        {
            if (BaseClass.IsTestingOff)
            {
                if (book == null || itemId == Guid.Empty || !IsBookContainsItem(book, itemId))
                    return false;
            }
            else
            {
                if (book == null)
                    throw new BookNullException();
                if (itemId == Guid.Empty)
                    throw new EmptyGuidException();
                if (!IsBookContainsItem(book, itemId))
                    throw new ElementIsNotInCollectionException();
            }
           

            book.ItemsOfBook[Book.GetIndexOfItemByID(book, itemId)].DeleteItem();
            return !IsBookContainsItem(book, itemId);
        }

        
        public bool DeleteCategory(Category category)
        {
            if (BaseClass.IsTestingOff)
            {
                if (category == null || !IsBookContainsCategory(this, category))
                {
                    return false;
                }
            }
            else
            {
                if (category == null)
                    throw new CategoryNullException();
                if (!IsBookContainsCategory(this, category))
                    throw new ElementIsNotInCollectionException();
            }           

            category.DeleteCategory();
            return !IsBookContainsCategory(this, category);
        }
        public bool DeleteCategory(Guid categoryId)
        {
            if (BaseClass.IsTestingOff)
            {
                if (categoryId == Guid.Empty || !IsBookContainsCategory(this, categoryId))
                    return false;
            }
            else
            {
                if (categoryId == Guid.Empty)
                    throw new EmptyGuidException();
                if (!IsBookContainsCategory(this, categoryId))
                    throw new ElementIsNotInCollectionException();
            }
            

            this.CategoriesOfBook[Book.GetIndexOfCategoryByID(this, categoryId)].DeleteCategory();
            return !IsBookContainsCategory(this, categoryId);
        }
        
        public static bool DeleteCategory(Book book, Category category)
        {
            if (BaseClass.IsTestingOff)
            {
                if (book == null || category == null || !IsBookContainsCategory(book, category))
                   return false;
            }
            else
            {
                if (book == null)
                    throw new BookNullException();
                if (category == null)
                    throw new CategoryNullException();
                if (!IsBookContainsCategory(book, category))
                    throw new ElementIsNotInCollectionException();
            }
            

            category.DeleteCategory();
            return !IsBookContainsCategory(book, category);
        }
        public static bool DeleteCategory(Book book, Guid categoryId)
        {
            if (BaseClass.IsTestingOff)
            {
                if (book == null || categoryId == Guid.Empty || !IsBookContainsCategory(book, categoryId))
                    return false;
            }
            else
            {
                if (book == null)
                    throw new BookNullException();
                if (categoryId == Guid.Empty)
                    throw new EmptyGuidException();
                if (!IsBookContainsCategory(book, categoryId))
                    throw new ElementIsNotInCollectionException();
            }           

            book.CategoriesOfBook[Book.GetIndexOfCategoryByID(book, categoryId)].DeleteCategory();
            return !IsBookContainsCategory(book, categoryId);
        }        
        #endregion
    }
}
