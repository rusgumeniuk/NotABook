using NotABookLibraryStandart.Exceptions;

using System;
using System.Collections.Generic;

namespace NotABookLibraryStandart.Models.BookElements
{
    /// <summary>
    /// Represents a category of the book
    /// </summary>
    public class Category : BookElement
    {
        #region Constr
        public Category() : base() { }
        public Category(string title) : base(title) { }
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
            return category != null ? true : (ProjectType == TypeOfRunningProject.Xamarin ? false : throw new CategoryNullException());
        }

        /// <summary>
        /// Indicates whether the category and its book is not null
        /// </summary>
        /// <param name="category">category to test</param>
        /// <exception cref="CategoryNullException">When category is null</exception>
        /// <exception cref="BookNullException">When book of note is null</exception>
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
            return collection.Count > 0 ? true : (Base.ProjectType == TypeOfRunningProject.Xamarin ? false : throw new EmptyCollectionException());
        }
    }
}
