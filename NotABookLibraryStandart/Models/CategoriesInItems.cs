using System;
using System.Collections.Generic;
using System.Text;
using NotABookLibraryStandart.Exceptions;
using System.Collections.ObjectModel;

namespace NotABookLibraryStandart.Models
{
    /// <summary>
    /// Represents a solvable class 
    /// </summary>
    public class CategoryInItem : ElementOfTheBook
    {
        #region Fields        
        private Guid categoryId;

        private Guid itemId;

        #endregion

        #region Properities

        public Category Category
        {
            get => CurrentBook.CategoriesOfBook[CurrentBook.GetIndexOfCategoryByID(categoryId)];
            set => categoryId = value.Id;
        }
        public Item Item
        {
            get => CurrentBook.ItemsOfBook[CurrentBook.GetIndexOfItemByID(itemId)];
            set => itemId = value.Id;
        }

        public Guid GetCategoryId { get => categoryId; }
        public Guid GetItemId { get => itemId; }

        #endregion

        #region Constr

        public CategoryInItem(Book curBook) : base(curBook)
        {
            CurrentBook.CategoryInItemsOfBook.Add(this);
        }
        public CategoryInItem(Category category, Item item) : base(category.CurrentBook)
        {
            CurrentBook = category.CurrentBook;
            categoryId = category.Id;
            itemId = item.Id;
            CurrentBook.CategoryInItemsOfBook.Add(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Indicates whether the CategoryInItem is not null
        /// </summary>
        /// <param name="categoryInItem">The categoryInItem to test</param>
        /// <exception cref="CategoryInItemNullException">When categoryInItem is null and Xamarin mode is off</exception>
        /// <returns>true if categoryInItem is not null. Else if Xamarin mode is on - false.</returns>
        public static bool IsCategoryInItemIsNotNull(CategoryInItem categoryInItem)
        {
            return categoryInItem != null ? true : (IsXamarinProjectDeploying ? false : throw new CategoryInItemNullException());
        }

        /// <summary>
        /// Indicates whether the item has connection
        /// </summary>
        /// <exception cref="ElementIsNotInCollectionException">When item has no connection and Xamarin mode is off</exception>
        /// <param name="item"></param>
        /// <returns>true if item is not null. Else if Xamarin mode is on - false.</returns>
        public static bool IsItemHasConnection(Item item)
        {
            if(Item.IsItemIsNotNull(item))
            {
                return IsItemHasConnectionHidden(item) ? true : (IsXamarinProjectDeploying ? false : throw new ElementIsNotInCollectionException());
            }

            return false;
        }

        /// <summary>
        /// Indicates whether the category has connection
        /// </summary>
        /// <param name="category"></param>
        /// /// <exception cref="ElementIsNotInCollectionException">When category has no connection and Xamarin mode is off</exception>
        /// <returns>true if category is not null. Else if Xamarin mode is on - false.</returns>
        public static bool IsCategoryHasConnection(Category category)
        {
            if (Category.IsCategoryIsNotNull(category))
            {
                return IsCategoryHasConnectionHidden(category) ? true : (IsXamarinProjectDeploying ? false : throw new ElementIsNotInCollectionException());
            }

            return false;
        }

        /// <summary>
        /// Indicates whether the item has connection WITHOUT exception
        /// </summary>
        /// <param name="item"> item to test</param>
        /// <returns></returns>
        private static bool IsItemHasConnectionHidden(Item item)
        {
            foreach (var pair in item.CurrentBook.CategoryInItemsOfBook)
            {
                if (pair.GetItemId == item.Id)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Indicates whether the category has connection WITHOUT exception
        /// </summary>
        /// <param name="category">category to test</param>
        /// <returns></returns>
        private static bool IsCategoryHasConnectionHidden(Category category)
        {
            foreach (var pair in category.CurrentBook.CategoryInItemsOfBook)
            {
                if (pair.GetCategoryId == category.Id)
                    return true;
            }
            return false;
        }
               

        //public static string IsItemHasConnectionStr(Item item)
        //{
        //    if (item == null)
        //        return "Item is null";
        //    if (item.CurrentBook == null)
        //        return "Book is null";

        //    return $"{IsItemHasConnection(item).ToString()}";
        //}
        //public static string IsCategoryHasConnectionStr(Book book, Category category)
        //{
        //    if (book == null)
        //        return "Book is null";
        //    if (category == null)
        //        return "Category is null";

        //    return $"{IsCategoryHasConnection(category).ToString()}";
        //}
        
        /// <summary>
        /// Indicates whether the elements from one book and is book contains pair of the category and item
        /// </summary>
        /// <param name="category">category of the pair</param>
        /// <param name="item">item of the pair</param>
        /// <exception cref="ElementIsNotInCollectionException">When book doesn't contains this pair</exception>
        /// <returns></returns>
        public static bool IsContainsThisPair(Category category, Item item)
        {
            return GetGuidOfPair(category, item) != Guid.Empty ? true : (IsXamarinProjectDeploying ? false : throw new ElementIsNotInCollectionException());
        }

        /// <summary>
        /// Indicates whether the elements from one book and is book contains pair of the categoryId and itemId
        /// </summary>
        /// <param name="book">Book where try to find pair</param>
        /// <param name="categoryId">Id of the category</param>
        /// <param name="itemId">Id of the item</param>
        /// /// <exception cref="ElementIsNotInCollectionException">When book doesn't contains this pair</exception>
        /// <returns></returns>
        public static bool IsContainsThisPair(Book book, Guid categoryId, Guid itemId)
        {
            return GetGuidOfPair(book, categoryId, itemId) != Guid.Empty ? true : (IsXamarinProjectDeploying ? false : throw new ElementIsNotInCollectionException());
        }
        
        /// <summary>
        /// Indicates whether book already contains this pair and returns ElementAlreadyInCollection if true
        /// </summary>
        /// <param name="book"></param>
        /// <param name="categoryId"></param>
        /// <param name="itemId"></param>
        /// <exception cref="ElementAlreadyExistException">When book already contains this pair</exception>
        /// <returns>Exception if true</returns>
        private static bool IsAlreadyContainsThisPair(Book book, Guid categoryId, Guid itemId)
        {
            return GetGuidOfPair(book, categoryId, itemId) != Guid.Empty ? (IsXamarinProjectDeploying ? true : throw new ElementAlreadyExistException()) : false;
        }

        /// <summary>
        /// Returns guid Id of the pair with these elements 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Guid GetGuidOfPair(Category category, Item item)
        {
            if (IsCategoryAndItemFromSameBook(category, item))
            {
                foreach (var pair in category.CurrentBook.CategoryInItemsOfBook)
                {
                    if (pair.categoryId == category.Id && pair.itemId == item.Id) return pair.Id;
                }
            }
            return Guid.Empty;
        }

        /// <summary>
        /// Returns guid Id of the pair with these elements 
        /// </summary>
        /// <param name="book"></param>
        /// <param name="currentCategoryId"></param>
        /// <param name="currentItemId"></param>
        /// <returns></returns>
        public static Guid GetGuidOfPair(Book book, Guid currentCategoryId, Guid currentItemId)
        {
            if(Book.IsBookIsNotNull(book) && IsGuidIsNotEmpty(currentCategoryId) && IsGuidIsNotEmpty(currentItemId))
            {
                foreach (var pair in book.CategoryInItemsOfBook)
                {
                    if (pair.categoryId == currentCategoryId && pair.itemId == currentItemId)
                        return pair.Id;
                }
            }
            return Guid.Empty;
        }        

        /// <summary>
        /// Indicates whether the category and the item from one book
        /// </summary>
        /// <param name="category"></param>
        /// <param name="item"></param>
        /// <exception cref="ElementsFromDifferentBooksException">When the category and the item from different book</exception>
        /// <returns></returns>
        public static bool IsCategoryAndItemFromSameBook(Category category, Item item)
        {
            if (Category.IsCategoryIsNotNull(category) && Item.IsItemIsNotNull(item)) 
            {
                return category.CurrentBook == item.CurrentBook ? true : (IsXamarinProjectDeploying ? false : throw new ElementsFromDifferentBooksException());
            }

            return false;
        }

        /// <summary>
        /// Returns an index of the pair in the CategoryInItemOfTheBook
        /// </summary>
        /// <param name="book">A book in which we will check</param>
        /// <param name="guid">Id of the pair</param>
        /// <returns></returns>
        public static int GetIndexOfPair(Book book, Guid guid)
        {
            if (Book.IsBookIsNotNull(book))
            {
                if (guid != Guid.Empty)
                {
                    for (int i = 0; i < book.CategoryInItemsOfBook.Count; ++i)
                    {
                        if (book.CategoryInItemsOfBook[i].Id == guid)
                            return i;
                    }
                }
                return -1;
            }           
            return -2;
        }

        public static CategoryInItem CreateCategoryInItem(Category category, Item item)
        {
            if(IsCategoryAndItemFromSameBook(category, item))
            {
                if (!IsAlreadyContainsThisPair(category.CurrentBook, category.Id, item.Id))
                    return new CategoryInItem(category, item);
            }
            return null;            
        }

        public static ObservableCollection<CategoryInItem> GetCIIListByCategory(Category category)
        {
            if (Category.IsCategoryAndItsBookNotNull(category))
            {
                ObservableCollection<CategoryInItem> list = new ObservableCollection<CategoryInItem>();
                foreach (var pair in category.CurrentBook.CategoryInItemsOfBook)
                {
                    if (pair.categoryId == category.Id) list.Add(pair);
                }
                return list;
            }
            return null;
        }
        public static ObservableCollection<CategoryInItem> GetCIIListByItem(Item item)
        {
            if (Item.IsItemAndItsBookNotNull(item))
            {
                ObservableCollection<CategoryInItem> list = new ObservableCollection<CategoryInItem>();
                foreach (var pair in item.CurrentBook.CategoryInItemsOfBook)
                {
                    if (pair.itemId == item.Id) list.Add(pair);
                }
                return list;
            }
            return null;
        }



        public static bool DeleteAllConnectionWithItem(Item item)
        {
            if (Item.IsItemAndItsBookNotNull(item))
            {
                if (!CategoryInItem.IsItemHasConnectionHidden(item))
                    return true;

                foreach (var pair in CategoryInItem.GetCIIListByItem(item))
                {
                    if (!pair.DeleteConnection())
                    {
                        if (!BaseClass.IsXamarinProjectDeploying)
                            throw new InvalidOperationException();
                    }
                }
            }

            return !IsItemHasConnectionHidden(item);
        }
        public static bool DeleteAllConnectionWithCategory(Category category)
        {
            if (Category.IsCategoryAndItsBookNotNull(category))
            {
                if (!CategoryInItem.IsCategoryHasConnectionHidden(category))
                    return true;

                foreach (var pair in CategoryInItem.GetCIIListByCategory(category))
                {
                    if (!pair.DeleteConnection())
                    {
                        if (!BaseClass.IsXamarinProjectDeploying)
                            throw new InvalidOperationException();
                    }
                }
            }

            return !IsCategoryHasConnectionHidden(category);
        }


        public static bool DeleteConnection(Category category, Item item)
        {
            if (IsContainsThisPair(category, item))
            {
                Guid guid = CategoryInItem.GetGuidOfPair(category, item);
                if (IsGuidIsNotEmpty(guid))
                    category.CurrentBook.CategoryInItemsOfBook
                        .RemoveAt(GetIndexOfPair(category.CurrentBook, guid));
            }

            return GetGuidOfPair(category, item) == Guid.Empty;
        }
        public static bool DeleteConnection(Book book, Guid categoryId, Guid itemId)
        {
            if (IsContainsThisPair(book, categoryId, itemId))
            {
                Guid guid = CategoryInItem.GetGuidOfPair(book, categoryId, itemId);
                if (IsGuidIsNotEmpty(guid))
                    book.CategoryInItemsOfBook.RemoveAt(GetIndexOfPair(book, guid));
            }
            return GetGuidOfPair(book, categoryId, itemId) == Guid.Empty;
        }

        public bool DeleteConnection()
        {
            if (IsContainsThisPair(CurrentBook, categoryId, itemId))
            {
                CurrentBook.CategoryInItemsOfBook.Remove(this);
            }

            return GetGuidOfPair(CurrentBook, this.categoryId, this.itemId) == Guid.Empty;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CategoryInItem)) return false;
            CategoryInItem pair = (CategoryInItem)obj;
            return pair.categoryId == this.categoryId && pair.itemId == this.itemId;
        }
        public override int GetHashCode()
        {
            return categoryId.GetHashCode() ^ itemId.GetHashCode();
        }        
        #endregion
    }
}
