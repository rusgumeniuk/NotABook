using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using NotABookLibraryStandart.Models;
using NotABookLibraryStandart.Exceptions;

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
         [TestMethod]
         //[ExpectedException(typeof(ElementIsNotInCollectionException))]
         public void GetStringCountOfItemsWithCategory_WhenHasNotConnections_ReturnsElementIsNotInThisCollection()
        {
            SetUp();

            Category category = new Category(CurrentBook);
            //Assert.IsFalse(CategoryInItem.IsCategoryHasConnection(CurrentBook, category));
            string text = category.GetStringCountOfItemsWithCategory;
            
        }



        [TestMethod]
        public void ItemsWithCategory_WhenNotEmptyList_ReturnsTrueCount()
        {
            SetUp();

            Assert.AreEqual(FirstCategory.ItemsWithThisCategory.Count, 2);
            Assert.AreEqual(FirstCategory.CountOfItemsWithThisCategory, 2);

            Assert.AreEqual(SecondCategory.ItemsWithThisCategory.Count, 1);
            Assert.AreEqual(SecondCategory.CountOfItemsWithThisCategory, 1);
        }

        [TestMethod]
        public void ItemsWithCategory_WhenAddNewConnections_ReturnsTrue()
        {
            SetUp();

            CategoryInItem.CreateCategoryInItem(SecondCategory, FirstItem);
            Assert.AreEqual(SecondCategory.ItemsWithThisCategory[1], FirstItem);
            Assert.AreEqual(SecondCategory.ItemsWithThisCategory.Count, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ElementIsNotInCollectionException))]
        public void ItemsWithCategory_WhenDeleteItem_ReturnsElementIsNotInCollectionException()
        {
            SetUp();

            SecondItem.DeleteItem();

            Assert.AreEqual(SecondCategory.ItemsWithThisCategory.Count, 0);
            Assert.AreEqual(SecondCategory.CountOfItemsWithThisCategory, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyCollectionException))]
        public void ItemsWithCategory_WhenCategoryInItemListIsEmpty_ReturnsEmptyCollectionException()
        {
            SetUp();

            CategoryInItem.DeleteAllConnectionWithItem(SecondItem);
            CategoryInItem.DeleteAllConnectionWithCategory(FirstCategory);

            int x = FirstCategory.ItemsWithThisCategory.Count;
        }


        [TestMethod]
        public void RemoveCategoryFromAllItems_WhenRealCategory_ReturnsTrue()
        {
            SetUp();
            Assert.IsTrue(FirstCategory.DeleteCategory());
            Assert.IsTrue(Category.DeleteCategory(SecondCategory));
            Assert.AreEqual(CurrentBook.CategoriesOfBook.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(CategoryNullException))]
        public void RemoveCategoryFromAllItems_WhenCategoryIsNull_ReturnsCategoryNullException()
        {
            Category.DeleteCategory(null);
        }


        [TestMethod]
        public void DeleteCategory_WhenRealCategory_ReturnsTrue()
        {
            SetUp();
            Assert.IsTrue(FirstCategory.DeleteCategory());
            Assert.IsTrue(Category.DeleteCategory(SecondCategory));
            Assert.AreEqual(CurrentBook.CategoriesOfBook.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(CategoryNullException))]
        public void DeleteCategory_WhenCategoryIsNull_ReturnsCategoryNullException()
        {
            Category.DeleteCategory(null);
        }


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
