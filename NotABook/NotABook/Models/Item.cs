using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
namespace NotABook.Models
{
    public class Item : BaseClass 
    {
        #region Prop
        public string Description { get; set; }

        public ObservableCollection<Category> Categories { get; set; }

        public string CategoriesStr { get => this.GetCategories(); }
        #endregion

        #region Constr
        public Item() : base()
        {
            if (NotABook.App.currentBook != null)
                NotABook.App.currentBook.ItemsOfBook.Add(this.Id, this);
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
            NotABook.App.currentBook.ItemsOfBook.Remove(this.Id);                       
        }
        #endregion
    }
}
