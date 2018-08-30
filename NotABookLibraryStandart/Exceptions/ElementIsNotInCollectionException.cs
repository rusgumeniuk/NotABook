using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NotABookLibraryStandart.Exceptions
{
    /// <summary>
    /// The exception that thrown when collection does not contains element
    /// </summary>
    public class ElementIsNotInCollectionException : Exception
    {
        public ElementIsNotInCollectionException() : base() { }
        public ElementIsNotInCollectionException(string message) : base(message) { }
        public ElementIsNotInCollectionException(string message, Exception inner) : base(message, inner) { }
        protected ElementIsNotInCollectionException(SerializationInfo info, StreamingContext strContext)
        {

        }
    }
}
