using System.Collections.ObjectModel;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using NotABook.Models.Exceptions;

namespace NotABook.Models
{
    public class Item : ElementOfTheBook 
    {
        #region Fields

        private string description;
        
        //public Book CurrentBook { get; private set; }

        #endregion

        #region Prop        

        public string Description
        {
            get => description;
            set
            {
                description = value;
                if (IsTestingOff)
                    OnPropertyChanged("Description");
            }
        }        
        //
        public ObservableCollection<Category> Categories
        {
            get
            {
                if (CurrentBook == null)
                    throw new BookNullException();                
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
                if (CurrentBook == null)
                    throw new BookNullException();

                CategoryInItem.DeleteAllConnectionWithItem(CurrentBook, this);
                foreach (var category in value ?? throw new ArgumentNullException())
                {
                    CategoryInItem.CreateCategoryInItem(CurrentBook, category, this);
                }

                if (IsTestingOff)
                    OnPropertyChanged("Categories");
            }
        }

        public string CategoriesStr { get => this.GetCategoriesInString(); }
        #endregion

        #region Constr
        //
        public Item(Book curBook) : base(curBook)
        {            
            curBook.ItemsOfBook.Add(this);
        }

        public Item(Book curBook, string title) : this(curBook)
        {
            Title = title;
        }

        public Item(Book curBook, string title, string descriprion) : this(curBook, title)
        {
            Description = descriprion;
        }

        public Item(Book curBook, string title, string descriprion, ObservableCollection<Category> categories) : this(curBook, title, descriprion)
        {
            Categories = categories;
        }       
        #endregion

        #region Methods
        //
        public bool ChangeBook(Book newBook)
        {
            Book lastBook = this.CurrentBook ?? throw new BookNullException();            
            lastBook.DeleteItem(this);            
            CurrentBook = newBook ?? throw new BookNullException();
            CurrentBook.ItemsOfBook.Add(this);            
            return !lastBook.ItemsOfBook.Contains(this) && newBook.ItemsOfBook.Contains(this);
        }
        public static bool ChangeBook(Book newBook, Item item)
        {
            if (item == null)
                throw new ItemNullException();

            Book lastBook = item.CurrentBook ?? throw new BookNullException();
            lastBook.DeleteItem(item);
            item.CurrentBook = newBook ?? throw new BookNullException();
            item.CurrentBook.ItemsOfBook.Add(item);
            return !lastBook.ItemsOfBook.Contains(item) && newBook.ItemsOfBook.Contains(item);
        }

        public string GetCategoriesInString()
        {
            if (Categories.Count < 1) return "No one categories";
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Category categories in Categories)
            {
                stringBuilder.Append(categories.Title).Append(", ");
            }
            return stringBuilder.Remove(stringBuilder.Length - 2, 2).ToString();
        }
        //
        public bool DeleteItem()
        {
            if (CurrentBook == null)
                throw new BookNullException();
            
            CurrentBook.ItemsOfBook.Remove(this);
            CategoryInItem.DeleteAllConnectionWithItem(CurrentBook, this);

            if(IsTestingOff)
                CurrentBook.OnPropertyChanged("DateOfLastChanging");
            return !CurrentBook.ItemsOfBook.Contains(this) && !CategoryInItem.IsItemHasConnection(CurrentBook, this);
        }              
        public static  bool DeleteItem(Item item)
        {
            if (item == null)
                throw new ItemNullException();
            if (item.CurrentBook == null)
                throw new BookNullException();

            item.CurrentBook.ItemsOfBook.Remove(item);
            CategoryInItem.DeleteAllConnectionWithItem(item.CurrentBook, item);

            if (item.IsTestingOff)
                item.CurrentBook.OnPropertyChanged("DateOfLastChanging");

            return !item.CurrentBook.ItemsOfBook.Contains(item) && !CategoryInItem.IsItemHasConnection(item.CurrentBook, item);
        }

        #endregion
    }
}
