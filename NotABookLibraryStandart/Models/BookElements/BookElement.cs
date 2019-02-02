using NotABookLibraryStandart.Models.BookElements;

using System;
using System.ComponentModel;

namespace NotABookLibraryStandart.Models
{
    /// <summary>
    /// Represents element of the book (category, note or book)
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

        public BookElement(string title) : base(title)
        {
            DateOfCreating = DateTime.Now;
            DateOfLastChanging = DateTime.Now;
        }

        public virtual bool IsContainsText(string text)
        {
            return Title.ToUpperInvariant().Contains(text.ToUpperInvariant());
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
        protected void UpdateDateOfLastChanging()
        {
            this.DateOfLastChanging = DateTime.Now;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return (obj as BookElement).Title.Equals(Title) || Id.Equals((obj as BookElement).Id);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() | Id.GetHashCode() ^ Title.GetHashCode();
        }
    }
}
