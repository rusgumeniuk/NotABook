using NotABookLibraryStandart.Models.BookElements.Contents;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Models.BookElements.Notes
{
    public class Note : Entity
    {
        private IList<IContent> contents = new List<IContent>();

        public object GetContent()
        {
            List<object> list = new List<object>();
            foreach (IContent item in contents)
            {
                list.Add(item.Content);

            }
            return list;
        }

    }
}
