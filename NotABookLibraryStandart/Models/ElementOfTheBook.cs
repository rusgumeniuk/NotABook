using System;
using System.Collections.Generic;
using System.Text;
using NotABookLibraryStandart.Exceptions;

namespace NotABookLibraryStandart.Models
{
    /// <summary>
    /// Represents elements of the book (for example, category, item)
    /// </summary>
    public abstract class ElementOfTheBook : BaseClass
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

        public ElementOfTheBook(Book book) : base()
        {
            CurrentBook = book ?? (IsXamarinProjectDeploying ? new Book("NULL BOOK") : throw new BookNullException());
        }
        public ElementOfTheBook(Book book, string title) : base(title)
        {
            CurrentBook = book ?? (IsXamarinProjectDeploying ? new Book("NULL BOOK") : throw new BookNullException());
        }

        public override void OnPropertyChanged(string prop = "")
        {
            base.OnPropertyChanged(prop);

            if (currentBook != null)
                Book.UpdateDateOfLastChanging(currentBook);
            else if (!IsXamarinProjectDeploying && currentBook != null)
                throw new Exceptions.BookNullException();
        }
    }
}
