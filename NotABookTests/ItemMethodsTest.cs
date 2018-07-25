using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotABook.Models;
using System.Collections.ObjectModel;

namespace NotABookTests
{
    [TestClass]
    public class ItemMethodsTest
    {
        private readonly Book CurrentBook = new Book("Boook");

        //[TestMethod]
        //public void ItemConstructor_WhenNotNull_ReturnsNewItem()
        //{
        //    string firstTitle = "Ti";
        //    string secondTitle = "To";
        //    string thirdTitle = firstTitle + secondTitle;

        //    string secondDesk = "desk";
        //    string thirdDesk = "script";

        //    ObservableCollection<Category> categories = new ObservableCollection<Category>() { new Category(CurrentBook, "first"), new Category(CurrentBook, "second") };

        //    Item firstItem = new Item(CurrentBook, firstTitle);
        //    Item secondItem = new Item(CurrentBook, secondTitle, secondDesk);
        //    Item thirdItem = new Item(CurrentBook, thirdTitle, thirdDesk, categories);

        //    Assert.AreEqual(firstItem.Title, firstTitle);
        //    Assert.IsNull(firstItem.Description);

        //    Assert.AreEqual(secondItem.Description, secondDesk);

        //    Assert.AreEqual(thirdItem.Title, firstTitle + secondTitle);
        //    //Assert.AreEqual(thirdItem.Categories.Count, categories.Count);
        //}
    }
}
