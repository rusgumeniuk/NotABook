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
        ObservableCollection<Category> SelectedCategories { get; set; } = new ObservableCollection<Category>();
		public AddEditItemPage ()
		{
			InitializeComponent ();
		}

        public AddEditItemPage(Models.Item item)
        {
            InitializeComponent();
            currentItem = item;                        
            BindingContext = item;
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedCategories.Add(picker.SelectedItem as Category);
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
    }
}