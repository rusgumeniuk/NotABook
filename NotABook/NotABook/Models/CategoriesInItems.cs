using System;
using System.Collections.ObjectModel;

namespace NotABook.Models
{
    public class CategoryInItem : BaseClass
    {
        #region Fields

        private Guid categoryId;

        private Guid itemId;

        #endregion

        #region Properities

        public Category Category
        {
            get => App.currentBook.CategoriesOfBook[App.currentBook.GetIndexOfCategoryByID(categoryId)];
            set => categoryId = value.Id;
        }

        public Item Item
        {
            get => App.currentBook.ItemsOfBook[App.currentBook.GetIndexOfItemByID(itemId)];
            set => itemId = value.Id;
        }        

        #endregion

        #region Constr

        public CategoryInItem()
        {
            App.currentBook?.CategoryInItemsOfBook.Add(this);
        }
        public CategoryInItem(Category category, Item item) : this()
        {
            categoryId = category.Id;
            itemId = item.Id;
        }

        #endregion

        #region Methods

        public static CategoryInItem CreateCategoryInItem(Category category, Item item)
        {
            return new CategoryInItem(category, item);
        }

        public static ObservableCollection<CategoryInItem> GetCIIListByCategory(Category category)
        {
            ObservableCollection<CategoryInItem> list = new ObservableCollection<CategoryInItem>();
            foreach (var cii in App.currentBook?.CategoryInItemsOfBook)
            {
                if (cii.categoryId == category.Id) list.Add(cii);
            }
            return list;
        }
        public static ObservableCollection<CategoryInItem> GetCIIListByItem(Item item)
        {
            ObservableCollection<CategoryInItem> list = new ObservableCollection<CategoryInItem>();
            foreach (var cii in App.currentBook?.CategoryInItemsOfBook)
            {
                if (cii.itemId == item.Id) list.Add(cii);
            }
            return list;
        }

        public static bool IsContainsThisPair(Category category, Item item)
        {
            return GetGuidOfPair(category, item) != Guid.Empty;
        }
        public static bool IsContainsThisPair(Guid categoryId, Guid itemId)
        {
            return GetGuidOfPair(categoryId, itemId) != Guid.Empty;
        }

        public static Guid GetGuidOfPair(Category category, Item item)
        {
            foreach (var cii in App.currentBook?.CategoryInItemsOfBook)
            {
                if (cii.categoryId == category.Id && cii.itemId == item.Id) return cii.Id;
            }
            return Guid.Empty;
        }
        public static Guid GetGuidOfPair(Guid currentCategoryId, Guid currentItemId)
        {
            foreach (var cii in App.currentBook?.CategoryInItemsOfBook)
            {
                if (cii.categoryId == currentCategoryId && cii.itemId == currentItemId) return cii.Id;
            }
            return Guid.Empty;
        }

        public static bool DeleteConnection(Category category, Item item)
        {
            if(CategoryInItem.IsContainsThisPair(category, item))
            {
                Guid guid = CategoryInItem.GetGuidOfPair(category, item);
                App.currentBook?.CategoryInItemsOfBook.RemoveAt(GetIndexOfPair(guid));
            }
            return !IsContainsThisPair(category, item);
        }
        public bool DeleteConnection()
        {
            App.currentBook?.CategoryInItemsOfBook.Remove(this);
            return !IsContainsThisPair(this.categoryId, this.itemId);
        }

        public static void DeleteAllConnectionWithItem(Item item)
        {
            foreach (var pair in App.currentBook?.CategoryInItemsOfBook)
            {
                if (pair.itemId == item.Id) DeleteConnection(pair.Category, item);
            }
        }
        public static void DeleteAllConnectionWithCategory(Category category)
        {
            foreach (var pair in App.currentBook?.CategoryInItemsOfBook)
            {
                if (pair.categoryId == category.Id) DeleteConnection(category, pair.Item);
            }
        }

        public static int GetIndexOfPair(Guid guid)
        {
            for (int i = 0; i < App.currentBook?.CategoryInItemsOfBook.Count; ++i)
            {
                if (App.currentBook?.CategoryInItemsOfBook[i].Id == guid) return i;
            }
            return -1;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CategoryInItem)) return false;
            CategoryInItem cii = (CategoryInItem)obj;
            return cii.categoryId == this.categoryId && cii.itemId == this.itemId;
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        #endregion
    }
}
