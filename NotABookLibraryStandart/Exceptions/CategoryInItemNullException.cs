using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Exceptions
{
    /// <summary>
    /// The exception that thrown when a null reference of CategoryInItem is passed to a method that does not accept as a valid argument
    /// </summary>
    public class CategoryInItemNullException : ArgumentNullException
    {
        public CategoryInItemNullException() : base() { }
        public CategoryInItemNullException(string message) : base(message) { }
    }
}
