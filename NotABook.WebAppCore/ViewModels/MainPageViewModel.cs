using NotABookLibraryStandart.Models.BookElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotABook.WebAppCore.ViewModels
{
    public class MainPageViewModel
    {
        public MainPageViewModel(Book currentBook, IEnumerable<Book> books, Note currentNote)
        {
            CurrentBook = currentBook;
            Books = books;
            CurrentNote = currentNote;
        }

        public Book CurrentBook { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public Note CurrentNote { get; set; }        
    }
}
