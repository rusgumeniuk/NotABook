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
                return ItemsWithThisCategory?.Count.ToString() ?? "No one item";
            }
        }

        public ObservableCollection<Item> ItemsWithThisCategory
        {
            get
            {
                if (Book.IsBookIsNotNull(CurrentBook) &&
                        CurrentBook.CategoryInItemsOfBook.Count > -1 &&
                            CategoryInItem.IsCategoryHasConnection(this))
                {
                    ObservableCollection<Item> items = new ObservableCollection<Item>();
                    foreach (var pair in CurrentBook.CategoryInItemsOfBook)
                    {
                        if (pair.GetCategoryId == Id)
                            items.Add(pair.Item);
                    }
                    return items;
                }
                return null;               
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
            CurrentBook.CategoriesOfBook.Add(this);
        }

        public Category(Book curBook, string title) : this(curBook)
        {
            Title = title;
        }
        #endregion

        #region Methods

        public static bool IsCategoryIsNotNull(Category category)
        {
            return category != null ? true : (IsXamarinProjectDeploying ? false : throw new CategoryNullException());
        }
        public static bool IsCategoryAndItsBookNotNull(Category category)
        {
            return IsCategoryIsNotNull(category) && Book.IsBookIsNotNull(category.CurrentBook);
        }

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
            if (Item.IsItemIsNotNull(item))
            {
                if (ItemsWithThisCategory?.Count < 1)
                    return false;

                return ItemsWithThisCategory.Contains(item);
            }
            return false;
        }

        public string DeleteCategoryStr()
        {
            if (Book.IsBookIsNotNull(CurrentBook))
                return $"Book of {this.Title} is null";

            return $"Deleting of {this.Title} is {DeleteCategory().ToString()}";
        }
        public static string DeleteCategoryStr(Category category)
        {
            if (Category.IsCategoryAndItsBookNotNull(category))
                return $"Smt is null";
            return $"Deleting of {category.Title} is {DeleteCategory(category).ToString()}";
        }

        public bool DeleteCategory()
        {
            if (Book.IsBookIsNotNull(CurrentBook))
            {
                CurrentBook.CategoriesOfBook.Remove(this);
                RemoveCategoryFromAllItems();
                CurrentBook.OnPropertyChanged("DateOfLastChanging");
                return !CurrentBook.CategoriesOfBook.Contains(this) && !CategoryInItem.IsCategoryHasConnection(this);
            }
            return false;
        }
        public static bool DeleteCategory(Category category)
        {
            if (Category.IsCategoryAndItsBookNotNull(category))
            {
                category.CurrentBook.CategoriesOfBook.Remove(category);
                category.RemoveCategoryFromAllItems();
                category.CurrentBook.OnPropertyChanged("DateOfLastChanging");
                return !category.CurrentBook.CategoriesOfBook.Contains(category) && !CategoryInItem.IsCategoryHasConnection(category);
            }
            return false;       
        }

        public string RemoveCategoryFromAllItemsStr()
        {
            if (Book.IsBookIsNotNull(CurrentBook))
                return $"Book of {this.Title} is null";
            return $"Removing all connections of {this.Title} is {RemoveCategoryFromAllItems().ToString()}";
        }
        public static string RemoveCategoryFromAllItemsStr(Category category)
        {
            if (Category.IsCategoryAndItsBookNotNull(category))
                return "Smt is null";
            return $"Removing all connections of {category.Title} is {RemoveCategoryFromAllItems(category).ToString()}";
        }

        public bool RemoveCategoryFromAllItems()
        {
            if (Book.IsBookIsNotNull(CurrentBook))
            {
                CategoryInItem.DeleteAllConnectionWithCategory(this);
                CurrentBook.OnPropertyChanged("DateOfLastChanging");
                return !CategoryInItem.IsCategoryHasConnection(this);
            }
            return false;           
        }
        public static bool RemoveCategoryFromAllItems(Category category)
        {
            if (Category.IsCategoryAndItsBookNotNull(category))
            {
                CategoryInItem.DeleteAllConnectionWithCategory(category);
                category.CurrentBook.OnPropertyChanged("DateOfLastChanging");
                return !CategoryInItem.IsCategoryHasConnection(category);
            }
            return false;           
        }
                
        #endregion
    }
}
