using System;
using System.Collections.Generic;
using System.Text;

namespace NotABook.Models.Exceptions
{
    public class ElementAlreadyExistException : ArgumentException
    {
        public ElementAlreadyExistException(string message) : base(message) { }
    }
}
