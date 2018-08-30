using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Exceptions
{
    /// <summary>
    /// The exception that thrown when the collection already contains the element
    /// </summary>
    public class ElementAlreadyExistException : ArgumentException
    {
        public ElementAlreadyExistException() : base() { }
        public ElementAlreadyExistException(string message) : base(message) { }
    }
}
