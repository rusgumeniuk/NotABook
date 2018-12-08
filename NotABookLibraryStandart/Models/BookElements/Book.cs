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
    public class Book : Entity
    {
        #region Fields

        private ObservableCollection<Item> itemsOfBook = new ObservableCollection<Item>();

        private ObservableCollection<Category> categoriesOfBook = new ObservableCollection<Category>();

        private ObservableCollection<CategoryInItem> categoriesInItemOfBook = new ObservableCollection<CategoryInItem>();

        public static ObservableCollection<Book> Books = new ObservableCollection<Book>();
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

        /// <summary>
        /// Represents a list of items which contain partOfItem in Title or Desription
        /// </summary>
        /// <param name="partOfItem"></param>
        /// <exception cref="ArgumentNullException">when partOfItem is null, empty or white spaces</exception>
        /// <returns></returns>
        public ObservableCollection<Item> FindItems(string partOfItem)
        {
            if (ExtensionClass.IsStringNotNull(partOfItem))
            {
                List<Item> items = new List<Item>();
                foreach (Item item in ItemsOfBook)
                {
                    if (item.IsItemContainsWord(partOfItem))
                        items.Add(item);
                }
                return new ObservableCollection<Item>(items);
            }
            return null;
        }

        /// <summary>
        /// Adds book to Books
        /// </summary>
        /// <param name="book">book to add</param>
        /// <exception cref="BookNullException">when book is null</exception>
        /// <exception cref="ElementIsNotInCollectionException">when after adding book not in the collection</exception>
        /// <returns></returns>
        public static bool AddBookToCollection(Book book)
        {
            if (IsBookIsNotNull(book))
            {
                if(IndexOfBookInBooks(book) == -1)
                {
                    Books.Add(book);
                    return IsBooksContainsThisBook(book);
                }
                return ProjectType == TypeOfRunningProject.Xamarin ? false : throw new ElementAlreadyExistException();
            }
            return false;
        }

        /// <summary>
        /// Removes all elements of the book and remove book from the Books
        /// </summary>
        /// <returns></returns>
        public  bool Delete()
        {
            if(IsBookIsNotNullAndInBooks(this))
            {
                CategoryInItemsOfBook.Clear();
                RemoveAllElementsOfBook(this);
                Book.Books.Remove(this);

                return IndexOfBookInBooks(this) == -1;
            }
            return false;
        }

        /// <summary>
        /// Removes all elements of the book and remove book from the Books
        /// </summary>
        /// <param name="book"> book to delete</param>
        /// <returns></returns>
        public static bool DeleteBook(Book book)
        {
            if (IsBookIsNotNullAndInBooks(book))
            {                
                book.CategoryInItemsOfBook.Clear();
                RemoveAllElementsOfBook(book);
                Book.Books.Remove(book);

                return IndexOfBookInBooks(book) == -1;
            }
            return false;
        }                


       /// <summary>
       /// Indicates whether the Books contains book
       /// </summary>
       /// <param name="book">book to test</param>
       /// <exception cref="ElementIsNotInCollectionException">when Book doesn't contain book</exception>
       /// <exception cref="BookNullException">when book is null</exception>
       /// <returns></returns>
        public static bool IsBooksContainsThisBook(Book book)
        {
            return IndexOfBookInBooks(book) != -1 ? true : (ProjectType == TypeOfRunningProject.Xamarin ? false : throw new ElementIsNotInCollectionException());
        }

        /// <summary>
        /// Indicates whether the Books contains book with bookId
        /// </summary>
        /// <param name="bookId">id to test</param>
        /// <exception cref="ElementIsNotInCollectionException">when Book doesn't contain book with same id</exception>       
        /// <exception cref="EmptyGuidException">when bookId is empty</exception>
        /// <returns></returns>
        public static bool IsBooksContainsThisBook(Guid bookId)
        {
            return IndexOfBookInBooks(bookId) != -1 ? true : (ProjectType == TypeOfRunningProject.Xamarin  ? false : throw new ElementIsNotInCollectionException());
        } 


        /// <summary>
        /// Indicates whether the book contains item
        /// </summary>
        /// <param name="book">book in which we try to find item</param>
        /// <param name="item">item to test</param>
        /// <exception cref="ElementIsNotInCollectionException">when book doesn't contain item</exception>
        /// <exception cref="ItemNullException">when item is null</exception>
        /// <returns></returns>
        public static bool IsBookContainsItem(Book book, Item item)
        {
            return GetIndexOfItemByID(book, item.Id) > -1 ? true : (ProjectType == TypeOfRunningProject.Xamarin  ? false : throw new ElementIsNotInCollectionException($"{item.Title} not in the {book.Title}"));
        }

        /// <summary>
        /// Indicates whether the book contains item with itemId ID
        /// </summary>
        /// <param name="book">book in which we try to find</param>
        /// <param name="itemId">Id to test</param>
        ///   /// <exception cref="ElementIsNotInCollectionException">when book doesn't contain item</exception>
        /// <exception cref="EmptyGuidException">when itemId is empty</exception>
        /// <returns></returns>
        public static bool IsBookContainsItem(Book book, Guid itemId)
        {
            return GetIndexOfItemByID(book, itemId) > -1 ? true : (ProjectType == TypeOfRunningProject.Xamarin  ? false : throw new ElementIsNotInCollectionException());
        }


        /// <summary>
        /// Indicates whether the book contains category
        /// </summary>
        /// <param name="book">book in which we try to find item</param>
        /// <param name="category">category to test</param>
        /// <exception cref="ElementIsNotInCollectionException">when book doesn't contain category</exception>
        /// <exception cref="CategoryNullException">when category is null</exception>
        /// <returns></returns>
        public static bool IsBookContainsCategory(Book book, Category category)
        {
            return GetIndexOfCategoryByID(book, category.Id) > -1 ? true : (ProjectType == TypeOfRunningProject.Xamarin  ? false : throw new ElementIsNotInCollectionException($"{category.Title} not in the {book.Title}"));
        }

        /// <summary>
        /// Indicates whether the book contains category with categoryId ID
        /// </summary>
        /// <param name="book">book in which we try to find</param>
        /// <param name="categoryId">Id to test</param>
        ///   /// <exception cref="ElementIsNotInCollectionException">when book doesn't contain category with categoryId</exception>
        /// <exception cref="EmptyGuidException">when categoryId is empty</exception>
        /// <returns></returns>
        public static bool IsBookContainsCategory(Book book, Guid categoryId)
        {
            return GetIndexOfCategoryByID(book, categoryId) > -1 ? true : (ProjectType == TypeOfRunningProject.Xamarin  ? false : throw new ElementIsNotInCollectionException());
        }



        /// <summary>
        /// Indicates whether the book is not null
        /// </summary>        
        /// <param name="book">The book to test</param>
        /// <exception cref="BookNullException">When book is null and Xamarin mode is off</exception>
        /// <returns>true if book is not null. Else if Xamarin mode is on - false.</returns>
        public static bool IsBookIsNotNull(Book book)
        {
            return book != null ? true : (ProjectType == TypeOfRunningProject.Xamarin ? false : throw new BookNullException());
        }

        /// <summary>
        /// Indicates whether the book is not null and is Books contains book
        /// </summary>        
        /// <param name="book">The book to test</param>
        /// <exception cref="BookNullException">When book is null  and Xamarin mode is off</exception>
        /// <exception cref="ElementIsNotInCollectionException">When Books doesn't contain book</exception>
        /// <returns>true if book is not null. Else if Xamarin mode is on - false.</returns>
        public static bool IsBookIsNotNullAndInBooks(Book book)
        {
            return IsBookIsNotNull(book) && IsBooksContainsThisBook(book);
        }

        
        internal static int IndexOfBookInBooks(Book book)
        {
            if (Book.IsBookIsNotNull(book))
            {
                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i] == book)
                        return i;
                }
            }
            return -1;
        }
        internal static int IndexOfBookInBooks(Guid bookId)
        {
            if (IsGuidIsNotEmpty(bookId))
            {
                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i].Id == bookId)
                        return i;
                }
            }
            return -1;
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
                    if (book.ItemsOfBook[i].Id == itemId)
                        return i;
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
                    if (book.CategoriesOfBook[i].Id == categoryId)
                        return i;
                }
            }            
            return -1;
        }

        internal int GetIndexOfCategoryInItemByID(Guid pairId)
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
        internal static int GetIndexOfCategoryInItemByID(Book book, Guid pairId)
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
            if (Item.IsItemIsNotNull(item) && IsBookContainsItem(this, item))
            {
                item.Delete();
                return GetIndexOfItemByID(item.Id) == -1;
            }
            return false;
        }
        public bool DeleteItem(Guid itemId)
        {
            if (IsGuidIsNotEmpty(itemId) && IsBookContainsItem(this, itemId))
            {
                this.ItemsOfBook[Book.GetIndexOfItemByID(this, itemId)].Delete();
                return GetIndexOfItemByID(itemId) == -1;
            }
            return false;           
        }

        public static bool DeleteItem(Book book, Item item)
        {
            if(Book.IsBookIsNotNull(book) && Item.IsItemIsNotNull(item) && IsBookContainsItem(book, item))
            {
                item.Delete();
                return GetIndexOfItemByID(book, item.Id) == -1;
            }
            return false;
        }
        public static bool DeleteItem(Book book, Guid itemId)
        {
            if (Book.IsBookIsNotNull(book) && IsGuidIsNotEmpty(itemId) && IsBookContainsItem(book, itemId))
            {
                book.ItemsOfBook[Book.GetIndexOfItemByID(book, itemId)].Delete();
                return GetIndexOfItemByID(book, itemId) == -1;
            }
            return false;           
        }


        public bool DeleteCategory(Category category)
        {
            if (Category.IsCategoryIsNotNull(category) && IsBookContainsCategory(this, category))
            {
                category.Delete();
                return GetIndexOfCategoryByID(category.Id) == -1;
            }
            return false;
        }
        public bool DeleteCategory(Guid categoryId)
        {
            if (IsGuidIsNotEmpty(categoryId) && IsBookContainsCategory(this, categoryId))
            {
                this.CategoriesOfBook[Book.GetIndexOfCategoryByID(this, categoryId)].Delete();
                return GetIndexOfCategoryByID(categoryId) == -1;
            }
            return false;            
        }

        public static bool DeleteCategory(Book book, Category category)
        {
            if (Book.IsBookIsNotNull(book) && Category.IsCategoryIsNotNull(category) && IsBookContainsCategory(book, category))
            {
                category.Delete();
                return GetIndexOfCategoryByID(book, category.Id) == -1;
            }
            return false;            
        }
        public static bool DeleteCategory(Book book, Guid categoryId)
        {
            if (Book.IsBookIsNotNull(book) && IsGuidIsNotEmpty(categoryId) && IsBookContainsCategory(book, categoryId))
            {
                book.CategoriesOfBook[Book.GetIndexOfCategoryByID(book, categoryId)].Delete();
                return GetIndexOfCategoryByID(book, categoryId) == -1;
            }
            return false;          
        }


        public static bool ClearItemsList(Book book)
        {
            if (Book.IsBookIsNotNull(book))
            {
                while (book.ItemsOfBook.Count > 0)
                {
                    if (!book.ItemsOfBook[0].Delete())
                        throw new InvalidOperationException();
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
                    if (!book.CategoriesOfBook[0].Delete())
                        throw new InvalidOperationException();
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
