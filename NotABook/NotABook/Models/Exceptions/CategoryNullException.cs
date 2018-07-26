using System;
using System.Collections.Generic;
using System.Text;

namespace NotABook.Models.Exceptions
{
    public class CategoryNullException : ArgumentNullException
    {
        public CategoryNullException(string message) : base(message) { }
    }
}
