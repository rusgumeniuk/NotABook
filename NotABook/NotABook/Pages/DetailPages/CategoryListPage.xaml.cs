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

        async private void OnEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HelpedPages.AddEditCategoryPage(((MenuItem)sender).CommandParameter as Models.Category));
        }

        async private void OnDelete_Clicked(object sender, EventArgs e)
        {
            
            if (await DisplayAlert("Delete category", "Do u want to delete this category?", "Yes", "No"))
            (((MenuItem)sender).CommandParameter as Models.Category).DeleteCategory();
            
        }

        async private void BtnAddNewCategory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HelpedPages.AddEditCategoryPage());
        }
    }
}