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
    public class Item : BookElement
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
                if (Book.IsBookIsNotNull(CurrentBook) && CategoryInItem.IsItemHasConnection(this)) 
                {
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
                return null;
            }
            set
            {
                if (IsItemAndItsBookNotNull(this))
                {
                    if (value != null)
                    {
                        CategoryInItem.DeleteAllConnectionWithItem(this);

                        foreach (var category in value)
                        {
                            CategoryInItem.CreateCategoryInItem(category, this);
                        }

                        OnPropertyChanged("Categories");
                    }
                    else if (!IsXamarinProjectDeploying)
                        throw new ArgumentNullException();
                }              
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
        /// Returns value indicating is item is not null
        /// </summary>
        /// <param name="item">The item that is checked </param>
        /// <exception cref="ItemNullException">When item is null and Xamarin mode is off</exception>
        /// <returns>True - not null, else if Xamain mode in on - false, else throw Exc</returns>
        public static bool IsItemIsNotNull(Item item)
        {
            return item != null ? true : (IsXamarinProjectDeploying ? false : throw new ItemNullException());
        }

        /// <summary>
        /// Indicates whether the item and its book is not null
        /// </summary>
        /// <param name="item">item to test</param>
        /// <exception cref="ItemNullException">When item is null</exception>
        /// <exception cref="BookNullException">When book of item is null</exception>
        /// <returns></returns>
        public static bool IsItemAndItsBookNotNull(Item item)
        {
            return IsItemIsNotNull(item) && Book.IsBookIsNotNull(item.CurrentBook);
        }

        /// <summary>
        /// Change book of the current item to another
        /// </summary>
        /// <param name="newBook">The book that will contain the current item </param>
        /// <returns>Result of changing</returns>
        public string ChangeBookStr(Book newBook)
        {
            if (Book.IsBookIsNotNull(CurrentBook))
                return "Current book is null";
            if (Book.IsBookIsNotNull(newBook))
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
            if (Item.IsItemIsNotNull(item))
                return "Item is null";
            if (Book.IsBookIsNotNull(item.CurrentBook))
                return "Current book is null";
            if (Book.IsBookIsNotNull(newBook)) 
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
            if(Book.IsBookIsNotNull(CurrentBook) && Book.IsBookIsNotNull(newBook))
            {
                Book lastBook = this.CurrentBook;
                lastBook.DeleteItem(this);
                CurrentBook = newBook;
                CurrentBook.ItemsOfBook.Add(this);
                return !lastBook.ItemsOfBook.Contains(this) && newBook.ItemsOfBook.Contains(this);
            }
            return false;
        }

        /// <summary>
        /// Change book of the "item" to "newBook"
        /// </summary>
        /// <param name="newBook">The book that will contain the current item</param>
        /// <param name="item">The item that will be moved from current book to "newBook"</param>
        /// <returns></returns>
        public static bool ChangeBook(Book newBook, Item item)
        {            
            if (Item.IsItemAndItsBookNotNull(item) && Book.IsBookIsNotNull(newBook))
            {
                Book lastBook = item.CurrentBook;
                lastBook.DeleteItem(item);
                item.CurrentBook = newBook;

                item.CurrentBook.ItemsOfBook.Add(item);
                return !lastBook.ItemsOfBook.Contains(item) && newBook.ItemsOfBook.Contains(item);
            }
            return false;
        }

        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this item.
        /// </summary>
        /// <param name="partOfItem">The string to seek</param>
        /// <returns>true if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false. </returns>        
        public bool IsItemContainsWord(string partOfItem)
        {
            if (ExtensionClass.IsStringNotNull(partOfItem))
            {
                if (Title.ToUpperInvariant().Contains(partOfItem.ToUpperInvariant()) 
                    || this.Description.Text.ToUpperInvariant().Contains(partOfItem.ToUpperInvariant()))
                    return true;
                foreach (Category category in this.Categories)
                {
                    if (category.IsCategoryContainsWord(partOfItem))
                        return true;
                }
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
            if (Item.IsItemIsNotNull(item) && ExtensionClass.IsStringNotNull(partOfItem))
            {
                if (item.Title.ToUpperInvariant().Contains(partOfItem.ToUpperInvariant()) || 
                    item.Description.Text.ToUpperInvariant().Contains(partOfItem.ToUpperInvariant()))
                    return true;

                foreach (Category category in item.Categories)
                {
                    if (category.IsCategoryContainsWord(partOfItem))
                        return true;
                }
            }          

            return false;
        }

        /// <summary>
        /// Represents a collection of categories of this item in string
        /// </summary>
        /// <returns>String represent of the Categories</returns>
        public string GetCategoriesInString()
        {
            try
            {
                if (Categories == null || Categories.Count < 1) return "No one categories. ";
            }
            catch(ElementIsNotInCollectionException)
            {
                return "No one categories.";
            }
            
            
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Category categories in Categories)
            {
                stringBuilder.Append(categories.Title).Append(", ");
            }
            return stringBuilder.Remove(stringBuilder.Length - 2, 2).Append(". ").ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Delete()
        {
            if(Book.IsBookIsNotNull(CurrentBook))
            {
                CategoryInItem.DeleteAllConnectionWithItem(this);
                CurrentBook.ItemsOfBook.Remove(this);

                OnPropertyChanged("DateOfLastChanging");
            }

            return !CurrentBook.ItemsOfBook.Contains(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool DeleteItem(Item item)
        {
            if (IsItemAndItsBookNotNull(item))
            {
                item.CurrentBook.ItemsOfBook.Remove(item);
                CategoryInItem.DeleteAllConnectionWithItem(item);

                item.OnPropertyChanged("DateOfLastChanging");
            }
            return !item.CurrentBook.ItemsOfBook.Contains(item);
        }

        public override void ThrowNullException()
        {
            throw new ItemNullException();
        }
        #endregion
    }

    /// <summary>
    /// Represents a description of the item
    /// </summary>
    public class Description : BaseClass
    {
        public string Text;
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

        public bool IsEmptyDescription()
        {
            return String.IsNullOrWhiteSpace(Text) && Files.Count == 0;
        }

        public override bool Delete()
        {
            Text = null;
            Files = null;
            return true;
        }

        public override void ThrowNullException()
        {
            throw new ArgumentNullException("Null description");
        }
    }
}
