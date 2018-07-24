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

        public static ObservableCollection<CategoryInItem> Items { get; set; }

        #endregion

        #region Constr

        public CategoryInItem() { }
        public CategoryInItem(Category category, Item item)
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
            foreach (var cii in CategoryInItem.Items)
            {
                if (cii.categoryId == category.Id) list.Add(cii);
            }
            return list;
        }
        public static ObservableCollection<CategoryInItem> GetCIIListByItem(Item item)
        {
            ObservableCollection<CategoryInItem> list = new ObservableCollection<CategoryInItem>();
            foreach (var cii in CategoryInItem.Items)
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
            foreach (var cii in CategoryInItem.Items)
            {
                if (cii.categoryId == category.Id && cii.itemId == item.Id) return cii.Id;
            }
            return Guid.Empty;
        }
        public static Guid GetGuidOfPair(Guid currentCategoryId, Guid currentItemId)
        {
            foreach (var cii in CategoryInItem.Items)
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
                CategoryInItem.Items.RemoveAt(GetIndexOfPair(guid));
            }
            return !IsContainsThisPair(category, item);
        }
        public bool DeleteConnection()
        {
            Items.Remove(this);
            return !IsContainsThisPair(this.categoryId, this.itemId);
        }

        public static int GetIndexOfPair(Guid guid)
        {
            for (int i = 0; i < CategoryInItem.Items.Count; ++i)
            {
                if (Items[i].Id == guid) return i;
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
