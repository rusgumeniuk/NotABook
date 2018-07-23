using NotABook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotABook.Pages.ItemPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemCarouselPage : CarouselPage
    {
        public ItemCarouselPage(Models.Book currentBook)
        {
            InitializeComponent();
            ItemsSource = NotABook.App.ItemsList;
            Models.Book curBook = NotABook.App.currentBook;
            this.BindingContext = this;                                
        }
        public ItemCarouselPage(Category category)
        {
            InitializeComponent();
            ItemsSource = category.ItemsWithThisCategory;
            this.BindingContext = this;            
        }


        public ItemCarouselPage(Item item)
        {
            InitializeComponent();
            ItemsSource = NotABook.App.ItemsList;
            Models.Book curBook = NotABook.App.currentBook;
            this.BindingContext = this;                      
            

            //lblTitle.Text = item.Title;
            //lblDateOfChanging.Text = item.DateOfLastChanging.ToString();
            //lblCategories.Text = item.GetCategories();
            //lblDescript.Text = item.Description;            

        }

        private async void btnEdit_Clicked(object sender, EventArgs e)
        {
            //ItemPages.AddEditItemPage page = new ItemPages.AddEditItemPage(SelectedItem as Item);
            await Navigation.PushAsync(new ItemPages.AddEditItemPage(SelectedItem as Item));
        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
           // var obj = ((Button)sender);
            (((Button)sender).CommandParameter as Item).DeleteItem();
        }
    }
}