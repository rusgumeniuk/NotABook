using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NotABook.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotABook.Pages.ItemPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEditItemPage : ContentPage
	{
        Models.Item currentItem = null;
        ObservableCollection<Category> SelectedCategories = new ObservableCollection<Category>();      

        public AddEditItemPage ()
		{
			InitializeComponent ();
            CreatePickers();       
        }

        public AddEditItemPage(Models.Item item)
        {
            InitializeComponent();
            currentItem = item;
            BindingContext = item;
            CreatePickers(item);
        }


        private void PickerOfAllCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PickerOfAllCategories.SelectedIndex != -1)
            {
                if (!SelectedCategories.Contains(PickerOfAllCategories.SelectedItem as Category))
                {
                    SelectedCategories.Add(PickerOfAllCategories.SelectedItem as Category);
                    PickerOfAllCategories.SelectedIndex = -1;
                }
                else DisplayAlert("Ooops", "You have already added this category", "Okey");
            }
        }

        private void PickerOfSelectedCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PickerOfSelectedCategories.SelectedIndex != -1)
            {
                //Category selectedCategory = PickerOfSelectedCategories.SelectedItem as Category;
                try
                {
                    if (SelectedCategories.Count == 1) SelectedCategories.Remove(PickerOfSelectedCategories.SelectedItem as Category); // ???
                    else SelectedCategories.Remove(PickerOfSelectedCategories.SelectedItem as Category);
                }
                catch(Exception ex)
                {
                    DisplayAlert("", ex.Message, "ok");
                }                               
                DisplayAlert("Category removed","item was removed from list", "ok");
                PickerOfSelectedCategories.SelectedIndex = -1;
            }            
        }


        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            if (currentItem != null)
            {
                currentItem.Title = entryTitle.Text;
                currentItem.Categories = SelectedCategories;
                currentItem.Description = editorDescript.Text;
            }
            else
            {
                Item newItem = new Item()
                {
                    Title = entryTitle.Text,
                    Description = editorDescript.Text,
                    Categories = SelectedCategories
                };
            }            
            await Navigation.PopAsync(true);                  
        }        

        private void CreatePickers()
        {
            PickerOfAllCategories.ItemsSource = App.CategoriesList;
            PickerOfAllCategories.SelectedIndexChanged += PickerOfAllCategories_SelectedIndexChanged; ;

            PickerOfSelectedCategories.ItemsSource = SelectedCategories;
            PickerOfSelectedCategories.SelectedIndexChanged += PickerOfSelectedCategories_SelectedIndexChanged;
        }

        private void CreatePickers(Item item)
        {
            PickerOfAllCategories.ItemsSource = App.CategoriesList;
            PickerOfAllCategories.SelectedIndexChanged += PickerOfAllCategories_SelectedIndexChanged; ;

            SelectedCategories = item.Categories;
            PickerOfSelectedCategories.ItemsSource = SelectedCategories;
            PickerOfSelectedCategories.SelectedIndexChanged += PickerOfSelectedCategories_SelectedIndexChanged;
        }
    }
}
