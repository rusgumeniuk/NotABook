using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Exceptions
{
    /// <summary>
    /// The exception that thrown when a null reference of Category is passed to a method that does not accept as a valid argument
    /// </summary>
    public class CategoryNullException : ArgumentNullException
    {
        public CategoryNullException() : base() { }
        public CategoryNullException(string message) : base(message) { }
    }
}
