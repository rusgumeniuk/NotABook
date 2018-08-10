using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotABookLibraryStandart.Models;

namespace NotABook.Pages.DetailPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoryListPage : ContentPage
	{
        //ObservableCollection<Category> Categories => App.currentBook?.CategoriesOfBook ?? new ObservableCollection<Category>();

        public CategoryListPage ()
		{
			InitializeComponent ();
            //BindingContext = this;
            ListOfCategories.ItemsSource = App.currentBook?.CategoriesOfBook ?? new ObservableCollection<Category>();
        }

        async private void ListOfCategories_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if(e.Item is Category selectedCategory)
            {
                await Navigation.PushAsync(new ItemsOfBookPage(selectedCategory));
            }
        }

        async private void OnEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HelpedPages.AddEditCategoryPage(((MenuItem)sender).CommandParameter as Category));
        }

        async private void OnDelete_Clicked(object sender, EventArgs e)
        {
            if(await DisplayAlert("Delete category",
                "Do u want to delete category?",
                "Yes", "NO"))
            {
                await DisplayAlert("Deleting of category", 
                                    (((MenuItem)sender).CommandParameter as Category).DeleteCategoryStr(),
                                     "OK");
            }
        }

        async private void OnDeleteConnections_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Delete connections with category",
                "Do u want to delete all connections?",
                "Yes", "NO"))
            {
                await DisplayAlert("Deleting connections",
                    (((MenuItem)sender).CommandParameter as Category).RemoveCategoryFromAllItemsStr(),
                    "OK");
            }
        }

        async private void BtnAddNewCategory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HelpedPages.AddEditCategoryPage());
        }
    }
}