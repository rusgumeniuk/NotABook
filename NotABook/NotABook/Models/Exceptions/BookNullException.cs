using System;
using System.Collections.Generic;
using System.Text;

namespace NotABook.Models.Exceptions
{
    public  class BookNullException : ArgumentNullException
    {
        public BookNullException(string message) : base(message) { }
    }
}
