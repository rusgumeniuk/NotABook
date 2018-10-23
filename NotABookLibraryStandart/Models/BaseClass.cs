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
        /// A field that show is project in Xamarin mode. "True" - to block Exceptions
        /// </summary>        
        public static bool IsXamarinProjectDeploying { get; set; } = false;

        /// <summary>
        /// A filed tha show is project in testing mode. "True" - to block OnPropertyChange
        /// </summary>
        public static bool IsTesingProjectRunning { get; set; } = true;

        public Guid Id { get; private set; }

        public virtual string Title
        {
            get => title;
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    title = value;
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
            OnPropertyChanged("New element");
        }

        public BaseClass(string title) : this()
        {
            Title = title;
        }
        #endregion

        #region Methods

        public abstract bool IsNotNull();

        public static bool IsGuidIsNotEmpty(Guid id)
        {
            return id != Guid.Empty ? true : (IsXamarinProjectDeploying ? false : throw new EmptyGuidException());
        }

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
            obj?.UpdateDateOfLastChanging();
        }

        public override string ToString()
        {
            return $"{ this.GetType().Name}: {Title}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string prop = "")
        {
            if (!IsTesingProjectRunning)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
                    UpdateDateOfLastChanging();
                }
            }           
        }

        public abstract bool Delete();

        #endregion
    }
}
