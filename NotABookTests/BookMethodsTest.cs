using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotABook.Models;
using System.Collections.ObjectModel;
namespace NotABookTests
{
    [TestClass]
    public class BookMethodsTest
    {

        Book CurrentBook = null;
        Item FirstItem = null;
        Item SecondItem = null;
        Category FirstCategory = null;
        Category SecondCategory = null;



        private void StartFunction()
        {
            CurrentBook = new Book("CurBook");

            FirstCategory = new Category(CurrentBook, "Sweet");
            SecondCategory = new Category(CurrentBook, "Salt");

            FirstItem = new Item(CurrentBook, "1 item", "desc1", new ObservableCollection<Category>() { FirstCategory });
            SecondItem = new Item(CurrentBook, "2 item", "descript 2", new ObservableCollection<Category>() { SecondCategory, FirstCategory });
        }
    }
}
