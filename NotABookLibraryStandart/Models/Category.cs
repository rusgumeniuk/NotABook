using System;
using System.Collections.Generic;
using System.Text;
using NotABookLibraryStandart.Exceptions;
using System.Collections.ObjectModel;

namespace NotABookLibraryStandart.Models
{
    /// <summary>
    /// Represents a category of the book
    /// </summary>
    public class Category : ElementOfTheBook
    {

        #region Prop
        public string GetStringCountOfItemsWithCategory
        {
            get
            {
                string text = "Empty";
                try
                {
                    text = ItemsWithThisCategory?.Count.ToString();
                    if (text == null)
                        return "No one item";
                }
                catch (ElementIsNotInCollectionException)
                {
                    text = "el";
                }
                catch (ArgumentException)
                {
                    text = "WTF";
                }
                catch (Exception)
                {
                    text = "unknown exception";
                }
                return text;
            }
        }

        public ObservableCollection<Item> ItemsWithThisCategory
        {
            get
            {
                if (IsXamarinProjectDeploying)
                {
                    if (CurrentBook == null ||
                      CurrentBook.CategoryInItemsOfBook.Count < 1 ||
                      !CategoryInItem.IsCategoryHasConnection(this))
                        return null;
                }
                else
                {
                    if (CurrentBook == null)
                        throw new BookNullException();
                    if (CurrentBook.CategoryInItemsOfBook.Count < 1)
                        throw new EmptyCollectionException();
                    if (!CategoryInItem.IsCategoryHasConnection(this))
                        throw new ElementIsNotInCollectionException();
                }


                ObservableCollection<Item> items = new ObservableCollection<Item>();
                foreach (var pair in CurrentBook.CategoryInItemsOfBook)
                {
                    if (pair.GetCategoryId == Id)
                        items.Add(pair.Item);
                }
                return items;
            }
        }

        public int CountOfItemsWithThisCategory
        {
            get => this.ItemsWithThisCategory.Count;
        }

        #endregion

        #region Constr
        public Category(Book curBook) : base(curBook)
        {
            curBook.CategoriesOfBook.Add(this);
        }

        public Category(Book curBook, string title) : this(curBook)
        {
            Title = title;
        }
        #endregion

        #region Methods

        public bool IsCategoryContainsWord(string word)
        {
            if (BaseClass.IsXamarinProjectDeploying)
            {
                if (word == null)
                    throw new ArgumentNullException();
                if (String.IsNullOrWhiteSpace(word))
                    throw new ArgumentException();
            }
            else
            {
                if (word == null ||
                    String.IsNullOrWhiteSpace(word))
                    return false;
            }
            return this.Title.ToUpperInvariant().Contains(word.ToUpperInvariant());
        }
        public static bool IsCategoryContainsWord(Category category, string word)
        {
            if (BaseClass.IsXamarinProjectDeploying)
            {
                if (category == null)
                    throw new CategoryNullException();
                if (word == null)
                    throw new ArgumentNullException();
                if (String.IsNullOrWhiteSpace(word))
                    throw new ArgumentException();
            }
            else
            {
                if (category == null || 
                    word == null ||
                    String.IsNullOrWhiteSpace(word))
                        return false;
            }
            return category.Title.Contains(word);
        }

        public bool IsCategoryHasConnectionWithItem(Item item)
        {
            if (BaseClass.IsXamarinProjectDeploying)
            {
                if (item == null)
                    throw new ItemNullException();                     
            }
            else
            {
                if (item == null)
                    return false;
            }

            if (ItemsWithThisCategory?.Count < 1)
                return false;

            return ItemsWithThisCategory.Contains(item);
        }

        public string DeleteCategoryStr()
        {
            if (CurrentBook == null)
                return $"Book of {this.Title} is null";

            return $"Deleting of {this.Title} is {DeleteCategory().ToString()}";
        }
        public static string DeleteCategoryStr(Category category)
        {
            if (category == null)
                return "u category is null";
            if (category.CurrentBook == null)
                return $"Book of {category.Title} is null";
            return $"Deleting of {category.Title} is {DeleteCategory(category).ToString()}";
        }

        public bool DeleteCategory()
        {
            if (BaseClass.IsXamarinProjectDeploying)
            {

                if (CurrentBook == null)
                    return false;
            }
            else
            {
                if (CurrentBook == null)
                    throw new BookNullException();
            }

            CurrentBook.CategoriesOfBook.Remove(this);
            RemoveCategoryFromAllItems();

            if (IsXamarinProjectDeploying)
                CurrentBook.OnPropertyChanged("DateOfLastChanging");

            return !CurrentBook.CategoriesOfBook.Contains(this) && !CategoryInItem.IsCategoryHasConnection(this);
        }
        public static bool DeleteCategory(Category category)
        {
            if (BaseClass.IsXamarinProjectDeploying)
            {
                if (category == null || category.CurrentBook == null)
                    return false;
            }
            else
            {
                if (category == null)
                    throw new CategoryNullException();
                if (category.CurrentBook == null)
                    throw new BookNullException();
            }

            category.CurrentBook.CategoriesOfBook.Remove(category);
            category.RemoveCategoryFromAllItems();

            if (BaseClass.IsXamarinProjectDeploying)
                category.CurrentBook.OnPropertyChanged("DateOfLastChanging");

            return !category.CurrentBook.CategoriesOfBook.Contains(category) && !CategoryInItem.IsCategoryHasConnection(category);
        }

        public string RemoveCategoryFromAllItemsStr()
        {
            if (CurrentBook == null)
                return $"Book of {this.Title} is null";
            return $"Removing all connections of {this.Title} is {RemoveCategoryFromAllItems().ToString()}";
        }
        public static string RemoveCategoryFromAllItemsStr(Category category)
        {
            if (category == null)
                return "u category is null";
            if (category.CurrentBook == null)
                return $"Book of {category.Title} is null";

            return $"Removing all connections of {category.Title} is {RemoveCategoryFromAllItems(category).ToString()}";
        }

        public bool RemoveCategoryFromAllItems()
        {
            if (BaseClass.IsXamarinProjectDeploying)
            {
                if (CurrentBook == null)
                    return false;
            }
            else
            {
                if (CurrentBook == null)
                    throw new BookNullException();
            }


            CategoryInItem.DeleteAllConnectionWithCategory(this);

            if (IsXamarinProjectDeploying)
                CurrentBook.OnPropertyChanged("DateOfLastChanging");

            return !CategoryInItem.IsCategoryHasConnection(this);
        }
        public static bool RemoveCategoryFromAllItems(Category category)
        {
            if (BaseClass.IsXamarinProjectDeploying)
            {
                if (category == null || category.CurrentBook == null)
                    return false;
            }
            else
            {
                if (category == null)
                    throw new CategoryNullException();
                if (category.CurrentBook == null)
                    throw new BookNullException();
            }

            CategoryInItem.DeleteAllConnectionWithCategory(category);

            if (BaseClass.IsXamarinProjectDeploying)
                category.CurrentBook.OnPropertyChanged("DateOfLastChanging");

            return !CategoryInItem.IsCategoryHasConnection(category);
        }
                
        #endregion
    }
}
