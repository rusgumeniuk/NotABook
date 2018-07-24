using System.Collections.ObjectModel;

namespace NotABook.Models
{
    public class Category : BaseClass
    {
        #region Prop

        public int CountOfItemsWithThisCategory
        {
            get => ItemsWithThisCategory.Count;
        }

        public ObservableCollection<Item> ItemsWithThisCategory
        {
            get
            {
                if (NotABook.App.CategoryInItemsList.Count < 1) return null;

                ObservableCollection<Item> items = new ObservableCollection<Item>();
                foreach (var item in NotABook.App.CategoryInItemsList)
                {
                    if (item.Category.Id == this.Id) items.Add(item.Item);
                }
                return items;
            }
        }

        #endregion

        #region Constr
        public Category() : base()
        {
            if (NotABook.App.currentBook != null)
                NotABook.App.currentBook.CategoriesOfBook.Add(this);
        }

        public Category(string title) : this()
        {
            Title = title;
        }
        #endregion

        #region Methods

        public void DeleteCategory()
        {
            App.currentBook.CategoriesOfBook.Remove(this);
            RemoveCategoryFromAllItems();
            App.currentBook?.OnPropertyChanged("DateOfLastChanging");
        }

        public void RemoveCategoryFromAllItems()
        {
            CategoryInItem.DeleteAllConnectionWithCategory(this);
            App.currentBook?.OnPropertyChanged("DateOfLastChanging");
        }

        #endregion
    }
}
