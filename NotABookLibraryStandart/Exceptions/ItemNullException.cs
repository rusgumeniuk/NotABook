using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Exceptions
{
    /// <summary>
    /// The exception that thrown when a null reference of Item is passed to a method that does not accept as a valid argument
    /// </summary>
    public class ItemNullException : ArgumentNullException
    {
        public ItemNullException() : base() { }
        public ItemNullException(string message) : base(message) { }
    }
}
