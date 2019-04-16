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
        private Category() { }
        public Category(string title) : base(title) { }                   
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
