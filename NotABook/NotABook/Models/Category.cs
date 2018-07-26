using System.Collections.ObjectModel;
using System;
using NotABook.Models.Exceptions;

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
        //
        public ObservableCollection<Item> ItemsWithThisCategory
        {
            get
            {
                if (CurrentBook == null)
                    throw new BookNullException();
                if (CurrentBook.CategoryInItemsOfBook.Count < 1)
                    throw new EmptyCollectionException();
                if (!CategoryInItem.IsCategoryHasConnection(CurrentBook, this))
                    throw new ElementIsNotInCollectionException();

                ObservableCollection<Item> items = new ObservableCollection<Item>();
                foreach (var pair in CurrentBook.CategoryInItemsOfBook)
                {
                    if (pair.GetCategoryId == Id) items.Add(pair.Item);
                }
                return items;
            }
        }

        #endregion

        #region Constr
        public Category(Book curBook) : base()
        {
            CurrentBook = curBook ?? throw new BookNullException();
            CurrentBook.CategoriesOfBook.Add(this);
        }

        public Category(Book curBook, string title) : this(curBook)
        {
            Title = title;
        }
        #endregion

        #region Methods
        //
        public bool DeleteCategory()
        {
            if (CurrentBook == null)
                throw new BookNullException();

            CurrentBook.CategoriesOfBook.Remove(this);
            RemoveCategoryFromAllItems();

            if(IsTestingOff)
                CurrentBook.OnPropertyChanged("DateOfLastChanging");

            return !CurrentBook.CategoriesOfBook.Contains(this) && !CategoryInItem.IsCategoryHasConnection(CurrentBook, this);
        }
        public static bool DeleteCategory(Category category)
        {
            if (category == null)
                throw new CategoryNullException();
            if (category.CurrentBook == null)
                throw new BookNullException();

            category.CurrentBook.CategoriesOfBook.Remove(category);
            category.RemoveCategoryFromAllItems();

            if (category.IsTestingOff)
                category.CurrentBook.OnPropertyChanged("DateOfLastChanging");

            return !category.CurrentBook.CategoriesOfBook.Contains(category) && !CategoryInItem.IsCategoryHasConnection(category.CurrentBook, category);
        }
        //
        public bool RemoveCategoryFromAllItems()
        {
            if (CurrentBook == null)
                throw new BookNullException();            

            CategoryInItem.DeleteAllConnectionWithCategory(CurrentBook, this);

            if (IsTestingOff)
                CurrentBook.OnPropertyChanged("DateOfLastChanging");

            return !CategoryInItem.IsCategoryHasConnection(CurrentBook, this);
        }
        public static bool RemoveCategoryFromAllItems(Category category)
        {
            if (category == null)
                throw new CategoryNullException();
            if (category.CurrentBook == null)
                throw new BookNullException();
            
            CategoryInItem.DeleteAllConnectionWithCategory(category.CurrentBook, category);

            if (category.IsTestingOff)
                category.CurrentBook.OnPropertyChanged("DateOfLastChanging");

            return !CategoryInItem.IsCategoryHasConnection(category.CurrentBook, category);
        }

        #endregion
    }
}
