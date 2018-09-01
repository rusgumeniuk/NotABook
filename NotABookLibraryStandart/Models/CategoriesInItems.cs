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

        public static bool IsCategoryInItemIsNotNull(CategoryInItem categoryInItem)
        {
            return categoryInItem != null ? true : (IsXamarinProjectDeploying ? false : throw new CategoryInItemNullException());
        }

        public static bool IsItemHasConnection(Item item)
        {
            if(Item.IsItemIsNotNull(item))
            {
                return isItemHasConnection(item) ? true : throw new ElementIsNotInCollectionException();
            }

            return false;
        }
        public static bool IsCategoryHasConnection(Category category)
        {
            if (Category.IsCategoryIsNotNull(category))
            {
                return isCategoryHasConnection(category) ? true : throw new ElementIsNotInCollectionException();
            }

            return false;
        }

        private static bool isItemHasConnection(Item item)
        {
            foreach (var pair in item.CurrentBook.CategoryInItemsOfBook)
            {
                if (pair.GetItemId == item.Id)
                    return true;
            }
            return false;
        }
        private static bool isCategoryHasConnection(Category category)
        {
            foreach (var pair in category.CurrentBook.CategoryInItemsOfBook)
            {
                if (pair.GetCategoryId == category.Id)
                    return true;
            }
            return false;
        }

        public static string IsItemHasConnectionStr(Item item)
        {
            if (item == null)
                return "Item is null";
            if (item.CurrentBook == null)
                return "Book is null";

            return $"{IsItemHasConnection(item).ToString()}";
        }
        public static string IsCategoryHasConnectionStr(Book book, Category category)
        {
            if (book == null)
                return "Book is null";
            if (category == null)
                return "Category is null";

            return $"{IsCategoryHasConnection(category).ToString()}";
        }

        public static bool IsContainsThisPair(Category category, Item item)
        {
            return GetGuidOfPair(category, item) != Guid.Empty ? true : (IsXamarinProjectDeploying ? false : throw new ElementIsNotInCollectionException());
        }
        public static bool IsContainsThisPair(Book book, Guid categoryId, Guid itemId)
        {
            return GetGuidOfPair(book, categoryId, itemId) != Guid.Empty ? true : (IsXamarinProjectDeploying ? false : throw new ElementIsNotInCollectionException());
        }

        private static bool IsAlreadyContainsThisPair(Book book, Guid categoryId, Guid itemId)
        {
            return GetGuidOfPair(book, categoryId, itemId) != Guid.Empty ? (IsXamarinProjectDeploying ? true : throw new ElementAlreadyExistException()) : false;
        }

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

        public static bool IsCategoryAndItemFromSameBook(Category category, Item item)
        {
            if (Category.IsCategoryIsNotNull(category) && Item.IsItemIsNotNull(item)) 
            {
                return category.CurrentBook == item.CurrentBook ? true : throw new ElementsFromDifferentBooksException();
            }

            return false;
        }             

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

        public static bool DeleteConnection(Category category, Item item)
        {
            if(IsContainsThisPair(category, item))
            {
                Guid guid = CategoryInItem.GetGuidOfPair(category, item);
                if (IsGuidIsNotEmpty(guid))
                    category.CurrentBook.CategoryInItemsOfBook.RemoveAt(GetIndexOfPair(category.CurrentBook, guid));
            }

            return GetGuidOfPair(category, item) == Guid.Empty;
        }
        public static bool DeleteConnection(Book book, Guid categoryId, Guid itemId)
        {
            if(IsContainsThisPair(book, categoryId, itemId))
            {
                Guid guid = CategoryInItem.GetGuidOfPair(book, categoryId, itemId);
                if (IsGuidIsNotEmpty(guid))
                    book.CategoryInItemsOfBook.RemoveAt(GetIndexOfPair(book, guid));
            }
            return GetGuidOfPair(book, categoryId, itemId) == Guid.Empty;
        }


        public bool DeleteConnection()
        {
            if(IsContainsThisPair(CurrentBook, categoryId, itemId))
            {
                CurrentBook.CategoryInItemsOfBook.Remove(this);
            }

            
            return GetGuidOfPair(CurrentBook, this.categoryId, this.itemId) == Guid.Empty;
        }


        public static bool DeleteAllConnectionWithItem(Item item)
        {
            if (Item.IsItemAndItsBookNotNull(item))
            {
                if (!CategoryInItem.isItemHasConnection(item))
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

            return !isItemHasConnection(item);
        }
        public static bool DeleteAllConnectionWithCategory(Category category)
        {
            if (Category.IsCategoryAndItsBookNotNull(category))
            {
                if (!CategoryInItem.isCategoryHasConnection(category))
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

            return !isCategoryHasConnection(category);
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
