using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotABook.Pages.DetailPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoryListPage : ContentPage
	{
        public CategoryListPage()
        {
            InitializeComponent();
            Models.Category[] categories = NotABook.App.currentBook.CategoriesOfBook.ToArray();
            Models.Book curBook = NotABook.App.currentBook;
        }

        private async void categoriesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Models.Category selectedCategory)
            {
                await Navigation.PushAsync(new ItemsOfBookPage(selectedCategory));
            }
        }
    }
}