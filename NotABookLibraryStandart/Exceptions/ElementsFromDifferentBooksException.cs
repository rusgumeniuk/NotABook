using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Exceptions
{
    /// <summary>
    /// The exception that thrown when some arguments belong to different collections
    /// </summary>
    public class ElementsFromDifferentBooksException : ArgumentException
    {
        public ElementsFromDifferentBooksException() : base() { }
        public ElementsFromDifferentBooksException(string message) : base(message) { }
    }
}
