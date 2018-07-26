using System;
using System.Collections.Generic;
using System.Text;

namespace NotABook.Models.Exceptions
{
    public class ElementIsNotInCollectionException : ArgumentException
    {
        public ElementIsNotInCollectionException() : base() { }
        public ElementIsNotInCollectionException(string message) : base(message) { }
    }
}
