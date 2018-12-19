using NotABookLibraryStandart.Models.BookElements.Contents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NotABookLibraryStandart.Models.BookElements
{
    public class Note : BookElement
    {
        private IList<IContent> noteContents = new ObservableCollection<IContent>();


        #region prop
        public IList<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public string CategoriesStr
        {
            get
            {
                if (Categories.Count < 1)
                    return null;
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var category in Categories)
                {
                    stringBuilder.Append(category.Title).Append(", ");
                }
                return stringBuilder.Remove(stringBuilder.Length - 2, 2).Append(".").ToString();
            }
        }
        public IList<IContent> Contents
        {
            get => noteContents;
            set
            {
                noteContents.Clear();
                foreach (var content in value)
                {
                    noteContents.Add(content);
                }
            }
        }
        public bool IsHasNotContent
        {
            get
            {
                foreach (var content in noteContents)
                {
                    if (!content.IsEmptyContent())
                        return true;
                }
                return false;
            }
        }
        public string TitleFromContent
        {
            get
            {
                foreach (var content in noteContents)
                {
                    if (content.GetTitleFromContent() != null)
                        return content.GetTitleFromContent();
                }
                return null;
            }
        }
        public TextContent FirstTextContent
        {
            get => noteContents[IndexOfTextContent] as TextContent;
        }
        public string SetTextToFirstTextContent
        {
            set
            {
                if (IndexOfTextContent != -1)
                {
                    (noteContents[IndexOfTextContent] as TextContent).Content = value;
                }
                else
                    throw new ArgumentException("U shouldn't see this");
            }
        }
        public int IndexOfTextContent
        {
            get
            {
                for (int i = 0; i < noteContents.Count; i++)
                {
                    if (noteContents[i] is TextContent)
                        return i;
                }
                return -1;
            }
        }

        #endregion

        #region ctors
        public Note(Book book) : base(book)
        {
            Contents.Add(new TextContent());
        }

        public Note(Book book, string title) : base(book, title)
        {
            Contents.Add(new TextContent());
        }

        public Note(Book book, string title, IList<IContent> contents) : this(book, title)
        {
            Contents = contents;
        }

        public Note(Book book, string title, IList<IContent> contents, IList<Category> categories) : this(book, title, contents)
        {
            Categories = categories;
        }
        #endregion

        public void SetCategories(Book CurrentBook, IEnumerable<Category> categories)
        {
            if (IsNoteAndBookNotNull(CurrentBook, this))
            {
                if (categories != null)
                {
                    // CategoryInItem.DeleteAllConnectionWithItem(CurrentBook, this);

                    foreach (var category in categories)
                    {
                        //CategoryInItem.CreateCategoryInItem(CurrentBook, category, this);
                    }

                    UpdateDateOfLastChanging();
                }
                else if (ProjectType != TypeOfRunningProject.Xamarin)
                    throw new ArgumentNullException();
            }
        }

        public IList<Category> GetCategories(Book CurrentBook)
        {
            if (Book.IsBookIsNotNull(CurrentBook) /*&& CategoryInItem.IsItemHasConnection(CurrentBook, this)*/)
            {
                IList<Category> categories = new List<Category>();
                foreach (CategoryInItem pair in CurrentBook.CategoryInItemsOfBook)
                {
                    if (pair.GetItemId == Id)
                    {
                        categories.Add(pair.Category);
                    }
                }
                return categories;
            }
            return null;
        }


        public static bool IsNoteAndBookNotNull(Book CurrentBook, Note note)
        {
            return IsNoteNotNull(note) && Book.IsBookIsNotNull(CurrentBook);
        }

        public static bool IsNoteNotNull(Note note)
        {
            return note != null ? true : (ProjectType == TypeOfRunningProject.Xamarin ? false : throw new ArgumentNullException());
        }

        public void AddContent(IContent content)
        {
            noteContents.Add(content);
        }

        public void RemoveContent(IContent content)
        {
            noteContents.Remove(content);
        }

        public bool IsContainContant(IContent content)
        {
            return noteContents.Contains(content);
        }
    }
}
