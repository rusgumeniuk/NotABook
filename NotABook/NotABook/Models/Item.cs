using System.Collections.ObjectModel;
using System.Text;
using System;
namespace NotABook.Models
{
    public class Item : BaseClass 
    {
        #region Fields

        private string description;

        private Book currentBook;

        #endregion

        #region Prop        

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }        

        public ObservableCollection<Category> Categories
        {
            get
            {
                if (currentBook == null) return null;

                ObservableCollection<Category> categories = new ObservableCollection<Category>();
                foreach (var item in currentBook.CategoryInItemsOfBook)
                {
                    if (item.Item.Id == this.Id) categories.Add(item.Category);
                }
                return categories;
            }
            set
            {
                //CategoryInItem.DeleteAllConnectionWithItem(this);
                foreach (var category in value)
                {
                    CategoryInItem.CreateCategoryInItem(currentBook, category, this);
                }
                OnPropertyChanged("Categories");
            }
        }

        public string CategoriesStr { get => this.GetCategories(); }
        #endregion

        #region Constr
        public Item(Book curBook) : base()
        {
            currentBook = curBook;
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
            Book lastBook = currentBook;
            currentBook = newBook ?? throw new ArgumentNullException();
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
            if (currentBook == null) throw new ArgumentNullException();
            this.Categories.Clear();
            currentBook.ItemsOfBook.Remove(this);
            CategoryInItem.DeleteAllConnectionWithItem(currentBook, this);
            currentBook.OnPropertyChanged("DateOfLastChanging");
        }

        #endregion
    }
}
