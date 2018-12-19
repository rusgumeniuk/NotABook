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
    public class Category : BookElement
    {
        #region Prop
        public string GetStringCountOfItemsWithCategory
        {
            get
            {
                return ItemsWithThisCategory?.Count.ToString() ?? "No one item. ";
            }
        }

        public ObservableCollection<Item> ItemsWithThisCategory
        {
            get
            {
                //if (
                //        //CurrentBook.CategoryInItemsOfBook.IsNotEmptyCollection() &&
                //            CategoryInItem.IsCategoryHasConnection(this))
                //{
                //    ObservableCollection<Item> items = new ObservableCollection<Item>();
                //    foreach (var pair in CurrentBook.CategoryInItemsOfBook)
                //    {
                //        if (pair.GetCategoryId == Id)
                //            items.Add(pair.Item);
                //    }
                //    return items;
                //}
                return null;               
            }
        }

        public int CountOfItemsWithThisCategory
        {
            get => ItemsWithThisCategory?.Count ?? -1;
        }
        #endregion

        #region Constr
        public Category(Book curBook) : base(curBook)
        {
            //CurrentBook.CategoriesOfBook.Add(this);
        }

        public Category(Book curBook, string title) : this(curBook)
        {
            Title = title;
        }
        #endregion

        #region Methods
       
        /// <summary>
        /// Indicates whether the CategoryInItem is null
        /// </summary>
        /// <param name="category">The category to test</param>
        /// <exception cref="CategoryNullException">when category is null</exception>
        /// <returns>true if categoryInItem is not null. Else if Xamarin mode is on - false.</returns>
        public static bool IsCategoryIsNotNull(Category category)
        {
            return category != null ? true : (ProjectType == TypeOfRunningProject.Xamarin  ? false : throw new CategoryNullException());
        }

        /// <summary>
        /// Indicates whether the category and its book is not null
        /// </summary>
        /// <param name="category">category to test</param>
        /// <exception cref="CategoryNullException">When category is null</exception>
        /// <exception cref="BookNullException">When book of item is null</exception>
        /// <returns></returns>
        public static bool IsCategoryAndItsBookNotNull(Book CurrentBook, Category category)
        {
            return IsCategoryIsNotNull(category) && Book.IsBookIsNotNull(CurrentBook);
        }

        /// <summary>
        /// Indicates whether the category contains word
        /// </summary>
        /// <param name="word">string to find</param>
        /// <exception cref="ArgumentNullException">when word is null or empty</exception>
        /// <returns></returns>
        public bool IsCategoryContainsWord(string word)
        {                      
            return ExtensionClass.IsStringNotNull(word) && Title.ToUpperInvariant().Contains(word.ToUpperInvariant());
        }

        /// <summary>
        /// Indicates whether the category contains word
        /// </summary>
        /// /// <param name="category"> category to test</param>
        /// <param name="word">string to find</param>
        /// <exception cref="ArgumentNullException">when word is null or empty</exception>
        /// <returns></returns>
        public static bool IsCategoryContainsWord(Category category, string word)
        {
            return Category.IsCategoryIsNotNull(category) && ExtensionClass.IsStringNotNull(word) && category.Title.ToUpperInvariant().Contains(word.ToUpperInvariant());
        }

        /// <summary>
        /// Indicates whether category has connection with item
        /// </summary>
        /// <param name="item">item with which we try to find a connection</param>
        /// <exception cref="ItemNullException">when item is null</exception>
        /// <exception cref="EmptyCollectionException">when category has not any connection</exception>        
        /// <returns></returns>
        public bool IsCategoryHasConnectionWithItem(Item item)
        {
            return Item.IsItemIsNotNull(item) && ItemsWithThisCategory.IsNotEmptyCollection() && ItemsWithThisCategory.Contains(item);
        }

        public string DeleteCategoryStr(Book CurrentBook)
        {
            //if (Book.IsBookIsNotNull(CurrentBook))
            //    return $"Book of {this.Title} is null";

            return $"Deleting of {this.Title} is {DeleteCategory( this, CurrentBook).ToString()}";
        }
        public static string DeleteCategoryStr(Category category, Book CurrentBook)
        {
            if (Category.IsCategoryAndItsBookNotNull(CurrentBook, category))
                return $"Smt is null";
            return $"Deleting of {category.Title} is {DeleteCategory(category, CurrentBook).ToString()}";
        }

        public bool Delete(Book CurrentBook)
        {
            if (Book.IsBookIsNotNull(CurrentBook))
            {
                CurrentBook.CategoriesOfBook.Remove(this);
                RemoveCategoryFromAllItems(CurrentBook);
                CurrentBook.OnPropertyChanged(CurrentBook, "DateOfLastChanging");
                return !CurrentBook.CategoriesOfBook.Contains(this);
            }
            return false;
        }
        public static bool DeleteCategory(Category category, Book CurrentBook)
        {
            if (Category.IsCategoryAndItsBookNotNull(CurrentBook, category))
            {
                CurrentBook.CategoriesOfBook.Remove(category);
                //CurrentBook.RemoveCategoryFromAllItems();
                category.UpdateDateOfLastChanging();
                return !CurrentBook.CategoriesOfBook.Contains(category);
            }
            return false;       
        }

        public string RemoveCategoryFromAllItemsStr(Book CurrentBook)
        {
            if (Book.IsBookIsNotNull(CurrentBook))
                return $"Book of {this.Title} is null";
            return $"Removing all connections of {this.Title} is {RemoveCategoryFromAllItems(CurrentBook).ToString()}";
        }
        //public static string RemoveCategoryFromAllItemsStr(Book CurrentBook, Category category)
        //{
        //    if (Category.IsCategoryAndItsBookNotNull(CurrentBook, category))
        //        return "Smt is null";
        //    return $"Removing all connections of {category.Title} is {RemoveCategoryFromAllItems(category).ToString()}";
        //}

        public bool RemoveCategoryFromAllItems(Book CurrentBook)
        {
            if (Book.IsBookIsNotNull(CurrentBook))
            {
                CurrentBook.OnPropertyChanged(CurrentBook, "DateOfLastChanging");
                return CategoryInItem.DeleteAllConnectionWithCategory(CurrentBook, this);                                
            }
            return false;           
        }
        public static bool RemoveCategoryFromAllItems(Book CurrentBook, Category category)
        {
            if (Category.IsCategoryAndItsBookNotNull(CurrentBook, category))
            {
                CurrentBook.OnPropertyChanged(CurrentBook, "DateOfLastChanging");
                return CategoryInItem.DeleteAllConnectionWithCategory(CurrentBook, category);                                
            }
            return false;           
        }


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return (obj as Category).Title.Equals(Title);
        }        
        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Title.GetHashCode();
        }
        #endregion
    }

    internal static class ExtensionClass
    {
        /// <summary>
        /// Indicates whether word is not null and not empty
        /// </summary>
        /// <param name="word">string to test</param>
        /// <exception cref="ArgumentNullException">when word is null, empty or white symbols</exception>
        /// <returns></returns>
        public static bool IsStringNotNull(this string word)
        {
            return !string.IsNullOrWhiteSpace(word) ? true : (Base.ProjectType == TypeOfRunningProject.Xamarin ? false : throw new ArgumentNullException());
        }

        /// <summary>
        /// Indicates whether a collection is not empty
        /// </summary>
        /// <typeparam name="T">any class</typeparam>
        /// <param name="collection">collection to test</param>
        /// <exception cref="EmptyCollectionException">When collection is empty</exception>
        /// <returns></returns>
        public static bool IsNotEmptyCollection<T>(this IList<T> collection)
        {
            return collection.Count > 0 ? true : (Base.ProjectType == TypeOfRunningProject.Xamarin  ? false : throw new EmptyCollectionException());
        }
      
    }    
}
