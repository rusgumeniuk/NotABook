using System;
using System.Collections.ObjectModel;

namespace NotABook.Models
{
    public class CategoryInItem : BaseClass
    {
        #region Fields
        private Book currentBook;

        private Guid categoryId;

        private Guid itemId;

        #endregion

        #region Properities

        public Category Category
        {
            get => currentBook.CategoriesOfBook[currentBook.GetIndexOfCategoryByID(categoryId)];
            set => categoryId = value.Id;
        }

        public Item Item
        {
            get => currentBook.ItemsOfBook[currentBook.GetIndexOfItemByID(itemId)];
            set => itemId = value.Id;
        }        

        #endregion

        #region Constr

        public CategoryInItem(Book curBook)
        {
            currentBook = curBook;
            currentBook.CategoryInItemsOfBook.Add(this);
        }
        public CategoryInItem(Book curBook, Category category, Item item) : this(curBook)
        {
            categoryId = category.Id;
            itemId = item.Id;
        }

        #endregion

        #region Methods

        public static CategoryInItem CreateCategoryInItem(Book curBook, Category category, Item item)
        {
            return new CategoryInItem(curBook, category, item);
        }

        public static ObservableCollection<CategoryInItem> GetCIIListByCategory(Book book, Category category)
        {
            if (book == null) throw new ArgumentNullException();
            ObservableCollection<CategoryInItem> list = new ObservableCollection<CategoryInItem>();
            foreach (var pair in book.CategoryInItemsOfBook)
            {
                if (pair.categoryId == category.Id) list.Add(pair);
            }
            return list;
        }
        public static ObservableCollection<CategoryInItem> GetCIIListByItem(Book book, Item item)
        {
            if (book == null) throw new ArgumentNullException();
            ObservableCollection<CategoryInItem> list = new ObservableCollection<CategoryInItem>();
            foreach (var pair in book.CategoryInItemsOfBook)
            {
                if (pair.itemId == item.Id) list.Add(pair);
            }
            return list;
        }

        public static bool IsContainsThisPair(Book book, Category category, Item item)
        {
            return GetGuidOfPair(book, category, item) != Guid.Empty;
        }
        public static bool IsContainsThisPair(Book book, Guid categoryId, Guid itemId)
        {
            return GetGuidOfPair(book, categoryId, itemId) != Guid.Empty;
        }

        public static Guid GetGuidOfPair(Book book, Category category, Item item)
        {
            if (book == null) throw new ArgumentNullException();
            foreach (var pair in book.CategoryInItemsOfBook)
            {
                if (pair.categoryId == category.Id && pair.itemId == item.Id) return pair.Id;
            }
            return Guid.Empty;
        }
        public static Guid GetGuidOfPair(Book book, Guid currentCategoryId, Guid currentItemId)
        {
            if (book == null) throw new ArgumentNullException();
            foreach (var pair in book.CategoryInItemsOfBook)
            {
                if (pair.categoryId == currentCategoryId && pair.itemId == currentItemId) return pair.Id;
            }
            return Guid.Empty;
        }

        public static int GetIndexOfPair(Book book, Guid guid)
        {
            if (book == null) throw new ArgumentNullException();
            for (int i = 0; i < book.CategoryInItemsOfBook.Count; ++i)
            {
                if (book.CategoryInItemsOfBook[i].Id == guid) return i;
            }
            return -1;
        }

        public static int DeleteConnection(Book book, Category category, Item item)
        {
            if (book == null) throw new ArgumentNullException();
            try
            {
                if (CategoryInItem.IsContainsThisPair(book, category, item))
                {
                    Guid guid = CategoryInItem.GetGuidOfPair(book, category, item);
                    book.CategoryInItemsOfBook.RemoveAt(GetIndexOfPair(book, guid));
                }
                return !IsContainsThisPair(book, category, item) == true ? 1 : 0;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public bool DeleteConnection()
        {
            if (currentBook == null) throw new ArgumentNullException();
            currentBook.CategoryInItemsOfBook.Remove(this);
            return !IsContainsThisPair(currentBook, this.categoryId, this.itemId);
        }

        public static string DeleteAllConnectionWithItem(Book book, Item item)
        {
            if (book == null) throw new ArgumentNullException();
            try
            {
                string text = String.Empty;
                foreach (var pair in book.CategoryInItemsOfBook)
                {
                    if (pair.itemId == item.Id)
                    {
                        text += DeleteConnection(book, pair.Category, item);
                    }
                }
                return text;
            }
            catch(Exception)
            {
                return "huII";
            }
           
        }        
        public static string DeleteAllConnectionWithCategory(Book book, Category category)
        {
            if (book == null) throw new ArgumentNullException();
            try
            {
                string text = String.Empty;
                foreach (var pair in book.CategoryInItemsOfBook)
                {
                    if (pair.categoryId == category.Id)
                    {
                        text += DeleteConnection(book, category, pair.Item);
                    }
                }
                return text;
            }
            catch (Exception)
            {
                return "hui";
            }
           
        }

        public static Exception TryTest(Book book, Item item)
        {
            try
            {
                DeleteAllConnectionWithItem(book, item);
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CategoryInItem)) return false;
            CategoryInItem pair = (CategoryInItem)obj;
            return pair.categoryId == this.categoryId && pair.itemId == this.itemId;
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        #endregion
    }
}
