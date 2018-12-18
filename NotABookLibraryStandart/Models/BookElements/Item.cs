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
                UpdateDateOfLastChanging();
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
                UpdateDateOfLastChanging();
            }
        }
     
        public void SetCategories(Book CurrentBook, IEnumerable<Category> categories)
        {

            if (IsItemAndItsBookNotNull(CurrentBook, this))
            {
                if (categories != null)
                {
                    CategoryInItem.DeleteAllConnectionWithItem(CurrentBook, this);

                    foreach (var category in categories)
                    {
                        CategoryInItem.CreateCategoryInItem(CurrentBook, category, this);
                    }

                    UpdateDateOfLastChanging();
                }
                else if (ProjectType != TypeOfRunningProject.Xamarin)
                    throw new ArgumentNullException();
            }
        }

        public ObservableCollection<Category> GetCategories(Book CurrentBook)
        {
            if (Book.IsBookIsNotNull(CurrentBook) && CategoryInItem.IsItemHasConnection(CurrentBook, this))
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

        /// <summary>
        /// Represents a collection of Categories in string
        /// </summary>
        //public string CategoriesStr { get => this.GetCategoriesInString(); }
        #endregion

        #region Constr

        public Item(Book curBook) : base(curBook)
        {
            curBook.ItemsOfBook.Add(this);
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
            SetCategories(curBook, categories);
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
            return item != null ? true : (ProjectType == TypeOfRunningProject.Xamarin ? false : throw new ItemNullException());
        }

        /// <summary>
        /// Indicates whether the item and its book is not null
        /// </summary>
        /// <param name="item">item to test</param>
        /// <exception cref="ItemNullException">When item is null</exception>
        /// <exception cref="BookNullException">When book of item is null</exception>
        /// <returns></returns>
        public static bool IsItemAndItsBookNotNull(Book CurrentBook, Item item)
        {
            return IsItemIsNotNull(item) && Book.IsBookIsNotNull(CurrentBook);
        }

        /// <summary>
        /// Change book of the current item to another
        /// </summary>
        /// <param name="newBook">The book that will contain the current item </param>
        /// <returns>Result of changing</returns>
        public string ChangeBookStr(Book CurrentBook, Book newBook)
        {
            if (Book.IsBookIsNotNull(CurrentBook))
                return "Current book is null";
            if (Book.IsBookIsNotNull(newBook))
                return "new book is null";
            return $"Changing {CurrentBook.Title} on {newBook.Title} is {ChangeBook(CurrentBook, newBook)}";
        }

        /// <summary>
        /// Change book of the "item" to "newBook"
        /// </summary>
        /// <param name="newBook">The book that will contain the current item</param>
        /// <param name="item">The item that will be moved from current book to "newBook"</param>
        /// <returns>String result</returns>
        //public static string ChangeBookStr(Book newBook, Item item)
        //{
        //    if (Item.IsItemIsNotNull(item))
        //        return "Item is null";
        //    if (Book.IsBookIsNotNull(item.CurrentBook))
        //        return "Current book is null";
        //    if (Book.IsBookIsNotNull(newBook)) 
        //        return "new book is null";
        //    return $"Changing {item.CurrentBook.Title} on {newBook.Title} is {ChangeBook(newBook, item)}";
        //}

        /// <summary>
        /// Change book of the current item to another
        /// </summary>
        /// <param name="newBook">The book that will contain the current item </param>
        /// <returns>Is moved is success</returns>
        public bool ChangeBook(Book CurrentBook, Book newBook)
        {            
            if(Book.IsBookIsNotNull(CurrentBook) && Book.IsBookIsNotNull(newBook))
            {
                Book lastBook = CurrentBook;
                Book.DeleteItem(CurrentBook, this);
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
        public static bool ChangeBook(Book CurrentBook, Book newBook, Item item)
        {            
            if (Item.IsItemAndItsBookNotNull(CurrentBook, item) && Book.IsBookIsNotNull(newBook))
            {
                Book lastBook = CurrentBook;
                Book.DeleteItem(lastBook, item);                

                newBook.ItemsOfBook.Add(item);
                return !lastBook.ItemsOfBook.Contains(item) && newBook.ItemsOfBook.Contains(item);
            }
            return false;
        }

        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this item.
        /// </summary>
        /// <param name="partOfItem">The string to seek</param>
        /// <returns>true if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false. </returns>        
        public bool IsItemContainsWord(Book CurrentBook, string partOfItem)
        {
            if (ExtensionClass.IsStringNotNull(partOfItem))
            {
                if (Title.ToUpperInvariant().Contains(partOfItem.ToUpperInvariant()) 
                    || this.Description.Text.ToUpperInvariant().Contains(partOfItem.ToUpperInvariant()))
                    return true;
                foreach (Category category in this.GetCategories(CurrentBook))
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
        public static bool IsItemContainsWord(Book CurrentBook, Item item, string partOfItem)
        {
            if (Item.IsItemIsNotNull(item) && ExtensionClass.IsStringNotNull(partOfItem))
            {
                if (item.Title.ToUpperInvariant().Contains(partOfItem.ToUpperInvariant()) || 
                    item.Description.Text.ToUpperInvariant().Contains(partOfItem.ToUpperInvariant()))
                    return true;

                foreach (Category category in item.GetCategories(CurrentBook))
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
        public string GetCategoriesInString(Book CurrentBook)
        {
            try
            {
                if (GetCategories(CurrentBook) == null || GetCategories(CurrentBook).Count < 1) return "No one categories. ";
            }
            catch(ElementIsNotInCollectionException)
            {
                return "No one categories.";
            }
            
            
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Category categories in GetCategories(CurrentBook))
            {
                stringBuilder.Append(categories.Title).Append(", ");
            }
            return stringBuilder.Remove(stringBuilder.Length - 2, 2).Append(". ").ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Delete(Book CurrentBook)
        {
            if (Book.IsBookIsNotNull(CurrentBook))
            {
                CategoryInItem.DeleteAllConnectionWithItem(CurrentBook, this);
                CurrentBook.ItemsOfBook.Remove(this);

                UpdateDateOfLastChanging();
            }

            return !CurrentBook.ItemsOfBook.Contains(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool DeleteItem(Book CurrentBook, Item item)
        {
            if (IsItemAndItsBookNotNull(CurrentBook, item))
            {
                CurrentBook.ItemsOfBook.Remove(item);
                CategoryInItem.DeleteAllConnectionWithItem(CurrentBook, item);

                item.UpdateDateOfLastChanging();
            }
            return !CurrentBook.ItemsOfBook.Contains(item);
        }
        #endregion
    }

    /// <summary>
    /// Represents a description of the item
    /// </summary>
    public class Description : BookElement
    {
        public string Text { get; set; }
        public List<Object> Files { get; set; }
        
        private Description(Book book, string text) : base(book)
        {
            Text = text;
            Files = new List<object>();
        }
        private Description(Book book, string text, List<Object> list) : this(book, text)
        {
            Files = list;
        }

        public static Description CreateDescription(Book book, string text)
        {
            return new Description(book, text);
        }
        public static Description CreateDescription(Book book, string text, List<Object> list)
        {
            return new Description(book, text, list);
        }

        public bool IsEmptyDescription()
        {
            return String.IsNullOrWhiteSpace(Text) && Files.Count == 0;
        }

        public string GetTitleFromDescription()
        {
            return !String.IsNullOrWhiteSpace(Text) ? GetTitleFromText() : GetTitleFromFiles();
        }
    
        internal string GetTitleFromFiles()
        {
            return $"New item {this.DateOfCreating}";
        }

        internal string GetTitleFromText()
        {
            if (String.IsNullOrEmpty(Text))
                return null;

            string title = Text.Clone() as string;

            if (Text.Contains(" "))
            {
                StringBuilder sb = new StringBuilder();
                for (byte countOfWords = 0; countOfWords < 4 && sb.Length < 30; ++countOfWords)
                {
                    string word = GetNextWord(ref title);
                    if (word != null)
                    {
                        sb.Append(word + " ");
                    }
                    else
                        break;
                }

                return (sb.Length < 30 ? sb.ToString() : sb.ToString().Substring(0, 29));

            }
            else
                return GetNextWord(ref title);
        }

        private string GetNextWord(ref string str)
        {
            if (String.IsNullOrWhiteSpace(str)) return null;

            StringBuilder stringBuilder = new StringBuilder();
            int i = 0;

            for(; i < str.Length && str[i] != ' ' && stringBuilder.Length < 30; ++i)                
            {
                stringBuilder.Append(str[i]);
            }

            str = str.Substring(str.Length > i + 1 ? i + 1 : i);

            return stringBuilder.ToString();
        }
    }
}
