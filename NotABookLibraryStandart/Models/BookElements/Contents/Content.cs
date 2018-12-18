using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Models.BookElements.Contents
{
    public abstract class Content : Base, IContent, ICloneable
    {
        object IContent.Content{ get; set; }

        public abstract object Clone();
        public abstract string GetTitleFromContent();
        public abstract bool IsEmptyContent();
    }
}
