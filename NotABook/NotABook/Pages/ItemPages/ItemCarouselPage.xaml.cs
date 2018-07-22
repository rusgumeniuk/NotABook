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
            //Pages.AddEditItemPage(SelectedItem);

            ItemPages.AddEditItemPage page = new ItemPages.AddEditItemPage(SelectedItem as Item);
            await Navigation.PushAsync(page);
        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            var obj = ((Button)sender);
            (obj.CommandParameter as Item).DeleteItem();
        }
    }
}