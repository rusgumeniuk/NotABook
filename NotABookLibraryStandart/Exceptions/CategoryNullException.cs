using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Exceptions
{
    public class CategoryNullException : ArgumentNullException
    {
        public CategoryNullException() : base() { }
        public CategoryNullException(string message) : base(message) { }
    }
}
