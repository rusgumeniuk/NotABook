using System;
using System.Collections.Generic;
using System.Text;

namespace NotABook.Models.Exceptions
{
    public class ItemNullException : ArgumentNullException
    {
        public ItemNullException() : base() { }
        public ItemNullException(string message) : base(message) { }
    }
}
