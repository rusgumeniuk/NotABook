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
            SelectedCategories = item.Categories ?? new ObservableCollection<Category>();
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
                Category selectedCategory = (Category)(PickerOfSelectedCategories.SelectedItem as Category);
                //int selectedIndex = (int)(PickerOfSelectedCategories.SelectedIndex);
                
                try
                {
                    SelectedCategories.Remove(selectedCategory);
                    //SelectedCategories.RemoveAt(selectedIndex);
                    //PickerOfSelectedCategories.
                }
                catch (Exception ex)
                {
                    //DisplayAlert("Title", ex.Message, "OK");
                    //return;
                    
                    //return;
                }
                //DisplayAlert("Category removed", selectedCategory.Title + " was removed from list", "ok");
               // PickerOfSelectedCategories.SelectedIndex = -1;
            }            
        }


        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            if (!IsRequiredFieldsIsFillIn())
            {
                await DisplayAlert("Error", "This item want to has Title", "ok");
                return;
            }

            if (currentItem != null)
            {
                CategoryInItem.DeleteAllConnectionWithItem(NotABook.App.currentBook, currentItem);
                //try
                //{
                //    //CategoryInItem.DeleteAllConnectionWithItem(NotABook.App.currentBook, currentItem);
                //}
                //catch (Exception ex)
                //{
                //    await DisplayAlert("btn", ex.Message, "ok");
                //}

                currentItem.Title = entryTitle.Text;
                currentItem.Categories = SelectedCategories;
                currentItem.Description = editorDescript.Text;
            }
            else
            {
                Item newItem = new Item(App.currentBook)
                {
                    Title = entryTitle.Text,
                    Description = editorDescript.Text,
                    Categories = SelectedCategories
                };
            }            
            await Navigation.PopAsync();                  
        }        

        private void CreatePickers()
        {
            PickerOfAllCategories.ItemsSource = App.CategoriesList;
            PickerOfAllCategories.SelectedIndexChanged += PickerOfAllCategories_SelectedIndexChanged; ;

            PickerOfSelectedCategories.SetBinding(Picker.ItemsSourceProperty, new Binding() { Source = SelectedCategories, Mode = BindingMode.TwoWay });//,  BindingMode.TwoWay, null,null);
            PickerOfSelectedCategories.SelectedIndexChanged += PickerOfSelectedCategories_SelectedIndexChanged;            
        }

        private void CreatePickers(Item item)
        {
            PickerOfAllCategories.ItemsSource = App.CategoriesList;
            PickerOfAllCategories.SelectedIndexChanged += PickerOfAllCategories_SelectedIndexChanged; ;

            SelectedCategories = item.Categories ?? new ObservableCollection<Category>();
            //PickerOfSelectedCategories.ItemsSource = SelectedCategories;
            //Binding 
            PickerOfSelectedCategories.SetBinding(Picker.ItemsSourceProperty, new Binding() {Source=SelectedCategories , Mode = BindingMode.TwoWay});//,  BindingMode.TwoWay, null,null);
            PickerOfSelectedCategories.SelectedIndexChanged += PickerOfSelectedCategories_SelectedIndexChanged;
        }

        private bool IsRequiredFieldsIsFillIn()
        {
            return !String.IsNullOrWhiteSpace(entryTitle.Text);
        }
    }
}
