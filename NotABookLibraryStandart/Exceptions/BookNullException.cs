using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Exceptions
{
    /// <summary>
    /// The exception that thrown when a null reference of Book is passed to a method that does not accept as a valid argument
    /// </summary>
    public class BookNullException : ArgumentNullException
    {
        public BookNullException() : base() { }
        public BookNullException(string message) : base(message) { }
    }
}
