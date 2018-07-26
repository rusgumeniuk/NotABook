using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotABook.Models;
using System.Collections.ObjectModel;
namespace NotABookTests
{
    [TestClass]
    public class BookMethodsTest
    {
        #region StartedInfo
        Book CurrentBook = null;
        Item FirstItem = null;
        Item SecondItem = null;
        Category FirstCategory = null;
        Category SecondCategory = null;
        #endregion

        [TestMethod]
         public void Books_WhenAddAndDeleteBooks_ReturnsTrue()
        {
            StartFunction();

            Book secondBook = new Book("sec");
            Book thirdBook = new Book("third");

            Assert.AreEqual(Book.Books.Count, 3);
            Assert.AreEqual(Book.Books[1], secondBook);

            CurrentBook.DeleteBook();
            Assert.AreEqual(Book.Books.Count, 2);
        }

        #region ISBookContainsBook
        [TestMethod]
        public void IsBooksContainsThisBook_WhenExistsBooks_ReturnsTrue()
        {
            StartFunction();

            Assert.IsTrue(Book.IsBooksContainsThisBook(CurrentBook));
            Assert.IsTrue(Book.IsBooksContainsThisBook(CurrentBook.Id));
            Assert.IsTrue(Book.IsBooksContainsThisBook(new Book("Title new")));        
        }

        [TestMethod] 
        public void IsBooksContainsThisBook_WhenDeleteBook_ReturnsTrue()
        {
            StartFunction();

            CurrentBook.DeleteBook();
            Assert.IsFalse(Book.IsBooksContainsThisBook(CurrentBook.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsBooksContainsThisBook_WhenThisBookIsNull_ReturnsArgumentNullException()
        {
            Book.IsBooksContainsThisBook(CurrentBook);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsBooksContainsThisBook_WhenGuidISEmpty_ReturnsArgumentException()
        {
            Book.IsBooksContainsThisBook(Guid.Empty);
        }
        #endregion

        #region AddBookToCollection
        [TestMethod]
        public void AddBookToCollection_WhenClearCollectionAndAfterAddBook_ReturnsTrue()
        {
            StartFunction();
            Assert.AreEqual(Book.Books.Count, 1);

            Book.Books.Clear();

            Assert.AreEqual(Book.Books.Count, 0);
            Assert.IsTrue(Book.AddBookToCollection(CurrentBook));
            Assert.AreEqual(Book.Books.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddBookToCollection_WhenAlreadyInCollection_ReturnsArgumentException()
        {
            StartFunction();

            Book.AddBookToCollection(CurrentBook);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddBookToCollection_WhenBookIsNull_ReturnsArgumentNullException()
        {
            Book.AddBookToCollection(CurrentBook);
        }
        #endregion

        #region DeleteBook
        [TestMethod]
        public void DeleteBook_WhenNonStaticMethodAndNotNullBook_ReturnsTrue()
        {
            StartFunction();

            Assert.IsTrue(CurrentBook.DeleteBook());
            Assert.IsFalse(Book.IsBooksContainsThisBook(CurrentBook));
            Assert.AreEqual(CurrentBook.ItemsOfBook.Count, 0);
        }

        [TestMethod]
        public void DeleteBook_WhenStaticMethodAndNotNullBook_ReturnsTrue()
        {
            StartFunction();

            Assert.IsTrue(Book.DeleteBook(CurrentBook));
            Assert.IsFalse(Book.IsBooksContainsThisBook(CurrentBook.Id));
            Assert.AreEqual(CurrentBook.CategoryInItemsOfBook.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DeleteBook_WhenThisBookIsNull_ReturnsArgumentNullException()
        {
            CurrentBook.DeleteBook();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteBook_WhenBookIsNull_ReturnsArgumentNullException()
        {
            Book.DeleteBook(CurrentBook);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteBook_WhenBooksNotContainsTheseBooks_ReturnsArgumentException()
        {
            StartFunction();
            CurrentBook.DeleteBook();
            CurrentBook.DeleteBook();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteBook_WhenStaticMethodAndBooksNotContainsTheseBooks_ReturnsArgumentException()
        {
            StartFunction();
            Book.DeleteBook(CurrentBook);
            Book.DeleteBook(CurrentBook);
        }
        #endregion

        #region GetIndexOf_Item\Category_ById
        [TestMethod]
        public void GetIndexOfItemById_WhenRealItems_ReturnsTrue()
        {
            StartFunction();

            Assert.AreEqual(Book.GetIndexOfItemByID(CurrentBook, FirstItem.Id), 0);
            Assert.AreEqual(CurrentBook.GetIndexOfItemByID(SecondItem.Id), 1);

            Assert.IsTrue(CurrentBook.DeleteItem(FirstItem));
            Assert.AreEqual(Book.GetIndexOfItemByID(CurrentBook, FirstItem.Id), -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetIndeOfItemById_WhenBookIsNull_ReturnsArgumentNullException()
        {
            Book.GetIndexOfItemByID(CurrentBook, new Item(CurrentBook).Id);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetIndeOfItemById_WhenItemIsNull_ReturnsNullReferenceException()
        {
            StartFunction();
            Book.GetIndexOfItemByID(CurrentBook, (null as Item).Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIndeOfItemById_WhenItemIdIsEmpty_ReturnsArgumentException()
        {
            StartFunction();
            Book.GetIndexOfItemByID(CurrentBook, Guid.Empty);
        }


        [TestMethod]
        public void GetIndexOfCategoryById_WhenRealCategory_ReturnsTrue()
        {
            StartFunction();

            Assert.AreEqual(Book.GetIndexOfCategoryByID(CurrentBook, FirstCategory.Id), 0);
            Assert.AreEqual(CurrentBook.GetIndexOfCategoryByID(SecondCategory.Id), 1);

            Assert.IsTrue(CurrentBook.DeleteCategory(FirstCategory));
            Assert.AreEqual(Book.GetIndexOfCategoryByID(CurrentBook, FirstCategory.Id), -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetIndexOfCategoryById_WhenBookIsNull_ReturnsArgumentNullException()
        {
            Book.GetIndexOfCategoryByID(CurrentBook, new Category(CurrentBook).Id);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetIndexOfCategoryById_WhenItemIsNull_ReturnsNullReferenceException()
        {
            StartFunction();
            Book.GetIndexOfCategoryByID(CurrentBook, (null as Category).Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIndexOfCategoryById_WhenCategoryIdIsEmpty_ReturnsArgumentException()
        {
            StartFunction();
            Book.GetIndexOfCategoryByID(CurrentBook, Guid.Empty);
        }
        #endregion

        #region IsBookContainsItem\Category

        [TestMethod]
        public void IsBookContainsItem_WhenExistingItems_ReturnsTrue()
        {
            StartFunction();

            Assert.IsTrue(Book.IsBookContainsItem(CurrentBook, FirstItem));
            Assert.IsTrue(Book.IsBookContainsItem(CurrentBook, SecondItem.Id));
            Assert.IsTrue(Book.IsBookContainsItem(CurrentBook, new Item(CurrentBook, "new")));
        }

        [TestMethod]
        public void IsBookContainsCategory_WhenExistingCategories_ReturnsTrue()
        {
            StartFunction();

            Assert.IsTrue(Book.IsBookContainsCategory(CurrentBook, FirstCategory));
            Assert.IsTrue(Book.IsBookContainsCategory(CurrentBook, SecondCategory.Id));
            Assert.IsTrue(Book.IsBookContainsCategory(CurrentBook, new Category(CurrentBook, "new")));
        }


        [TestMethod]
        public void IsBookContainsItem_WhenItemFromOtherBook_ReturnsFalse()
        {
            StartFunction();
            Book newBook = new Book();

            Assert.IsFalse(Book.IsBookContainsItem(newBook, FirstItem));
            Assert.IsFalse(Book.IsBookContainsItem(newBook, SecondItem.Id));
            Assert.IsFalse(Book.IsBookContainsItem(newBook, new Item(CurrentBook)));

            Assert.IsTrue(Book.IsBookContainsItem(newBook, new Item(newBook)));
        }

        [TestMethod]
        public void IsBookContainsCategories_WhenCategoriesromOtherBook_ReturnsFalse()
        {
            StartFunction();
            Book newBook = new Book();

            Assert.IsFalse(Book.IsBookContainsCategory(newBook, FirstCategory));
            Assert.IsFalse(Book.IsBookContainsCategory(newBook, SecondCategory.Id));
            Assert.IsFalse(Book.IsBookContainsCategory(newBook, new Category(CurrentBook)));

            Assert.IsTrue(Book.IsBookContainsCategory(newBook, new Category(newBook)));
        }

        #endregion

        #region DeleteItem
        [TestMethod]
        public void DeleteItem_WhenRealItems_ReturnsTrue()
        {
            StartFunction();

            Assert.IsTrue(CurrentBook.DeleteItem(FirstItem));
            Assert.IsTrue(Book.DeleteItem(CurrentBook, SecondItem));
            Assert.IsTrue(Book.DeleteItem(CurrentBook, new Item(CurrentBook).Id));

            Assert.AreEqual(CurrentBook.ItemsOfBook.Count, 0);
            Item newItem = new Item(CurrentBook);

            Assert.IsTrue(CurrentBook.DeleteItem(newItem.Id));
            Assert.AreEqual(CurrentBook.ItemsOfBook.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteItem_WhenItemIsNull_ReturnsArgumentNullException()
        {
            StartFunction();
            FirstItem = null;
            CurrentBook.DeleteItem(FirstItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteItem_WhenGuidIsEmpty_ReturnsArgumentException()
        {
            StartFunction();
            CurrentBook.DeleteItem(Guid.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteItem_WhenBookHasNotThisItem_ReturnsArgumentException()
        {
            StartFunction();
            Item newItem = new Item(new Book());
            CurrentBook.DeleteItem(newItem);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteItem_WhenStaticMethodAndBookIsNull_ReturnsArgumentNullException()
        {                    
            Book.DeleteItem(CurrentBook, FirstItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteItem_WhenStaticMethodAndItemIsNull_ReturnsArgumentNullException()
        {
            StartFunction();
            FirstItem = null;
            Book.DeleteItem(CurrentBook, FirstItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteItem_WhenStaticMethodAndGuidIsEmpty_ReturnsArgumentException()
        {
            StartFunction();
            Book.DeleteItem(CurrentBook, Guid.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteItem_WhenStaticMethodAndBookHasNotThisItem_ReturnsArgumentException()
        {
            StartFunction();
            Item newItem = new Item(new Book());
            Book.DeleteItem(CurrentBook, newItem);
        }
        #endregion

        #region DeleteCategory
        [TestMethod]
        public void DeleteCategory_WhenRealCategories_ReturnsTrue()
        {
            StartFunction();

            Assert.IsTrue(CurrentBook.DeleteCategory(FirstCategory));
            Assert.IsTrue(Book.DeleteCategory(CurrentBook, SecondCategory));
            Assert.IsTrue(Book.DeleteCategory(CurrentBook, new Category(CurrentBook).Id));

            Assert.AreEqual(CurrentBook.CategoriesOfBook.Count, 0);
            Category newICateg = new Category(CurrentBook);

            Assert.IsTrue(CurrentBook.DeleteCategory(newICateg.Id));
            Assert.AreEqual(CurrentBook.CategoriesOfBook.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCategory_WhenItemIsNull_ReturnsArgumentNullException()
        {
            StartFunction();
            FirstCategory = null;
            CurrentBook.DeleteCategory(FirstCategory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteCategory_WhenGuidIsEmpty_ReturnsArgumentException()
        {
            StartFunction();
            CurrentBook.DeleteCategory(Guid.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteCategory_WhenBookHasNotThisCategory_ReturnsArgumentException()
        {
            StartFunction();
            Category newCategory = new Category(new Book());
            CurrentBook.DeleteCategory(newCategory);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCategory_WhenStaticMethodAndBookIsNull_ReturnsArgumentNullException()
        {
            Book.DeleteCategory(CurrentBook, FirstCategory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteCategory_WhenStaticMethodAndCategoryIsNull_ReturnsArgumentNullException()
        {
            StartFunction();
            FirstCategory = null;
            Book.DeleteCategory(CurrentBook, FirstCategory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteCategory_WhenStaticMethodAndGuidIsEmpty_ReturnsArgumentException()
        {
            StartFunction();
            Book.DeleteCategory(CurrentBook, Guid.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteCategory_WhenStaticMethodAndBookHasNotThisCategory_ReturnsArgumentException()
        {
            StartFunction();
            Category newCategory = new Category(new Book());
            Book.DeleteCategory(CurrentBook, newCategory);
        }
        #endregion

        private void StartFunction()
        {
            Book.Books.Clear();
            CurrentBook = new Book("CurBook");

            FirstCategory = new Category(CurrentBook, "Sweet");
            SecondCategory = new Category(CurrentBook, "Salt");

            FirstItem = new Item(CurrentBook, "1 item", "desc1", new ObservableCollection<Category>() { FirstCategory });
            SecondItem = new Item(CurrentBook, "2 item", "descript 2", new ObservableCollection<Category>() { SecondCategory, FirstCategory });
        }
    }
}
