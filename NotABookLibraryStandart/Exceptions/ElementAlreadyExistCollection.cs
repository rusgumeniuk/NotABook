using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Exceptions
{
    public class ElementAlreadyExistException : ArgumentException
    {
        public ElementAlreadyExistException() : base() { }
        public ElementAlreadyExistException(string message) : base(message) { }
    }
}
