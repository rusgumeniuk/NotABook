using System;
using System.Collections.Generic;
using System.Text;
using NotABookLibraryStandart.Exceptions;
using System.Collections.ObjectModel;

namespace NotABookLibraryStandart.Models
{
    /// <summary>
    /// Represents a note of the book
    /// </summary>
    public class Item : ElementOfTheBook
    {
        #region Fields

        private Description description;

        #endregion

        #region Prop        

        public Description Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        /// <summary>
        /// Represents a text of the description
        /// </summary>
        public string DescriptionText
        {
            get => description.Text;
            set
            {
                description.Text = value;
                OnPropertyChanged("Description.Text");
            }
        }

        public ObservableCollection<Category> Categories
        {
            get
            {
                if (IsXamarinProjectDeploying)
                {
                    if (CurrentBook == null)
                        return null;
                    if (!CategoryInItem.IsItemHasConnection(this))
                        return null;
                }
                else
                {
                    if (CurrentBook == null)
                        throw new BookNullException();
                    if (!CategoryInItem.IsItemHasConnection(this))
                        throw new ElementIsNotInCollectionException();
                }

                ObservableCollection<Category> categories = new ObservableCollection<Category>();
                foreach (CategoryInItem pair in CurrentBook.CategoryInItemsOfBook)
                {
                    if (pair.GetItemId == Id)
                    {
                        categories.Add(pair.Category);
                    }
                }
                return categories;
            }
            set
            {
                if (IsXamarinProjectDeploying)
                {
                    if (CurrentBook == null || value == null)
                        return;
                }
                else
                {
                    if (CurrentBook == null)
                        throw new BookNullException();
                    if (value == null)
                        throw new ArgumentNullException();
                }

                CategoryInItem.DeleteAllConnectionWithItem(this);
                foreach (var category in value ?? throw new ArgumentNullException())
                {
                    CategoryInItem.CreateCategoryInItem(category, this);
                }

                OnPropertyChanged("Categories");
            }
        }

        /// <summary>
        /// Represents a collection of Categories in string
        /// </summary>
        public string CategoriesStr { get => this.GetCategoriesInString(); }
        #endregion

        #region Constr

        public Item(Book curBook) : base(curBook)
        {
            CurrentBook.ItemsOfBook.Add(this);
        }

        public Item(Book curBook, string title) : this(curBook)
        {
            Title = title;
        }

        public Item(Book curBook, string title, Description description) : this(curBook, title)
        {
            Description = description;
        }

        public Item(Book curBook, string title, Description description, ObservableCollection<Category> categories) : this(curBook, title, description)
        {
            Categories = categories;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Change book of the current item to another
        /// </summary>
        /// <param name="newBook">The book that will contain the current item </param>
        /// <returns>Result of changing</returns>
        public string ChangeBookStr(Book newBook)
        {
            if (CurrentBook == null)
                return "Current book is null";
            if (newBook == null)
                return "new book is null";
            return $"Changing {CurrentBook.Title} on {newBook.Title} is {ChangeBook(newBook)}";
        }

        /// <summary>
        /// Change book of the "item" to "newBook"
        /// </summary>
        /// <param name="newBook">The book that will contain the current item</param>
        /// <param name="item">The item that will be moved from current book to "newBook"</param>
        /// <returns>String result</returns>
        public static string ChangeBookStr(Book newBook, Item item)
        {
            if (item == null)
                return "Item is null";
            if (item.CurrentBook == null)
                return "Current book is null";
            if (newBook == null)
                return "new book is null";
            return $"Changing {item.CurrentBook.Title} on {newBook.Title} is {ChangeBook(newBook, item)}";
        }

        /// <summary>
        /// Change book of the current item to another
        /// </summary>
        /// <param name="newBook">The book that will contain the current item </param>
        /// <returns>Is moved is success</returns>
        public bool ChangeBook(Book newBook)
        {
            Book lastBook = null;
            if (BaseClass.IsXamarinProjectDeploying)
            {
                if (CurrentBook == null || newBook == null)
                    return false;
                lastBook = this.CurrentBook;
                lastBook.DeleteItem(this);
                CurrentBook = newBook;
            }
            else
            {
                lastBook = this.CurrentBook ?? throw new BookNullException();
                lastBook.DeleteItem(this);
                CurrentBook = newBook ?? throw new BookNullException();
            }

            CurrentBook.ItemsOfBook.Add(this);
            return !lastBook.ItemsOfBook.Contains(this) && newBook.ItemsOfBook.Contains(this);
        }

        /// <summary>
        /// Change book of the "item" to "newBook"
        /// </summary>
        /// <param name="newBook">The book that will contain the current item</param>
        /// <param name="item">The item that will be moved from current book to "newBook"</param>
        /// <returns></returns>
        public static bool ChangeBook(Book newBook, Item item)
        {
            Book lastBook = null;
            if (BaseClass.IsXamarinProjectDeploying)
            {
                if (item == null || item.CurrentBook == null || newBook == null)
                    return false;
                lastBook = item.CurrentBook;
                lastBook.DeleteItem(item);
                item.CurrentBook = newBook;
            }
            else
            {
                if (item == null)
                    throw new ItemNullException();
                lastBook = item.CurrentBook ?? throw new BookNullException();
                lastBook.DeleteItem(item);
                item.CurrentBook = newBook ?? throw new BookNullException();
            }

            item.CurrentBook.ItemsOfBook.Add(item);
            return !lastBook.ItemsOfBook.Contains(item) && newBook.ItemsOfBook.Contains(item);
        }

        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this item.
        /// </summary>
        /// <param name="partOfItem">The string to seek</param>
        /// <returns>true if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false. </returns>        
        public bool IsItemContainsWord(string partOfItem)
        {
            if (BaseClass.IsXamarinProjectDeploying)
            {
                if (partOfItem == null)
                    throw new ArgumentNullException();
                if (String.IsNullOrWhiteSpace(partOfItem))
                    throw new ArgumentException();
            }
            else
            {
                if (partOfItem == null ||
                    String.IsNullOrWhiteSpace(partOfItem))
                    return false;
            }

            if (Title.ToUpperInvariant().Contains(partOfItem.ToUpperInvariant()) || this.Description.Text.ToUpperInvariant().Contains(partOfItem.ToUpperInvariant()))
                return true;
            foreach (Category category in this.Categories)
            {
                if (category.IsCategoryContainsWord(partOfItem))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within "item".
        /// </summary>
        /// <param name="item">The item in that looking for</param>
        /// <param name="partOfItem">The string to seek</param>
        /// <returns>true if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false. </returns>        
        public static bool IsItemContainsWord(Item item, string partOfItem)
        {
            if (BaseClass.IsXamarinProjectDeploying)
            {
                if (item == null)
                    throw new ItemNullException();
                if (partOfItem == null)
                    throw new ArgumentNullException();
                if (String.IsNullOrWhiteSpace(partOfItem))
                    throw new ArgumentException();
            }
            else
            {
                if (item == null ||
                    partOfItem == null ||
                    String.IsNullOrWhiteSpace(partOfItem))
                    return false;
            }

            if (item.Title.Contains(partOfItem) || item.Description.Text.Contains(partOfItem))
                    return true;

            foreach(Category category in item.Categories)
            {
                if (category.IsCategoryContainsWord(partOfItem))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Represents a collection of categories of this item in string
        /// </summary>
        /// <returns>String represent of the Categories</returns>
        public string GetCategoriesInString()
        {
            if (Categories == null || Categories.Count < 1) return "No one categories";
            
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Category categories in Categories)
            {
                stringBuilder.Append(categories.Title).Append(", ");
            }
            return stringBuilder.Remove(stringBuilder.Length - 2, 2).ToString();
        }

        /// <summary>
        /// Delete this item
        /// </summary>
        /// <returns>String represent of the deleting</returns>
        public string DeleteItemStr()
        {
            if (CurrentBook == null)
                return $"Current book of {Title} is null";
            return $"Deleting of {Title} is {DeleteItem().ToString()}";
        }

        /// <summary>
        /// Delete "item"
        /// </summary>
        /// <param name="item">The item to delete</param>
        /// <returns></returns>
        public static string DeleteItemStr(Item item)
        {
            if (item == null)
                return "Item is null";
            if (item.CurrentBook == null)
                return $"Current book of {item.Title} is null";
            return $"Deleting of {item.Title} is {DeleteItem(item).ToString()}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DeleteItem()
        {
            if (BaseClass.IsXamarinProjectDeploying)
            {
                if (CurrentBook == null)
                    return false;
            }
            else
            {
                if (CurrentBook == null)
                    throw new BookNullException();
            }

            CurrentBook.ItemsOfBook.Remove(this);
            CategoryInItem.DeleteAllConnectionWithItem(this);

            if (IsXamarinProjectDeploying)
                CurrentBook.OnPropertyChanged("DateOfLastChanging");

            return !CurrentBook.ItemsOfBook.Contains(this) && !CategoryInItem.IsItemHasConnection(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool DeleteItem(Item item)
        {
            if (BaseClass.IsXamarinProjectDeploying)
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

            item.CurrentBook.ItemsOfBook.Remove(item);
            CategoryInItem.DeleteAllConnectionWithItem(item);

            if (BaseClass.IsXamarinProjectDeploying)
                item.CurrentBook.OnPropertyChanged("DateOfLastChanging");

            return !item.CurrentBook.ItemsOfBook.Contains(item) && !CategoryInItem.IsItemHasConnection(item);
        }

        #endregion
    }

    /// <summary>
    /// Represents a description of the item
    /// </summary>
    public class Description : BaseClass
    {
        public string Text { get; set; }
        public List<Object> Files { get; set; }

        private Description(string text)
        {
            Text = text;
        }
        private Description(string text, List<Object> list) : this(text)
        {
            Files = list;
        }

        public static Description CreateDescription(string text)
        {
            return new Description(text);
        }
        public static Description CreateDescription(string text, List<Object> list)
        {
            return new Description(text, list);
        }       
    }
}
