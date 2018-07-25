using System.Collections.ObjectModel;
using System;

namespace NotABook.Models
{
    public class Category : BaseClass
    {
        private Book currentBook;

        #region Prop

        public int CountOfItemsWithThisCategory
        {
            get => ItemsWithThisCategory.Count;
        }

        public ObservableCollection<Item> ItemsWithThisCategory
        {
            get
            {
                if (currentBook == null) throw new ArgumentNullException();
                if (currentBook.CategoryInItemsOfBook.Count < 1) return null;

                ObservableCollection<Item> items = new ObservableCollection<Item>();
                foreach (var item in currentBook.CategoryInItemsOfBook)
                {
                    if (item.Category.Id == this.Id) items.Add(item.Item);
                }
                return items;
            }
        }

        #endregion

        #region Constr
        public Category(Book curBook) : base()
        {
            currentBook = curBook;
            currentBook.CategoriesOfBook.Add(this);
        }

        public Category(Book curBook, string title) : this(curBook)
        {
            Title = title;
        }
        #endregion

        #region Methods

        public void DeleteCategory()
        {
            if (currentBook == null) throw new ArgumentNullException();
            currentBook.CategoriesOfBook.Remove(this);
            RemoveCategoryFromAllItems();
            currentBook.OnPropertyChanged("DateOfLastChanging");
        }

        public void RemoveCategoryFromAllItems()
        {
            if (currentBook == null) throw new ArgumentNullException();
            CategoryInItem.DeleteAllConnectionWithCategory(currentBook, this);
            currentBook.OnPropertyChanged("DateOfLastChanging");
        }

        #endregion
    }
}
