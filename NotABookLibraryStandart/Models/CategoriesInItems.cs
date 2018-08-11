using System;
using System.Collections.Generic;
using System.Text;
using NotABookLibraryStandart.Exceptions;
using System.Collections.ObjectModel;

namespace NotABookLibraryStandart.Models
{
    public class CategoryInItem : ElementOfTheBook
    {
        #region Fields        
        private Guid categoryId;

        private Guid itemId;

        #endregion

        #region Properities

        public Category Category
        {
            get => CurrentBook.CategoriesOfBook[CurrentBook.GetIndexOfCategoryByID(categoryId)];
            set => categoryId = value.Id;
        }
        public Item Item
        {
            get => CurrentBook.ItemsOfBook[CurrentBook.GetIndexOfItemByID(itemId)];
            set => itemId = value.Id;
        }

        public Guid GetCategoryId { get => categoryId; }
        public Guid GetItemId { get => itemId; }

        #endregion

        #region Constr

        public CategoryInItem(Book curBook) : base(curBook)
        {
            CurrentBook.CategoryInItemsOfBook.Add(this);
        }
        public CategoryInItem(Category category, Item item) : base(category.CurrentBook)
        {
            if (IsTestingOff)
            {
                if (category.CurrentBook != item.CurrentBook)
                    return;
                if (IsContainsThisPair(category, item))
                    return;
            }
            else
            {                
                if (category.CurrentBook != item.CurrentBook)
                    throw new ElementsFromDifferentBooksException();
                if (IsContainsThisPair(category, item))
                    throw new ElementAlreadyExistException();
            }

            CurrentBook = category.CurrentBook;
            categoryId = category.Id;
            itemId = item.Id;
            CurrentBook.CategoryInItemsOfBook.Add(this);
        }

        #endregion

        #region Methods
        public static CategoryInItem CreateCategoryInItem(Category category, Item item)
        {
            if (BaseClass.IsTestingOff)
            {
                if (category.CurrentBook == null || item.CurrentBook == null)
                    return null;
                if (category.CurrentBook != item.CurrentBook)
                    return null;
                if (CategoryInItem.IsContainsThisPair(category, item))
                    return category.CurrentBook.CategoryInItemsOfBook[CategoryInItem.GetIndexOfPair(category.CurrentBook, CategoryInItem.GetGuidOfPair(category, item))];
            }
            else
            {
                if (category.CurrentBook == null || item.CurrentBook == null)
                    throw new BookNullException();
                if (category.CurrentBook != item.CurrentBook)
                    throw new ElementsFromDifferentBooksException();
                if (CategoryInItem.IsContainsThisPair(category, item))
                    throw new ElementAlreadyExistException();
            }

            return new CategoryInItem(category, item);
        }

        public static ObservableCollection<CategoryInItem> GetCIIListByCategory(Category category)
        {
            if (BaseClass.IsTestingOff)
            {
                if (category == null || category.CurrentBook == null)
                    return null;
            }
            else
            {
                if (category == null)
                    throw new CategoryNullException();
                if (category.CurrentBook == null)
                    throw new BookNullException();               
            }


            ObservableCollection<CategoryInItem> list = new ObservableCollection<CategoryInItem>();
            foreach (var pair in category.CurrentBook.CategoryInItemsOfBook)
            {
                if (pair.categoryId == category.Id) list.Add(pair);
            }
            return list;
        }
        public static ObservableCollection<CategoryInItem> GetCIIListByItem(Item item)
        {
            if (BaseClass.IsTestingOff)
            {
                if (item == null || item.CurrentBook == null)
                    return null;
            }
            else
            {
                if (item == null)
                    throw new ItemNullException();
                if (item.CurrentBook == null)
                    throw new BookNullException();
            }

            ObservableCollection<CategoryInItem> list = new ObservableCollection<CategoryInItem>();
            foreach (var pair in item.CurrentBook.CategoryInItemsOfBook)
            {
                if (pair.itemId == item.Id) list.Add(pair);
            }
            return list;
        }


        public static string IsItemHasConnectionStr(Item item)
        {
            if (item == null)
                return "Item is null";
            if (item.CurrentBook == null)
                return "Book is null";

            return $"{IsItemHasConnection(item).ToString()}";
        }
        public static string IsCategoryHasConnectionStr(Book book, Category category)
        {
            if (book == null)
                return "Book is null";
            if (category == null)
                return "Category is null";

            return $"{IsCategoryHasConnection(category).ToString()}";
        }

        public static bool IsItemHasConnection(Item item)
        {
            if (BaseClass.IsTestingOff)
            {
                if (item == null || item.CurrentBook == null)
                    return false;
            }
            else
            {
                if (item == null)
                    throw new ItemNullException();
                if (item.CurrentBook == null)
                    throw new BookNullException();
            }


            foreach (var pair in item.CurrentBook.CategoryInItemsOfBook)
            {
                if (pair.GetItemId == item.Id) return true;
            }
            return false;
        }
        public static bool IsCategoryHasConnection(Category category)
        {
            if (BaseClass.IsTestingOff)
            {
                if (category == null || category.CurrentBook == null)
                    return false;
            }
            else
            {
                if (category == null)
                    throw new CategoryNullException();
                if (category.CurrentBook == null)
                    throw new BookNullException();
            }

            foreach (var pair in category.CurrentBook.CategoryInItemsOfBook)
            {
                if (pair.GetCategoryId == category.Id)
                    return true;
            }
            return false;
        }


        public static bool IsContainsThisPair(Category category, Item item)
        {
            if (BaseClass.IsTestingOff)
            {
                if (category == null || item == null)
                    return false;
                if (category.CurrentBook != item.CurrentBook)
                    return false;
            }
            else
            {
                if (category == null)
                    throw new CategoryNullException();
                if (item == null)
                    throw new ItemNullException();
                if (category.CurrentBook != item.CurrentBook)
                    throw new ElementsFromDifferentBooksException();
            }

            return GetGuidOfPair(category, item) != Guid.Empty;
        }
        public static bool IsContainsThisPair(Book book, Guid categoryId, Guid itemId)
        {
            if (BaseClass.IsTestingOff)
            {
                if (book == null || categoryId == null || itemId == null || categoryId == Guid.Empty || itemId == Guid.Empty)
                    return false;
            }
            else
            {
                if (book == null)
                    throw new BookNullException();
                if (categoryId == Guid.Empty || itemId == Guid.Empty)
                    throw new EmptyGuidException();
            }
            return GetGuidOfPair(book, categoryId, itemId) != Guid.Empty;
        }


        public static Guid GetGuidOfPair(Category category, Item item)
        {
            if (BaseClass.IsTestingOff)
            {
                if (category == null || item == null)
                    return Guid.Empty;
                if (category.CurrentBook != item.CurrentBook)
                    return Guid.Empty;
            }
            else
            {
                if (category == null)
                    throw new CategoryNullException();
                if (item == null)
                    throw new ItemNullException();
                if (category.CurrentBook != item.CurrentBook)
                    throw new ElementsFromDifferentBooksException();
            }

            foreach (var pair in category.CurrentBook.CategoryInItemsOfBook)
            {
                if (pair.categoryId == category.Id && pair.itemId == item.Id) return pair.Id;
            }
            return Guid.Empty;
        }
        public static Guid GetGuidOfPair(Book book, Guid currentCategoryId, Guid currentItemId)
        {
            if (BaseClass.IsTestingOff)
            {
                if (book == null || currentCategoryId == null || currentItemId == null || currentCategoryId == Guid.Empty || currentItemId == Guid.Empty)
                    return Guid.Empty;
            }
            else
            {
                if (book == null)
                    throw new BookNullException();
                if (currentCategoryId == Guid.Empty || currentItemId == Guid.Empty)
                    throw new EmptyGuidException();
            }

            foreach (var pair in book.CategoryInItemsOfBook)
            {
                if (pair.categoryId == currentCategoryId && pair.itemId == currentItemId) return pair.Id;
            }
            return Guid.Empty;
        }


        public static int GetIndexOfPair(Book book, Guid guid)
        {
            if (BaseClass.IsTestingOff)
            {
                if (book == null)
                    return -2;
            }
            else
            {
                if (book == null)
                    throw new BookNullException();
            }


            for (int i = 0; i < book.CategoryInItemsOfBook.Count; ++i)
            {
                if (book.CategoryInItemsOfBook[i].Id == guid) return i;
            }
            return -1;
        }


        public static int DeleteConnection(Category category, Item item)
        {
            if (BaseClass.IsTestingOff)
            {
                if (category == null || item == null)
                    return -1;
                if (category.CurrentBook != item.CurrentBook)
                    return -2;
                if (!IsContainsThisPair(category, item))
                    return -3;
            }
            else
            {
                if (category == null)
                    throw new CategoryNullException();
                if (item == null)
                    throw new ItemNullException();
                if (category.CurrentBook != item.CurrentBook)
                    throw new ElementsFromDifferentBooksException();
                if (!IsContainsThisPair(category, item))
                    throw new ElementIsNotInCollectionException();
            }

            Guid guid = CategoryInItem.GetGuidOfPair(category, item);
            if (guid == Guid.Empty)
                return -4;
            category.CurrentBook.CategoryInItemsOfBook.RemoveAt(GetIndexOfPair(category.CurrentBook, guid));

            return !IsContainsThisPair(category, item) == true ? 1 : -1;
        }
        public static int DeleteConnection(Book book, Guid categoryId, Guid itemId)
        {
            if (BaseClass.IsTestingOff)
            {
                if (book == null)
                    return -2;
                if (categoryId == Guid.Empty || itemId == Guid.Empty)
                    return -1;
                if (!IsContainsThisPair(book, categoryId, itemId))
                    return -3;
            }
            else
            {
                if (book == null)
                    throw new BookNullException();
                if (categoryId == Guid.Empty || itemId == Guid.Empty)
                    throw new EmptyGuidException();
                    if (!IsContainsThisPair(book, categoryId, itemId))
                    throw new ElementIsNotInCollectionException();
            }


            Guid guid = CategoryInItem.GetGuidOfPair(book, categoryId, itemId);
            if (guid == Guid.Empty)
                return -4;
            book.CategoryInItemsOfBook.RemoveAt(GetIndexOfPair(book, guid));

            return !IsContainsThisPair(book, categoryId, itemId) == true ? 1 : -1;
        }


        public bool DeleteConnection()
        {
            if (BaseClass.IsTestingOff)
            {
                if (CurrentBook == null || !IsContainsThisPair(CurrentBook, categoryId, itemId))
                    return false;
            }
            else
            {
                if (CurrentBook == null)
                    throw new BookNullException();
                if (!IsContainsThisPair(CurrentBook, categoryId, itemId))
                    throw new ElementIsNotInCollectionException();
            }

            CurrentBook.CategoryInItemsOfBook.Remove(this);
            return !IsContainsThisPair(CurrentBook, this.categoryId, this.itemId);
        }


        public static bool DeleteAllConnectionWithItem(Item item)
        {
            if (BaseClass.IsTestingOff)
            {
                if (item == null || item.CurrentBook == null)
                    return false;
            }
            else
            {
                if (item == null)
                    throw new ItemNullException();
                if (item.CurrentBook == null)
                    throw new BookNullException();               
            }

            if (!CategoryInItem.IsItemHasConnection(item))
                return true;

            foreach (var pair in CategoryInItem.GetCIIListByItem(item))
            {
                if (!pair.DeleteConnection())
                {
                    if (!BaseClass.IsTestingOff)
                        throw new InvalidOperationException();
                }
            }

            return !IsItemHasConnection(item);
        }
        public static bool DeleteAllConnectionWithCategory(Category category)
        {
            if (BaseClass.IsTestingOff)
            {
                if (category == null || category.CurrentBook == null)
                    return false;
            }
            else
            {
                if (category == null)
                    throw new CategoryNullException();
                if (category.CurrentBook == null)
                    throw new BookNullException();
            }

            if (!CategoryInItem.IsCategoryHasConnection(category))
                return true;

            foreach (var pair in CategoryInItem.GetCIIListByCategory(category))
            {
                if (!pair.DeleteConnection())
                {
                    if (!BaseClass.IsTestingOff)
                        throw new InvalidOperationException();
                }
            }

            return !IsCategoryHasConnection(category);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CategoryInItem)) return false;
            CategoryInItem pair = (CategoryInItem)obj;
            return pair.categoryId == this.categoryId && pair.itemId == this.itemId;
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        #endregion
    }
}
