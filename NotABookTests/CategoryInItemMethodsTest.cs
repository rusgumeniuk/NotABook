using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using NotABookLibraryStandart.Models;
using NotABookLibraryStandart.Exceptions;

namespace NotABookTests
{
    [TestClass]
    public class CategoryInItemMethodsTest
    {
        #region StartedInfo
        Book CurrentBook = null;
        Item FirstItem = null;
        Item SecondItem = null;
        Category FirstCategory = null;
        Category SecondCategory = null;
        #endregion

        #region IsItemHasConnection
        [TestMethod]
        public void IsItemHasConnection_WhenHas_ReturnsTrue()
        {
            SetUp();

            Assert.IsTrue(CategoryInItem.IsItemHasConnection(FirstItem));
            Assert.IsTrue(CategoryInItem.IsItemHasConnection(SecondItem));
        }

        [TestMethod]
        public void IsItemHasConnection_WhenHasNot_ReturnsFalse()
        {
            SetUp();

            CategoryInItem.DeleteConnection(FirstCategory, FirstItem);

            Assert.IsFalse(CategoryInItem.IsItemHasConnection(new Item(CurrentBook)));
            Assert.IsFalse(CategoryInItem.IsItemHasConnection(FirstItem));
        }

        [TestMethod]
        [ExpectedException(typeof(ItemNullException))]
        public void IsItemHasConnection_WhenItemIsNull_ReturnsItemNullException()
        {
            SetUp();

            CategoryInItem.IsItemHasConnection(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ItemNullException))]
        public void IsItemHasConnection_WhenBookIsNull_ReturnsBookNullException()
        {
            CategoryInItem.IsItemHasConnection(null);
        }
        #endregion

        #region GetGuidOfPair
        [TestMethod]
        public void GetGuidOfPairTest_WhenNotNull_ReturnsRealGuid()
        {
            SetUp();

            Assert.AreNotEqual(CategoryInItem.GetGuidOfPair(FirstCategory, FirstItem), Guid.Empty);
            Assert.AreNotEqual(CategoryInItem.GetGuidOfPair(CurrentBook, SecondCategory.Id, SecondItem.Id), Guid.Empty);
        }

        [TestMethod]
        public void GetGuidOfPairTest_WhenNull_ReturnsEmptyGuid()
        {
            SetUp();

            Assert.AreEqual(CategoryInItem.GetGuidOfPair(FirstCategory, new Item(CurrentBook)), Guid.Empty);
            Assert.AreEqual(CategoryInItem.GetGuidOfPair(CurrentBook, SecondCategory.Id, FirstItem.Id), Guid.Empty);
        }
        #endregion

        #region IsBookContainsPair
        [TestMethod]
        public void IsBookContains_WhenNotNull_ReturnsTrue()
        {
            SetUp();

            Assert.IsTrue(CategoryInItem.IsContainsThisPair(SecondCategory, SecondItem));
            Assert.IsTrue(CategoryInItem.IsContainsThisPair(CurrentBook, FirstCategory.Id, FirstItem.Id));
        }

        [TestMethod]
        public void IsBookContains_WhenNull_ReturnsFalse()
        {
            SetUp();

            Assert.IsFalse(CategoryInItem.IsContainsThisPair(SecondCategory, FirstItem));
            Assert.IsFalse(CategoryInItem.IsContainsThisPair(CurrentBook, new Category(CurrentBook).Id, FirstItem.Id));
        }
        #endregion

        #region GetIndexOfPair
        [TestMethod]
        public void GetIndexOfPairTest_WhenRealObj_ReturnsTrue()
        {
            SetUp();

            Assert.AreEqual(CategoryInItem.GetIndexOfPair(CurrentBook, CategoryInItem.GetGuidOfPair(FirstCategory, FirstItem)), 0);
            Assert.AreEqual(CategoryInItem.GetIndexOfPair(CurrentBook, CategoryInItem.CreateCategoryInItem(SecondCategory, FirstItem).Id), 3);
            Assert.AreEqual(CategoryInItem.GetIndexOfPair(CurrentBook, CategoryInItem.GetGuidOfPair(SecondCategory, SecondItem)), 1);
        }

        [TestMethod]
        public void GetIndexOfPairTest_WhenNullObj_ReturnsFalse()
        {
            SetUp();

            Assert.AreEqual(CategoryInItem.GetIndexOfPair(CurrentBook, CategoryInItem.GetGuidOfPair(new Category(CurrentBook), SecondItem)), -1);
            Assert.AreEqual(CategoryInItem.GetIndexOfPair(CurrentBook, CategoryInItem.GetGuidOfPair(SecondCategory, FirstItem)), -1);
        }
        #endregion

        #region CreateCategoryInItem
        [TestMethod]
        public void CreateCategoryInItemTest_WhenGoodArguments_ReturnsTrue()
        {
            SetUp();

            Item third = new Item(CurrentBook);
            CategoryInItem newPair = CategoryInItem.CreateCategoryInItem(SecondCategory, third);

            Assert.AreEqual(newPair.GetCategoryId, SecondCategory.Id);
            Assert.AreEqual(newPair.GetItemId, third.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ElementAlreadyExistException))]
        public void CreateCategoryInItemTest_WhenIPairAlreadyInList_ReturnsElementAlreadyExistException()
        {
            SetUp();
            CategoryInItem.CreateCategoryInItem(FirstCategory, FirstItem);
        }

        #endregion

        #region GetPairListByCategory
        [TestMethod]
        public void GetPairListByCategory_WhenRealArg_ReturnsTrue()
        {
            SetUp();

            Assert.AreEqual(CategoryInItem.GetCIIListByCategory(FirstCategory).Count, 2);
            Assert.AreEqual(CategoryInItem.GetCIIListByCategory(SecondCategory).Count, 1);
            Assert.AreEqual(CategoryInItem.GetCIIListByCategory(new Category(CurrentBook)).Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(CategoryNullException))]
        public void GetPairListByCategory_WhenCategoryIsNull_ReturnsCategoryNullException()
        {
            SetUp();

            CategoryInItem.GetCIIListByCategory(null);
        }        
        #endregion

        #region DeleteConnection
        [TestMethod]
        public void DeleteConnection_WhenDeletePair_ReturnsTrue()
        {
            SetUp();

            Item newItem = new Item(CurrentBook);
            Category newCategory = new Category(CurrentBook);
            CategoryInItem pair = CategoryInItem.CreateCategoryInItem(newCategory, newItem);
            Assert.IsTrue(pair.DeleteConnection());
        }

        [TestMethod]
        [ExpectedException(typeof(ElementIsNotInCollectionException))]
        public void DeleteConnection_WhenAfterDeletingTryToDeleteAgain_ReturnsElementIsNotInCollectionException()
        {
            SetUp();

            Item newItem = new Item(CurrentBook);
            Category newCategory = new Category(CurrentBook);
            CategoryInItem pair = CategoryInItem.CreateCategoryInItem(newCategory, newItem);
            pair.DeleteConnection();
            pair.DeleteConnection();
        }




        [TestMethod]
        public void DeleteConnectionWithArgs_WhenSetRealArg_Returns1()
        {
            SetUp();
            Assert.AreEqual(CategoryInItem.DeleteConnection(SecondCategory, SecondItem), 1);
            Assert.AreEqual(CategoryInItem.DeleteConnection(FirstCategory, FirstItem), 1);
            Assert.AreEqual(CategoryInItem.DeleteConnection(FirstCategory, SecondItem), 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ElementIsNotInCollectionException))]
        public void DeleteConnectionWithArgs_WhenPairAlreadyDeleted_ReturnsElementIsNotInCollectionException()
        {
            SetUp();
            CategoryInItem.DeleteConnection(SecondCategory, SecondItem);
            CategoryInItem.DeleteConnection(SecondCategory, SecondItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ElementIsNotInCollectionException))]
        public void DeleteConnectionWithArgs_WhenPairHasWrongArguments_ReturnsElementIsNotInCollectionException()
        {
            SetUp();
            CategoryInItem.DeleteConnection(SecondCategory, new Item(CurrentBook));
        }       
        #endregion

        #region DeleteAllConnections
        [TestMethod]
        public void DeleteAllConnectionWithItem_WhenArgGood_ReturnsTrue()
        {
            SetUp();

            Assert.AreEqual(CategoryInItem.DeleteAllConnectionWithItem(SecondItem), true);
            Assert.IsFalse(CategoryInItem.IsCategoryHasConnection(SecondCategory));
            Assert.AreEqual(CategoryInItem.DeleteAllConnectionWithItem(FirstItem), true);
            Assert.AreEqual(CurrentBook.CategoryInItemsOfBook.Count, 0);
        }

        [TestMethod]
        public void DeleteAllConnectionWithItem_WhenConnectionAlreadyDeleted_ReturnsTrue()
        {
            SetUp();
            Assert.IsTrue(CategoryInItem.DeleteAllConnectionWithItem(new Item(CurrentBook)));
        }

        [TestMethod]
        [ExpectedException(typeof(ItemNullException))]
        public void DeleteAllConnectionWithItem_WhenItemIsNull_ReturnsItemNullException()
        {
            SetUp();
            CategoryInItem.DeleteAllConnectionWithItem(null);
        }

        [TestMethod]
        [ExpectedException(typeof(BookNullException))]
        public void DeleteAllConnectionWithItem_WhenBookIsNull_ReturnsBookNullException()
        {
            SetUp();
            SecondItem.ChangeBook(null);
            CategoryInItem.DeleteAllConnectionWithItem(SecondItem);
        }
        #endregion

        #region Getting Item\Category        
        [TestMethod]
        public void CategoryInItem_WhenGettingFromProp_ReturnTrue()
        {
            SetUp();
            CategoryInItem.DeleteAllConnectionWithItem(SecondItem);

            CategoryInItem pair = CategoryInItem.CreateCategoryInItem(FirstCategory, SecondItem);

            Assert.AreEqual(FirstCategory.Title, pair.Category.Title);
            Assert.AreEqual(SecondItem.Description, pair.Item.Description);
            Assert.AreEqual(SecondItem, pair.Item);

        }
        #endregion

        private void SetUp()
        {
            CurrentBook = new Book("CurBook");

            FirstCategory = new Category(CurrentBook, "Sweet");
            SecondCategory = new Category(CurrentBook, "Salt");

            FirstItem = new Item(CurrentBook, "1 item", Description.CreateDescription("desc1"), new ObservableCollection<Category>() { FirstCategory });
            SecondItem = new Item(CurrentBook, "2 item", Description.CreateDescription("descript 2"), new ObservableCollection<Category>() { SecondCategory, FirstCategory });
        }
    }
}
