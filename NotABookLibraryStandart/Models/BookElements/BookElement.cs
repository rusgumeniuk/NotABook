using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using NotABookLibraryStandart.Exceptions;
using NotABookLibraryStandart.Models.BookElements;

namespace NotABookLibraryStandart.Models
{
    /// <summary>
    /// Represents elements of the book (for example, category, item)
    /// </summary>
    public abstract class BookElement : Entity, INotifyPropertyChanged
    {
        public DateTime DateOfCreating { get; private set; }
        public DateTime DateOfLastChanging { get; internal set; }

        public override string Title
        {
            get => base.Title;
            set
            {
                base.Title = value;
                UpdateDateOfLastChanging();
            }
        }

        public BookElement() : base()
        {
            DateOfCreating = DateTime.Now;
            DateOfLastChanging = DateTime.Now;           
        }
        public BookElement( string title) : base(title)
        {
            DateOfCreating = DateTime.Now;
            DateOfLastChanging = DateTime.Now;
        }

        protected void UpdateDateOfLastChanging()
        {
            this.DateOfLastChanging = DateTime.Now;
        }

        /// <summary>
        /// The method that update date of last changing after any operation with obj
        /// </summary>
        protected static void UpdateDateOfLastChanging(BookElement bookElement, Book book)
        {
            bookElement?.UpdateDateOfLastChanging();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(Book currentBook, string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
                UpdateDateOfLastChanging();
                currentBook?.UpdateDateOfLastChanging();
            }
        }
    }
}
