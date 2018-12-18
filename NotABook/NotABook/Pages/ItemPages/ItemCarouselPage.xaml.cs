using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotABookLibraryStandart.Models;

namespace NotABook.Pages.ItemPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemCarouselPage : CarouselPage
    {
        Book CurBook { get; set; }
        public ItemCarouselPage(Book currentBook)
        {
            InitializeComponent();
            ItemsSource = App.ItemsList;
            CurBook = NotABook.App.currentBook;
            this.BindingContext = this;                                
        }

        public ItemCarouselPage(Book currBook, Category category)
        {
            InitializeComponent();
            ItemsSource = category.ItemsWithThisCategory;
            CurBook = currBook;
            this.BindingContext = this;            
        }

        public ItemCarouselPage(Book curBook, Item item)
        {
            InitializeComponent();
            ItemsSource = NotABook.App.ItemsList;
            CurBook = curBook;
            this.BindingContext = this;                      
        }

        private async void BtnEdit_Clicked(object sender, EventArgs e)
        {           
            await Navigation.PushAsync(new ItemPages.AddEditItemPage(SelectedItem as Item));
        }

       async private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Deleting item", (((Button)sender).CommandParameter as Item).Delete(App.currentBook) ? "Deleted" : "oops, is not deleted", "ok");
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