using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
                if (NotABook.App.ItemsList.Count < 1) return null;

                ObservableCollection<Item> items = new ObservableCollection<Item>();
                foreach (Models.Item item in NotABook.App.ItemsList)
                {
                    if (item.Categories.Contains(this))
                        items.Add(item);
                }
                return items;
            }
        }
        #endregion

        #region Constr
        public Category() : base()
        {
            if (NotABook.App.currentBook != null)
                NotABook.App.currentBook.CategoriesOfBook.Add(/*this.Id,*/ this);
        }

        public Category(string title) : this()
        {
            Title = title;
        }
        #endregion
    }
}
