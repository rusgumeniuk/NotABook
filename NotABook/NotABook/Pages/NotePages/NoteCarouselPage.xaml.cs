using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotABookLibraryStandart.Models;
using NotABookLibraryStandart.Models.BookElements;

namespace NotABook.Pages.ItemPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteCarouselPage : CarouselPage
    {
        Book CurBook { get; set; }
        public NoteCarouselPage(Book currentBook)
        {
            InitializeComponent();
            ItemsSource = App.currentBook.Notes;
            CurBook = NotABook.App.currentBook;
            this.BindingContext = this;                                
        }

        public NoteCarouselPage(Book currBook, Category category)
        {
            InitializeComponent();
            DisplayAlert("Non implemented", "Error", "ok");//ItemsSource = category.ItemsWithThisCategory;
            CurBook = currBook;
            this.BindingContext = this;            
        }

        public NoteCarouselPage(Book curBook, Note item)
        {
            InitializeComponent();
            ItemsSource = NotABook.App.currentBook.Notes;
            CurBook = curBook;
            this.BindingContext = this;                      
        }

        private async void BtnEdit_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Non implemented", "Error", "ok");//await Navigation.PushAsync(new ItemPages.AddEditItemPage(SelectedItem as Item));
        }

       async private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Non implemented", "Error", "ok");//await DisplayAlert("Deleting item", (((Button)sender).CommandParameter as Item).Delete(App.currentBook) ? "Deleted" : "oops, is not deleted", "ok");
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