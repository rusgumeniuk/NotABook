using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Exceptions
{
    public class ElementsFromDifferentBooksException : ArgumentException
    {
        public ElementsFromDifferentBooksException() : base() { }
        public ElementsFromDifferentBooksException(string message) : base(message) { }
    }
}
