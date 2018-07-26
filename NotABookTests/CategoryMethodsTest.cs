using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotABook.Models;
using System.Collections.ObjectModel;
namespace NotABookTests
{
    [TestClass]
    public class CategoryMethodsTest
    {
        #region StartedInfo
        Book CurrentBook = null;
        Item FirstItem = null;
        Item SecondItem = null;
        Category FirstCategory = null;
        Category SecondCategory = null;
        #endregion

        /*
         * TODO list:
         * ItemsWithThisCategory
         * RemoveCtegoryFromAllItems
         * DeleteCategory         
         */

        // [TestMethod]
        // public void ItemsWithCategory_WhenNotEmptyList_ReturnsTrueCount()
        //{
        //    StartFunction();

        //    Assert.AreEqual(FirstCategory.ItemsWithThisCategory.Count, 2);
        //    Assert.AreEqual(FirstCategory.CountOfItemsWithThisCategory, 2);

        //    Assert.AreEqual(SecondCategory.ItemsWithThisCategory.Count, 1);
        //    Assert.AreEqual(SecondCategory.CountOfItemsWithThisCategory, 1);

        //    Assert.AreEqual(new Category(CurrentBook).ItemsWithThisCategory.Count, 0);
        //    Assert.AreEqual(new Category(CurrentBook).CountOfItemsWithThisCategory, 0);
        //}


        //[TestMethod]
        //public void ItemsWithCategory_WhenDeleteItem_Returns0()
        //{
        //    StartFunction();

        //    SecondItem.DeleteItem();

        //    Assert.AreEqual(FirstCategory.ItemsWithThisCategory.Count, 1);
        //    Assert.AreEqual(FirstCategory.CountOfItemsWithThisCategory, 1);

        //    Assert.AreEqual(SecondCategory.ItemsWithThisCategory.Count, 0);
        //    Assert.AreEqual(SecondCategory.CountOfItemsWithThisCategory, 0);
        //}

        //[TestMethod]
        //public void ItemsWithCategory_WhenAddNewConnections_ReturnsTrue()
        //{
        //    StartFunction();

        //    CategoryInItem.CreateCategoryInItem(CurrentBook, SecondCategory, FirstItem);
        //    Assert.AreEqual(SecondCategory.ItemsWithThisCategory[1], FirstItem);
        //    Assert.AreEqual(SecondCategory.ItemsWithThisCategory.Count, 1);
        //}



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
