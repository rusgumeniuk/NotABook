using System;
using System.ComponentModel;

namespace NotABook.Models
{
   abstract public class BaseClass : INotifyPropertyChanged
    {
        #region Fields

        protected static bool IsTestingOff = true; //To start testing project set "false". 
        protected string title;

        #endregion

        #region Prop

        public Guid Id { get; private set; }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                if(IsTestingOff)
                    OnPropertyChanged("Title");
            }
        }

        public DateTime DateOfCreating { get; private set; }

        public DateTime DateOfLastChanging { get; internal set; }

        #endregion

        #region Constr
        public BaseClass()
        {
            Id = Guid.NewGuid();
            DateOfCreating = DateTime.Now;
            DateOfLastChanging = DateTime.Now;
            if (IsTestingOff)
                OnPropertyChanged("New element");
        }

        public BaseClass(string title) : this()
        {
            Title = title;
        }
        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{ this.GetType().Name}: {Title}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
                this.DateOfLastChanging = DateTime.Now;                
            }                
        }

        public void OnPropertyChanged(Book book, string prop = "")
        {
            if (PropertyChanged != null)
            {
                if (book == null)
                    throw new Exceptions.BookNullException();

                PropertyChanged(this, new PropertyChangedEventArgs(prop));
                DateOfLastChanging = DateTime.Now;
                book.DateOfLastChanging = DateTime.Now;
            }

        }

        #endregion
    }
}
