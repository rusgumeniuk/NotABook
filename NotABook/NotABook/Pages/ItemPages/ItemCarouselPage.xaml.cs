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
            ItemsSource = App.ItemsList;
            Models.Book curBook = NotABook.App.currentBook;
            this.BindingContext = this;                                
        }

        public ItemCarouselPage(Category category)
        {
            InitializeComponent();
            ItemsSource = category.ItemsWithThisCategory;
            Models.Book curBook = NotABook.App.currentBook;
            this.BindingContext = this;            
        }

        public ItemCarouselPage(Item item)
        {
            InitializeComponent();
            ItemsSource = NotABook.App.ItemsList;
            Models.Book curBook = NotABook.App.currentBook;
            this.BindingContext = this;                      
        }

        private async void BtnEdit_Clicked(object sender, EventArgs e)
        {           
            await Navigation.PushAsync(new ItemPages.AddEditItemPage(SelectedItem as Item));
        }

       async private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Deletin item", (((Button)sender).CommandParameter as Item).DeleteItemStr(), "ok");
            await Navigation.PopAsync();
            //try
            //{
            //    //await DisplayAlert("", Book.GetIndexOfItemByID(App.currentBook, (((Button)sender).CommandParameter as Item).Id).ToString(), "OK");
               
            //}
            //catch(IndexOutOfRangeException ex)
            //{
            //   await DisplayAlert("EXC", ex.Message + "\n\n" + ex.StackTrace, "OK");
            //}                       
        }
    }
}