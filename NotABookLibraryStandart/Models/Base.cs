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
    abstract public class Base : INotifyPropertyChanged
    {
        #region Fields

        protected string title;

        #endregion

        #region Prop
        public static ProjectType ProjectType = ProjectType.WPF;

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
        public Base()
        {
            Id = Guid.NewGuid();
            DateOfCreating = DateTime.Now;
            DateOfLastChanging = DateTime.Now;
            OnPropertyChanged("New element");
        }

        public Base(string title) : this()
        {
            Title = title;
        }
        #endregion

        #region Methods

        public static bool IsGuidIsNotEmpty(Guid id)
        {
            return id != Guid.Empty ? true : (ProjectType == ProjectType.Xamarin ? false : throw new EmptyGuidException());
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
        protected static void UpdateDateOfLastChanging(Base obj)
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
            if (ProjectType != ProjectType.Testing)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
                    UpdateDateOfLastChanging();
                }
            }
        }

        public abstract bool Delete();

        public virtual void ThrowNullException() { }

        #endregion
    }
}
