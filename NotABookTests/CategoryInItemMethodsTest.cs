using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotABook.Models;
using System.Collections.ObjectModel;
namespace NotABookTests
{
    [TestClass]
    public class CategoryInItemMethodsTest
    {

        Book CurrentBook = null;
        Item FirstItem = null;
        Item SecondItem = null;
        Category FirstCategory = null;
        Category SecondCategory = null;


        [TestMethod]
        public void IsItemHasConnection_WhenHas_ReturnsTrue()
        {
            StartFunction();

            Assert.IsTrue(CategoryInItem.IsItemHasConnection(CurrentBook, FirstItem));
            Assert.IsTrue(CategoryInItem.IsItemHasConnection(CurrentBook, SecondItem));
        }

        [TestMethod]
        public void IsItemHasConnection_WhenHasNot_ReturnsFalse()
        {
            StartFunction();

            CategoryInItem.DeleteConnection(CurrentBook, FirstCategory, FirstItem);

            Assert.IsFalse(CategoryInItem.IsItemHasConnection(CurrentBook, new Item(CurrentBook)));
            Assert.IsFalse(CategoryInItem.IsItemHasConnection(CurrentBook, FirstItem));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsItemHasConnection_WhenArgIsNull_ReturnsArgumentException()
        {
            StartFunction();

            CategoryInItem.IsItemHasConnection(CurrentBook, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsItemHasConnection_WhenBookIsNull_ReturnsArgumentNullException()
        {
            CategoryInItem.IsItemHasConnection(CurrentBook, null);
        }




        [TestMethod]
        public void GetGuidOfPairTest_WhenNotNull_ReturnsRealGuid()
        {
            StartFunction();

            Assert.AreNotEqual(CategoryInItem.GetGuidOfPair(CurrentBook, FirstCategory, FirstItem), Guid.Empty);
            Assert.AreNotEqual(CategoryInItem.GetGuidOfPair(CurrentBook, SecondCategory.Id, SecondItem.Id), Guid.Empty);
        }

        [TestMethod]
        public void GetGuidOfPairTest_WhenNull_ReturnsEmptyGuid()
        {
            StartFunction();

            Assert.AreEqual(CategoryInItem.GetGuidOfPair(CurrentBook, FirstCategory, new Item(CurrentBook)), Guid.Empty);
            Assert.AreEqual(CategoryInItem.GetGuidOfPair(CurrentBook, SecondCategory.Id, FirstItem.Id), Guid.Empty);
        }



        [TestMethod]
        public void IsBookContains_WhenNotNull_ReturnsTrue()
        {
            StartFunction();

            Assert.IsTrue(CategoryInItem.IsContainsThisPair(CurrentBook, SecondCategory, SecondItem));
            Assert.IsTrue(CategoryInItem.IsContainsThisPair(CurrentBook, FirstCategory.Id, FirstItem.Id));
        }

        [TestMethod]
        public void IsBookContains_WhenNull_ReturnsFalse()
        {
            StartFunction();

            Assert.IsFalse(CategoryInItem.IsContainsThisPair(CurrentBook, SecondCategory, FirstItem));
            Assert.IsFalse(CategoryInItem.IsContainsThisPair(CurrentBook, new Category(CurrentBook).Id, FirstItem.Id));
        }



        [TestMethod]
        public void GetIndexOfPairTest_WhenRealObj_ReturnsTrue()
        {
            StartFunction();

            Assert.AreEqual(CategoryInItem.GetIndexOfPair(CurrentBook, CategoryInItem.GetGuidOfPair(CurrentBook, FirstCategory, FirstItem)), 0);
            Assert.AreEqual(CategoryInItem.GetIndexOfPair(CurrentBook, CategoryInItem.CreateCategoryInItem(CurrentBook, SecondCategory, FirstItem).Id), 3);
            Assert.AreEqual(CategoryInItem.GetIndexOfPair(CurrentBook, CategoryInItem.GetGuidOfPair(CurrentBook, SecondCategory, SecondItem)), 1);
        }

        [TestMethod]
        public void GetIndexOfPairTest_WhenNullObj_ReturnsFalse()
        {
            StartFunction();

            Assert.AreEqual(CategoryInItem.GetIndexOfPair(CurrentBook, CategoryInItem.GetGuidOfPair(CurrentBook, new Category(CurrentBook), SecondItem)), -1);
            Assert.AreEqual(CategoryInItem.GetIndexOfPair(CurrentBook, CategoryInItem.GetGuidOfPair(CurrentBook, SecondCategory, FirstItem)), -1);
        }




        [TestMethod]
        public void CreateCategoryInItemTest_WhenGoodArguments_ReturnsTrue()
        {
            StartFunction();

            Item third = new Item(CurrentBook);
            CategoryInItem newPair = CategoryInItem.CreateCategoryInItem(CurrentBook, SecondCategory, third);

            Assert.AreEqual(newPair.GetCategoryId, SecondCategory.Id);
            Assert.AreEqual(newPair.GetItemId, third.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateCategoryInItemTest_WhenIPairAlreadyInList_ReturnsArgumentException()
        {
            StartFunction();
            CategoryInItem.CreateCategoryInItem(CurrentBook, FirstCategory, FirstItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateCategoryInItemTest_WhenBookIsNull_ReturnsArgumentException()
        {
            StartFunction();
            CategoryInItem.CreateCategoryInItem(null, new Category(CurrentBook), FirstItem);
        }




        [TestMethod]
        public void GetPairListByCategory_WhenRealArg_ReturnsTrue()
        {
            StartFunction();

            Assert.AreEqual(CategoryInItem.GetCIIListByCategory(CurrentBook, FirstCategory).Count, 2);
            Assert.AreEqual(CategoryInItem.GetCIIListByCategory(CurrentBook, SecondCategory).Count, 1);
            Assert.AreEqual(CategoryInItem.GetCIIListByCategory(CurrentBook, new Category(CurrentBook)).Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetPairListByCategory_WhenCategoryIsNull_ReturnsArgumentNullException()
        {
            StartFunction();

            CategoryInItem.GetCIIListByCategory(CurrentBook, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetPairListByCategory_WhenBookIsNull_ReturnsArgumentNullException()
        {
            StartFunction();
            CategoryInItem.GetCIIListByCategory(null, SecondCategory);
        }



        [TestMethod]
        public void DeleteConnection_WhenDeletePair_ReturnsTrue()
        {
            StartFunction();

            Item newItem = new Item(CurrentBook);
            Category newCategory = new Category(CurrentBook);
            CategoryInItem pair = CategoryInItem.CreateCategoryInItem(CurrentBook, newCategory, newItem);
            Assert.IsTrue(pair.DeleteConnection());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteConnection_WhenAfterDeletingTryToDeleteAgain_ReturnsArgumentException()
        {
            StartFunction();

            Item newItem = new Item(CurrentBook);
            Category newCategory = new Category(CurrentBook);
            CategoryInItem pair = CategoryInItem.CreateCategoryInItem(CurrentBook, newCategory, newItem);
            pair.DeleteConnection();
            pair.DeleteConnection();
        }




        [TestMethod]
        public void DeleteConnectionWithArgs_WhenSetRealArg_Returns1()
        {
            StartFunction();
            Assert.AreEqual(CategoryInItem.DeleteConnection(CurrentBook, SecondCategory, SecondItem), 1);
            Assert.AreEqual(CategoryInItem.DeleteConnection(CurrentBook, FirstCategory, FirstItem), 1);
            Assert.AreEqual(CategoryInItem.DeleteConnection(CurrentBook, FirstCategory, SecondItem), 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteConnectionWithArgs_WhenPairAlreadyDeleted_ReturnsArgumentException()
        {
            StartFunction();
            CategoryInItem.DeleteConnection(CurrentBook, SecondCategory, SecondItem);
            CategoryInItem.DeleteConnection(CurrentBook, SecondCategory, SecondItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteConnectionWithArgs_WhenPairHasWrongArguments_ReturnsArgumentException()
        {
            StartFunction();
            CategoryInItem.DeleteConnection(CurrentBook, SecondCategory, new Item(CurrentBook));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteConnectionWithArgs_WhenBookIsNull_ReturnsArgumentNullException()
        {
            CategoryInItem.DeleteConnection(null, SecondCategory, SecondItem);
        }





        [TestMethod]
        public void DeleteAllConnectionWithItem_WhenArgGood_ReturnsTrue()
        {
            StartFunction();

            Assert.AreEqual(CategoryInItem.DeleteAllConnectionWithItem(CurrentBook, SecondItem), true);
            Assert.IsFalse(CategoryInItem.IsCategoryHasConnection(CurrentBook, SecondCategory));
            Assert.AreEqual(CategoryInItem.DeleteAllConnectionWithItem(CurrentBook, FirstItem), true);
            Assert.AreEqual(CurrentBook.CategoryInItemsOfBook.Count, 0);
        }

        [TestMethod]
        public void DeleteAllConnectionWithItem_WhenConnectionAlreadyDeleted_ReturnsTrue()
        {
            StartFunction();
            Assert.IsTrue(CategoryInItem.DeleteAllConnectionWithItem(CurrentBook, new Item(CurrentBook)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteAllConnectionWithItem_WhenItemIsWrong_ReturnsArgumentException()
        {
            StartFunction();
            CategoryInItem.DeleteAllConnectionWithItem(CurrentBook, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteAllConnectionWithItem_WhenBookIsNull_ReturnsArgumentNullException()
        {
            StartFunction();
            CategoryInItem.DeleteAllConnectionWithItem(null, SecondItem);
        }




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
