using System;
using System.Collections.Generic;
using System.Text;

namespace NotABook.Models
{
   public class BaseClass
    {
        #region Prop
        public Guid Id { get; private set; }

        public string Title { get; set; }

        public DateTime DateOfCreating { get; private set; }

        public DateTime DateOfLastChanging { get; protected set; }
        #endregion

        #region Constr
        public BaseClass()
        {
            Id = Guid.NewGuid();
            DateOfCreating = DateTime.Now;
            DateOfLastChanging = DateTime.Now;
        }

        public BaseClass(string title) : this()
        {
            Title = title;
        }
        #endregion

        public override string ToString()
        {
            return $"{ this.GetType().Name}: {Title}";
        }
    }
}
