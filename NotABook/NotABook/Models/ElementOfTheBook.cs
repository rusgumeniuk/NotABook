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
                if(IsTestingOff)
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
            if (IsTestingOff)
                CurrentBook = book ?? new Book("NULL BOOK");
            else
                CurrentBook = book ?? throw new Exceptions.BookNullException();
        }
        public ElementOfTheBook(Book book, string title) : base(title)
        {
            if (IsTestingOff)
                CurrentBook = book ?? new Book("NULL BOOK");
            else
                CurrentBook = book ?? throw new Exceptions.BookNullException();
        }

        public new void OnPropertyChanged(string prop = "")
        {            
            base.OnPropertyChanged(prop);
            if (CurrentBook == null)
                throw new Exceptions.BookNullException();
            CurrentBook.DateOfLastChanging = DateTime.Now;
        }
    }
}
