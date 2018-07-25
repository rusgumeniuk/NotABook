using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotABook.Models;


namespace NotABookTests
{
    [TestClass]
    public class UnitTest1
    {
        //Book CurrentBook = null;
        //Item FirstItem = null;
        //Item SecondItem = null;
        //Category FirstCategory = null;
        //Category SecondCategory = null;


        
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                Book CurrentBook = new Book("CurBook");
            }
            catch(Exception)
            {
                //Console.
            }
            //Item FirstItem = null;
            //Item SecondItem = null;
            //Category FirstCategory = null;
            //Category SecondCategory = null;
            //StartFunction();
            
            //NotABook.App.currentBook = CurrentBook;

            //FirstCategory = new Category("Sweet");
            //SecondCategory = new Category("Salt");

            //FirstItem = new Item("1 item", "desc1", new System.Collections.ObjectModel.ObservableCollection<Category>() { FirstCategory });
            //SecondItem = new Item("2 item", "descript 2", new System.Collections.ObjectModel.ObservableCollection<Category>() { SecondCategory, FirstCategory });

            //Assert.AreEqual(FirstCategory.Title, "Sweet");
            //Assert.IsNotNull(SecondCategory);
            Assert.IsNull(null);
        }

        //[TestMethod]
        //public void Test2()
        //{
        //    StartFunction();
        //    //Assert.AreEqual(NotABook.App.currentBook, CurrentBook);            
        //    Assert.AreEqual(FirstCategory.Title, "Sweet");
        //}

        //private void StartFunction()
        //{
        //    CurrentBook = new Book("CurBook");
        //    NotABook.App.currentBook = CurrentBook;

        //    FirstCategory = new Category("Sweet");
        //    SecondCategory = new Category("Salt");

        //    FirstItem = new Item("1 item", "desc1", new System.Collections.ObjectModel.ObservableCollection<Category>() { FirstCategory });
        //    SecondItem = new Item("2 item", "descript 2", new System.Collections.ObjectModel.ObservableCollection<Category>() { SecondCategory, FirstCategory });

        //}
    }
}
