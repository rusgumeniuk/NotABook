using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Exceptions
{
    /// <summary>
    /// /// The exception that thrown when a empty guid is passed to a method that does not accept as a valid argument
    /// </summary>
    public class EmptyGuidException : ArgumentException
    {
        public EmptyGuidException() : base() { }
        public EmptyGuidException(string message) : base(message) { }
    }
}
