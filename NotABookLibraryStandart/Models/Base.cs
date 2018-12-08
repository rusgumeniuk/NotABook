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
    public abstract class Base : INotifyPropertyChanged
    {
        #region Prop
        /// <summary>
        /// A field that show current project mode.
        /// </summary>       
        public static TypeOfRunningProject ProjectType = TypeOfRunningProject.WPF;

        public Guid Id { get; private set; }      

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
        #endregion

        #region Methods
        
        public static bool IsGuidIsNotEmpty(Guid id)
        {
            return id != Guid.Empty ? true : (ProjectType == TypeOfRunningProject.Xamarin ? false : throw new EmptyGuidException());
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
            return $"{ this.GetType().Name}";
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

        public abstract bool Delete();

        public abstract void ThrowNullException();

        #endregion
    }
}
