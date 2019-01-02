using System;

namespace NotABookLibraryStandart.Models
{
    public class Entity : Base
    {
        private string title;
        public virtual string Title
        {
            get => title;
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    title = value;
                }
            }
        }

        public Entity() : base() { }
        public Entity(string title) : base()
        {
            Title = title;
        }

        public override string ToString()
        {
            return base.ToString() + ": " + title;
        }
    }
}
