using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Exceptions
{
    public class BookNullException : ArgumentNullException
    {
        public BookNullException() : base() { }
        public BookNullException(string message) : base(message) { }
    }
}
