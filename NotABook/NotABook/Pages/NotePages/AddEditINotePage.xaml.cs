using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotABookLibraryStandart.Models;
using NotABookLibraryStandart.Models.BookElements;

namespace NotABook.Pages.ItemPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEditNotePage : ContentPage
	{
        Note CurrentItem = null;
        ObservableCollection<Category> SelectedCategories { get; set; } = new ObservableCollection<Category>();               
        
		public AddEditNotePage ()
		{
			InitializeComponent ();
            DisplayAlert("Non implemented", "Error", "ok");//PickerAllCategories.ItemsSource = App.currentBook?.CategoriesOfBook ?? new ObservableCollection<Category>();
            PickerSelectedCategories.ItemsSource = SelectedCategories;
        }

        public AddEditNotePage(Note item)
        {
            InitializeComponent();
            CurrentItem = item;
            BindingContext = CurrentItem;
            DisplayAlert("Non implemented", "Error", "ok");//SelectedCategories = item.GetCategories(App.currentBook);

            DisplayAlert("Non implemented", "Error", "ok");//PickerAllCategories.ItemsSource = App.currentBook?.CategoriesOfBook ?? new ObservableCollection<Category>();
            PickerSelectedCategories.ItemsSource = SelectedCategories;
        }


        private void PickerAllCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PickerAllCategories.SelectedIndex != -1)//!!!///???///
            {
                if (!SelectedCategories.Contains(PickerAllCategories.SelectedItem as Category))
                {
                    SelectedCategories.Add(PickerAllCategories.SelectedItem as Category);
                    PickerAllCategories.SelectedIndex = -1;
                }
                else DisplayAlert("Oh no", "u have already added this category", "ok");
            }
        }

        async private void PickerSelectedCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PickerSelectedCategories.SelectedIndex != -1)
            {
                try
                {
                    if (SelectedCategories.Contains(PickerSelectedCategories.SelectedItem as Category))
                    {
                        Category selectedCategory = PickerSelectedCategories.SelectedItem as Category;
                        SelectedCategories.Remove(selectedCategory);
                        PickerSelectedCategories.SelectedIndex = -1;
                    }
                    else await DisplayAlert("hmm", "error select", "oops");
                }
                catch (Exception) { }            
            }
        }

        async private void BtnSave_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(EntryTitle.Text))
            {
                await DisplayAlert("No way", "Your item need some title", "thx");
                return;
            }
                
            if(CurrentItem == null)
            {
                await DisplayAlert("Non implemented", "Error", "ok");//Item newItem = new Item(App.currentBook, EntryTitle.Text, Description.CreateDescription(App.currentBook, EntryDescription.Text), SelectedCategories);
            }
            else
            {
                await DisplayAlert("Non implemented", "Error", "ok");//
                //CurrentItem.Title = EntryTitle.Text;
                //CurrentItem.Description = Description.CreateDescription(App.currentBook, EntryDescription.Text);
                //CurrentItem.SetCategories(App.currentBook, SelectedCategories);
            }

            await Navigation.PopAsync();
        }
    }
}