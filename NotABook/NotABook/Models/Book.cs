using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NotABook.Models
{
    public class Book : BaseClass
    {
        public ObservableCollection<Item> ItemsOfBook { get; set; } = new ObservableCollection<Item>();
        public ObservableCollection<Category> CategoriesOfBook { get; set; } = new ObservableCollection<Category>();
        
        public Book() : base()
        {
            NotABook.App.Books.Add(this);
        }
        public Book(string title) : base(title)
        {
            NotABook.App.Books.Add(this);
        }
    }
}
