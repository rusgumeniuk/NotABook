using System.Collections.ObjectModel;
using System;

namespace NotABook.Models
{
    public class Category : BaseClass
    {
        public Book CurrentBook { get; private set; }

        #region Prop

        public int CountOfItemsWithThisCategory
        {
            get => ItemsWithThisCategory.Count;
        }

        public ObservableCollection<Item> ItemsWithThisCategory
        {
            get
            {
                if (CurrentBook == null) throw new ArgumentNullException();
                if (CurrentBook.CategoryInItemsOfBook.Count < 1) return null;

                ObservableCollection<Item> items = new ObservableCollection<Item>();
                foreach (var item in CurrentBook.CategoryInItemsOfBook)
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
            CurrentBook = curBook ?? throw new ArgumentNullException();
            CurrentBook.CategoriesOfBook.Add(this);
        }

        public Category(Book curBook, string title) : this(curBook)
        {
            Title = title;
        }
        #endregion

        #region Methods

        public void DeleteCategory()
        {
            if (CurrentBook == null) throw new ArgumentNullException();
            CurrentBook.CategoriesOfBook.Remove(this);
            RemoveCategoryFromAllItems();
            CurrentBook.OnPropertyChanged("DateOfLastChanging");
        }

        public void RemoveCategoryFromAllItems()
        {
            if (CurrentBook == null) throw new ArgumentNullException();
            CategoryInItem.DeleteAllConnectionWithCategory(CurrentBook, this);
            CurrentBook.OnPropertyChanged("DateOfLastChanging");
        }

        #endregion
    }
}
