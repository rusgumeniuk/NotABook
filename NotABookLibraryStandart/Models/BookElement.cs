using System;
using System.Collections.Generic;
using System.Text;
using NotABookLibraryStandart.Exceptions;

namespace NotABookLibraryStandart.Models
{
    /// <summary>
    /// Represents elements of the book (for example, category, item)
    /// </summary>
    public abstract class BookElement : Base
    {
        protected Book currentBook;
        public Book CurrentBook
        {
            get => currentBook;
            protected set
            {
                currentBook = value;
                OnPropertyChanged("CurrentBook");
            }
        }

        public BookElement(Book book) : base()
        {
            CurrentBook = Book.IsBookIsNotNull(book) ? book : new Book("NULL BOOK");
        }
        public BookElement(Book book, string title) : base(title)
        {
            CurrentBook = Book.IsBookIsNotNull(book) ? book : new Book("NULL BOOK");
        }

        public override void OnPropertyChanged(string prop = "")
        {
            base.OnPropertyChanged(prop);

            if (currentBook != null)
                Book.UpdateDateOfLastChanging(currentBook);
            else if (ProjectType != ProjectType.Xamarin && currentBook != null)
                throw new Exceptions.BookNullException();
        }
    }
}
