using NotABookLibraryStandart.Exceptions;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NotABookLibraryStandart.Models.BookElements
{
    /// <summary>
    /// Represents a book, set of that's forms a notebook
    /// </summary>
    public class Book : BookElement
    {
        private ObservableCollection<Note> notes = new ObservableCollection<Note>();
        public ObservableCollection<Note> Notes
        {
            get => this.notes ?? (notes = new ObservableCollection<Note>());
            set
            {
                this.notes = value;
                UpdateDateOfLastChanging();
            }
        }

        private Book() { }

        public Book(string title) : base(title) { }

        public bool ChangeBook(Note note, Book newBook)
        {
            if (!Notes.Contains(note) || this.Equals(newBook))
                throw new ArgumentException("Oooops, wrong argument!");
            else
            {
                Notes.Remove(note);
                newBook.Notes.Add(note);
            }
            return !Notes.Contains(note) && newBook.Notes.Contains(note);
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
        public static bool IsBookContainsNote(Book book, Guid noteId)
        {
            return book.GetIndexOfNoteByID(noteId) > -1 ? true : (ProjectType == TypeOfRunningProject.Xamarin ? false : throw new ElementIsNotInCollectionException());
        }

        public override bool IsContainsText(string text)
        {
            return base.IsContainsText(text) || Notes.FirstOrDefault(note => note.IsContainsText(text)) != null;
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
    }
}
