using NotABookLibraryStandart.Models.BookElements.Contents;

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NotABookLibraryStandart.Models.BookElements
{
    public class Note : BookElement
    {
        public IList<Content> NoteContents { get; set; } = new ObservableCollection<Content>();

        #region prop             
        public bool IsHasNotContent
        {
            get
            {
                foreach (var content in NoteContents)
                {
                    if (!content.IsEmptyContent())
                        return false;
                }
                return true;
            }
        }
        public string TitleFromContent
        {
            get
            {
                foreach (var content in NoteContents)
                {
                    if (content.GetTitleFromContent() != null)
                        return content.GetTitleFromContent();
                }
                return null;
            }
        }
        public TextContent FirstTextContent
        {
            get => IndexOfTextContent != -1 ? NoteContents[IndexOfTextContent] as TextContent : null;
        }
        public int IndexOfTextContent
        {
            get
            {
                for (int i = 0; i < NoteContents.Count; i++)
                {
                    if (NoteContents[i] is TextContent)
                        return i;
                }
                return -1;
            }
        }
        #endregion

        #region ctors
        public Note(string title) : base(title) { }
        public Note(string title, IList<Content> contents) : this(title)
        {
            NoteContents = contents;
        }
        #endregion

        public override bool IsContainsText(string text)
        {
            if (base.IsContainsText(text))
                return true;
            foreach (var content in NoteContents)
            {
                if (content.IsContainsText(text))
                    return true;
            }
            return false;
        }

        public void AddContent(Content content)
        {
            NoteContents.Add(content);
        }
        public void RemoveContent(Content content)
        {
            NoteContents.Remove(content);
        }
        public bool IsContainsContent(Content content)
        {
            return NoteContents.Contains(content);
        }
    }
}
