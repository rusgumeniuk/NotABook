using System;
using System.Collections.Generic;
using System.Text;

namespace NotABook.Models
{
    public abstract class ElementOfTheBook : BaseClass
    {
        protected Book currentBook;
        public Book CurrentBook
        {
            get => currentBook;
            set
            {
                currentBook = value;
                OnPropertyChanged(currentBook, "CurrentBook");
            }
        }

        public new string Title
        {
            get => title;
            set
            {
                title = value;
                if (IsTestingOff)
                    OnPropertyChanged(CurrentBook, "Title");
            }
        }

        public ElementOfTheBook(Book book) : base()
        {
            CurrentBook = book ?? throw new Exceptions.BookNullException();
        }
        public ElementOfTheBook(Book book, string title) : base(title)
        {
            CurrentBook = book ?? throw new Exceptions.BookNullException();
        }
    }
}
