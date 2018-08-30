using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Exceptions
{
    /// <summary>
    /// The exception that thrown when a some collection does not containts any item
    /// </summary>
    public class EmptyCollectionException : Exception
    {
        public EmptyCollectionException() { }
        public EmptyCollectionException(string message) : base(message) { }    //  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
