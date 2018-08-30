using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using NotABookLibraryStandart.Exceptions;

namespace NotABookLibraryStandart.Models
{
    /// <summary>
    /// Represent a basic class for elements of the notebook
    /// </summary>
    abstract public class BaseClass : INotifyPropertyChanged
    {
        #region Fields
       
        protected string title;

        #endregion

        #region Prop
        /// <summary>
        /// A field that show is project in testing mode. "True" - to start Xamarin, "False" - to start testing and WPF
        /// </summary>
        //public static bool IsXamarinProjectDeploying = false; //To start testing project set "false". 
        public static bool IsXamarinProjectDeploying { get; set; } = true;

        public Guid Id { get; private set; }

        public virtual string Title
        {
            get => title;
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    title = value;
                    if (IsXamarinProjectDeploying)
                        OnPropertyChanged("Title");
                }                   
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
            if (IsXamarinProjectDeploying)
                OnPropertyChanged("New element");
        }

        public BaseClass(string title) : this()
        {
            Title = title;
        }
        #endregion

        #region Methods

        /// <summary>
        /// The method that update date of last changing after any operation with object
        /// </summary>
        protected void UpdateDateOfLastChanging()
        {
            this.DateOfLastChanging = DateTime.Now;
        }

        /// <summary>
        /// The method that update date of last changing after any operation with obj
        /// </summary>
        protected static void UpdateDateOfLastChanging(BaseClass obj)
        {
            obj.UpdateDateOfLastChanging();
        }

        public override string ToString()
        {
            return $"{ this.GetType().Name}: {Title}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
                UpdateDateOfLastChanging();
            }
        }

        public virtual void OnPropertyChanged(Book book, string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
                UpdateDateOfLastChanging();


                if (book != null)
                    book.UpdateDateOfLastChanging();
                else if(!IsXamarinProjectDeploying)
                    throw new Exceptions.BookNullException();                               
                
            }
        }

        #endregion
    }
}
