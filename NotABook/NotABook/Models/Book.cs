using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NotABook.Models
{
    public class Book : BaseClass
    {
        public Dictionary<Guid, Item> ItemsOfBook { get; set; } = new Dictionary<Guid, Item>();
        public Dictionary<Guid, Category> CategoriesOfBook { get; set; } = new Dictionary<Guid, Category>();

        public ObservableCollection<Item> ItemsList => new ObservableCollection<Item>(ItemsOfBook.Values.ToList());
        public ObservableCollection<Category> CategoriesList => new ObservableCollection<Category>(CategoriesOfBook.Values.ToList());

        public Book() : base()
        {
            NotABook.App.Books.Add(this.Id, this);
        }
        public Book(string title) : base(title)
        {
            NotABook.App.Books.Add(this.Id, this);
        }
    }
}
