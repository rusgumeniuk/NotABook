using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Models.BookElements.Contents
{
    public interface IContent
    {
        object Content { get; set; }
        bool IsEmptyContent();
        string GetTitleFromContent();
        bool IsContainsText(string text);
    }
}
