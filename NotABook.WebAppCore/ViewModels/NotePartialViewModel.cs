using NotABookLibraryStandart.Models.BookElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotABook.WebAppCore.ViewModels
{
    public class NotePartialViewModel
    {
        public Note Note { get; set; }
        public IEnumerable<Category> AllCategories { get; set; }
        public IEnumerable<Category> NoteCategories { get; set; }
        public Book NoteBook { get; set; }
        public IEnumerable<Book> AllBooks { get; set; }
    }
}
