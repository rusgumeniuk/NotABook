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
                int i = 0;
                Console.WriteLine(++i);
                if (CurrentBook == null) throw new ArgumentNullException();
                Console.WriteLine(++i);
                ObservableCollection<Category> categories = new ObservableCollection<Category>();
                Console.WriteLine(++i);
                foreach (CategoryInItem pair in CurrentBook.CategoryInItemsOfBook)
                {
                    Console.WriteLine("cycle");
                    if (pair.Item.Id == Id)
                    {
                        Console.WriteLine("IF start");
                        categories.Add(pair.Category);
                        Console.WriteLine("IF end");
                    }
                }
                return categories;
            }
            set
            {
                //CategoryInItem.DeleteAllConnectionWithItem(this);
                foreach (var category in value)
                {
                    CategoryInItem.CreateCategoryInItem(CurrentBook, category, this);
                }

                if (IsTestingOff)
                    OnPropertyChanged("Categories");
            }
        }

        public string CategoriesStr { get => this.GetCategories(); }
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

        public string GetCategories()
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
            if (CurrentBook == null) throw new ArgumentNullException();
            this.Categories.Clear();
            CurrentBook.ItemsOfBook.Remove(this);
            CategoryInItem.DeleteAllConnectionWithItem(CurrentBook, this);
            CurrentBook.OnPropertyChanged("DateOfLastChanging");
        }

        #endregion
    }
}
