using System;
using System.Collections.Generic;
using System.Text;
using NotABookLibraryStandart.Exceptions;
using System.Collections.ObjectModel;
using NotABookLibraryStandart.Models.BookElements;

namespace NotABookLibraryStandart.Models
{
    /// <summary>
    /// Represents a book, set of that's forms a notebook
    /// </summary>
    public class Book : BookElement
    {
        #region Fields       

        private ObservableCollection<Note> notes = new ObservableCollection<Note>();

        public static ObservableCollection<Book> Books = new ObservableCollection<Book>();
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

        private Book() : base(null)
        {
            Books.Add(this);
        }

        public Book(string title) : base(null, title)
        {
            Books.Add(this);
        }

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
        public IList<Note> FindNotes(string text)
        {
            if (ExtensionClass.IsStringNotNull(text))
            {
                IList<Note> result = new List<Note>();
                foreach (var note in Notes)
                {
                    if (note.IsContainsText(text))
                        result.Add(note);
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// Removes all elements of the book and remove book from the Books
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            if (IsBookIsNotNullAndInBooks(this))
            {
                RemoveAllElementsOfBook(this);
                Book.Books.Remove(this);

                return IndexOfBookInBooks(this) == -1;
            }
            return false;
        }

        /// <summary>
        /// Removes all elements of the book and remove book from the Books
        /// </summary>
        /// <param name="book"> book to delete</param>
        /// <returns></returns>
        public static bool DeleteBook(Book book)
        {
            if (IsBookIsNotNullAndInBooks(book))
            {
                RemoveAllElementsOfBook(book);
                Book.Books.Remove(book);

                return IndexOfBookInBooks(book) == -1;
            }
            return false;
        }


        /// <summary>
        /// Indicates whether the Books contains book
        /// </summary>
        /// <param name="book">book to test</param>
        /// <exception cref="ElementIsNotInCollectionException">when Book doesn't contain book</exception>
        /// <exception cref="BookNullException">when book is null</exception>
        /// <returns></returns>
        public static bool IsBooksContainsThisBook(Book book)
        {
            return IndexOfBookInBooks(book) != -1 ? true : (ProjectType == TypeOfRunningProject.Xamarin ? false : throw new ElementIsNotInCollectionException());
        }

        /// <summary>
        /// Indicates whether the Books contains book with bookId
        /// </summary>
        /// <param name="bookId">id to test</param>
        /// <exception cref="ElementIsNotInCollectionException">when Book doesn't contain book with same id</exception>       
        /// <exception cref="EmptyGuidException">when bookId is empty</exception>
        /// <returns></returns>
        public static bool IsBooksContainsThisBook(Guid bookId)
        {
            return IndexOfBookInBooks(bookId) != -1 ? true : (ProjectType == TypeOfRunningProject.Xamarin ? false : throw new ElementIsNotInCollectionException());
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

        /// <summary>
        /// Indicates whether the book is not null and is Books contains book
        /// </summary>        
        /// <param name="book">The book to test</param>
        /// <exception cref="BookNullException">When book is null  and Xamarin mode is off</exception>
        /// <exception cref="ElementIsNotInCollectionException">When Books doesn't contain book</exception>
        /// <returns>true if book is not null. Else if Xamarin mode is on - false.</returns>
        public static bool IsBookIsNotNullAndInBooks(Book book)
        {
            return IsBookIsNotNull(book) && IsBooksContainsThisBook(book);
        }


        internal static int IndexOfBookInBooks(Book book)
        {
            if (Book.IsBookIsNotNull(book))
            {
                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i] == book)
                        return i;
                }
            }
            return -1;
        }
        internal static int IndexOfBookInBooks(Guid bookId)
        {
            if (IsGuidIsNotEmpty(bookId))
            {
                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i].Id == bookId)
                        return i;
                }
            }
            return -1;
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
