using System;
using System.Collections.Generic;
using System.Text;
using NotABookLibraryStandart.Exceptions;
using System.Collections.ObjectModel;
using NotABookLibraryStandart.Models.BookElements;

namespace NotABookLibraryStandart.Models.BookElements
{
    /// <summary>
    /// Represents a book, set of that's forms a notebook
    /// </summary>
    public class Book : BookElement
    {
        #region Fields       
        private ObservableCollection<Note> notes = new ObservableCollection<Note>();
        public Book() { }
        public Book(string title) : base(title) { }
        #endregion

        #region Propereties            
        public ObservableCollection<Note> Notes
        {
            get => this.notes;
            set
            {
                this.notes = value;
                UpdateDateOfLastChanging();
            }
        }

        #endregion

        #region Constr       

        #endregion

        #region Methods

        public bool RemoveBookElement(BookElement bookElement)
        {
            if (bookElement is Note)
                return notes.Remove(bookElement as Note);

            return false;
        }
        /// <summary>
        /// Represents a list of notes which contain text in Title, categories or contents
        /// </summary>
        /// <param name="text"></param>
        /// <exception cref="ArgumentNullException">when text is null, empty or white spaces</exception>
        /// <returns></returns>
        public IList<Note> FindNotes(string text, IEnumerable<LinkNoteCategory> connections)
        {
            if (ExtensionClass.IsStringNotNull(text))
            {
                text = text.ToUpperInvariant();
                IList<Note> result = new List<Note>();
                foreach (var note in Notes)
                {
                    if (note.IsContainsText(text))
                        result.Add(note);
                }
                foreach (var connection in connections)
                {
                    if (connection.Category.Title.ToUpperInvariant().Contains(text) && (!result.Contains(connection.Note)))
                        result.Add(connection.Note);
                }
                return result;
            }
            return null;
        }

       
        public bool Delete()
        {
            return false;
        }

      
        public static bool DeleteBook(Book book)
        {            
            return false;
        }

        /// <summary>
        /// Indicates whether the book contains note
        /// </summary>
        /// <param name="book">book in which we try to find note</param>
        /// <param name="note">note to test</param>
        /// <exception cref="ElementIsNotInCollectionException">when book doesn't contain note</exception>
        /// <exception cref="ItemNullException">when note is null</exception>
        /// <returns></returns>
        public static bool IsBookContainsNote(Book book, Note note)
        {
            return book.GetIndexOfNoteByID(note.Id) > -1 ? true : (ProjectType == TypeOfRunningProject.Xamarin ? false : throw new ElementIsNotInCollectionException($"{note.Title} not in the {book.Title}"));
        }

        /// <summary>
        /// Indicates whether the book contains note with noteId ID
        /// </summary>
        /// <param name="book">book in which we try to find</param>
        /// <param name="noteId">Id to test</param>
        ///   /// <exception cref="ElementIsNotInCollectionException">when book doesn't contain note</exception>
        /// <exception cref="EmptyGuidException">when noteId is empty</exception>
        /// <returns></returns>
        public static bool IsBookContainsItem(Book book, Guid noteId)
        {
            return book.GetIndexOfNoteByID(noteId) > -1 ? true : (ProjectType == TypeOfRunningProject.Xamarin ? false : throw new ElementIsNotInCollectionException());
        }

        /// <summary>
        /// Indicates whether the book is not null
        /// </summary>        
        /// <param name="book">The book to test</param>
        /// <exception cref="BookNullException">When book is null and Xamarin mode is off</exception>
        /// <returns>true if book is not null. Else if Xamarin mode is on - false.</returns>
        public static bool IsBookIsNotNull(Book book)
        {
            return book != null ? true : (ProjectType == TypeOfRunningProject.Xamarin ? false : throw new BookNullException());
        }

        public int GetIndexOfNoteByID(Guid noteId)
        {
            if (IsGuidIsNotEmpty(noteId))
            {
                for (int i = 0; i < Notes.Count; ++i)
                {
                    if (Notes[i].Id == noteId) return i;
                }
            }
            return -1;
        }
        public static int GetIndexOfNoteyID(Book book, Guid noteId)
        {
            if (Book.IsBookIsNotNull(book) && IsGuidIsNotEmpty(noteId))
            {
                for (int i = 0; i < book.Notes.Count; ++i)
                {
                    if (book.Notes[i].Id == noteId)
                        return i;
                }
            }
            return -1;
        }

        public static bool ClearNotesList(Book book)
        {
            if (Book.IsBookIsNotNull(book))
            {
                book.Notes.Clear();
            }
            return book.Notes.Count == 0;
        }

        public static bool RemoveAllElementsOfBook(Book book)
        {
            return Book.IsBookIsNotNull(book) ? ClearNotesList(book) : false;
        }
        #endregion
    }
}
