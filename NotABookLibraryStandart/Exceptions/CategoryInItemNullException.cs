using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Exceptions
{
    public class CategoryInItemNullException : ArgumentNullException
    {
        public CategoryInItemNullException() : base() { }
        public CategoryInItemNullException(string message) : base(message) { }
    }
}
