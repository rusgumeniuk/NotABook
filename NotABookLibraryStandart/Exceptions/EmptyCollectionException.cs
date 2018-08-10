using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Exceptions
{
    public class EmptyCollectionException : Exception
    {
        public EmptyCollectionException() { }
        public EmptyCollectionException(string message) : base(message) { }    //  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
