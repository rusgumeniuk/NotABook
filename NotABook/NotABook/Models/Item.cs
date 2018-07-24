using System.Collections.ObjectModel;
using System.Text;
namespace NotABook.Models
{
    public class Item : BaseClass 
    {
        #region Fields

        private string description;

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
                ObservableCollection<Category> categories = new ObservableCollection<Category>();
                foreach (var item in App.currentBook?.CategoryInItemsOfBook)
                {
                    if (item.Item.Id == this.Id) categories.Add(item.Category);
                }
                return categories;
            }
            set
            {                                
                foreach (var category in value)
                {
                    CategoryInItem.CreateCategoryInItem(category, this);
                }
                OnPropertyChanged("Categories");
            }
        }

        public string CategoriesStr { get => this.GetCategories(); }
        #endregion

        #region Constr
        public Item() : base()
        {
            if (NotABook.App.currentBook != null)
                NotABook.App.currentBook.ItemsOfBook.Add(this);
        }

        public Item(string title) : this()
        {
            Title = title;
        }

        public Item(string title, string descriprion) : this(title)
        {
            Description = descriprion;
        }

        public Item(string title, string descriprion, ObservableCollection<Category> categories) : this(title, descriprion)
        {
            Categories = categories;
        }
        #endregion

        #region Methods

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
            this.Categories.Clear();
            App.currentBook?.ItemsOfBook.Remove(this);
            CategoryInItem.DeleteAllConnectionWithItem(this);
            App.currentBook?.OnPropertyChanged("DateOfLastChanging");
        }

        #endregion
    }
}
