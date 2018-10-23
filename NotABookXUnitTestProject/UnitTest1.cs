using System;
using Xunit;
using NotABookLibraryStandart.Models;
using NotABookLibraryStandart.Exceptions;
using System.Collections.ObjectModel;

namespace NotABookXUnitTestProject
{
    public class UnitTest1
    {
        Book CurrentBook = null;
        Item FirstItem = null;
        Item SecondItem = null;
        Category FirstCategory = null;
        Category SecondCategory = null;

        private void SetUp()
        {
            Book.Books.Clear();

            CurrentBook = new Book("CurBook");
            
            FirstCategory = new Category(CurrentBook, "Sweet");
            SecondCategory = new Category(CurrentBook, "Salt");

            FirstItem = new Item(CurrentBook, "1 item", Description.CreateDescription("desc1"), new ObservableCollection<Category>() { FirstCategory });
            SecondItem = new Item(CurrentBook, "2 item", Description.CreateDescription("descript 2"), new ObservableCollection<Category>() { SecondCategory, FirstCategory });
        }

        [Fact]
        public void Test1()
        {
            SetUp();
           // BaseClass.IsTesingProjectRunning = false;
           // BaseClass.IsXamarinProjectDeploying = false;

            DateTime lastUpd = FirstItem.DateOfLastChanging;

            FirstItem.Title = "NEW TITLE";
            Assert.False(lastUpd.Equals(FirstItem.DateOfLastChanging));
        }

        
    }
}
