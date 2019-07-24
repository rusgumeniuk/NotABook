using System.ComponentModel.DataAnnotations.Schema;

namespace NotABookLibraryStandart.Models.BookElements.Contents
{
    public abstract class Content : Base, IContent
    {
        [NotMapped]
        object IContent.Content { get; set; }

        public abstract string GetTitleFromContent();
        public abstract bool IsContainsText(string text);
        public abstract bool IsEmptyContent();
    }
}
