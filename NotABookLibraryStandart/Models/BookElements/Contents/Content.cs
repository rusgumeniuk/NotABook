using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Models.BookElements.Contents
{
    public abstract class Content : Base, IContent, ICloneable
    {
        object IContent.Content { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public abstract object Clone();
    }
}
