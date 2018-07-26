using System.Collections.ObjectModel;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NotABook.Models
{
    public class Item : BaseClass 
    {
        #region Fields

        private string description;
        
        public Book CurrentBook { get; private set; }

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

        public ObservableCollection<Category> Categories
        {
            get
            {
                if (CurrentBook == null) throw new ArgumentNullException();             
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
                CategoryInItem.DeleteAllConnectionWithItem(CurrentBook, this);
                foreach (var category in value)
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
        public Item(Book curBook) : base()
        {
            CurrentBook = curBook ?? throw new ArgumentNullException();
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

        public bool ChangeBook(Book newBook)
        {
            //TEST IT!
            Book lastBook = CurrentBook;
            CurrentBook = newBook ?? throw new ArgumentNullException();
            return !lastBook.ItemsOfBook.Contains(this) && newBook.ItemsOfBook.Contains(this);
        }

        public string GetCategoriesInString()
        {
            if (Categories.Count < 1) return "No one categories";
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Category categories in Categories)
            {
                stringBuilder.Append(categories.Title).Append(", ");
            }
            return stringBuilder.Remove(stringBuilder.Length - 2, 2).Append(".").ToString();
        }

        public void DeleteItem()
        {
            if (CurrentBook == null)
                throw new ArgumentNullException();
            
            CurrentBook.ItemsOfBook.Remove(this);
            CategoryInItem.DeleteAllConnectionWithItem(CurrentBook, this);

            if(IsTestingOff)
                CurrentBook.OnPropertyChanged("DateOfLastChanging");
        }
        

        #endregion
    }
}
