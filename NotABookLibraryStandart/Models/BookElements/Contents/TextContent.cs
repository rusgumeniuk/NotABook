using NotABookLibraryStandart.Models.BookElements;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Models.BookElements.Contents
{
    public class TextContent : Content
    {
        private string content;
        public object Content
        {
            get => content;
            set
            {
                content = value?.ToString();
            }
        }

        public override object Clone()
        {
            TextContent clone = this.MemberwiseClone() as TextContent;
            clone.Content = this.content.Clone() as byte[];
            return clone;
        }
    }
}
