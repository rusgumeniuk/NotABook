using NotABookLibraryStandart.Exceptions;
using System;

namespace NotABookLibraryStandart.Models
{
    /// <summary>
    /// Represent a basic class for elements of the notebook
    /// </summary>
    public abstract class Base
    {
        #region Prop
        /// <summary>
        /// A field that show current project mode.
        /// </summary>       
        public static TypeOfRunningProject ProjectType = TypeOfRunningProject.WPF;

        public Guid Id { get; set; }      


        #endregion

        #region Constr
        public Base()
        {
            Id = Guid.NewGuid();
            
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
      
        public override string ToString()
        {
            return $"{ this.GetType().Name}";
        }
        #endregion
    }
}
