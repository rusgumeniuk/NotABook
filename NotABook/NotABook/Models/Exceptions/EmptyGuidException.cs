using System;
using System.Collections.Generic;
using System.Text;

namespace NotABook.Models.Exceptions
{
    public class EmptyGuidException : ArgumentException
    {
        public EmptyGuidException() : base() { }
        public EmptyGuidException(string message) : base(message) { }
    }
}
