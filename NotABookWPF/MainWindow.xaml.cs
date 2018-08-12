using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

using NotABookLibraryStandart.Models;

namespace NotABookWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Lists etc
        public static Book currentBook = null;

        public static ObservableCollection<Book> Books
        {
            get => Book.Books;
            set => Book.Books = value;
        }

        public static ObservableCollection<Item> ItemsList => currentBook?.ItemsOfBook;
        public static ObservableCollection<Category> CategoriesList => currentBook?.CategoriesOfBook;
        public static ObservableCollection<CategoryInItem> CategoryInItemsList => currentBook?.CategoryInItemsOfBook;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            SetUp();
            LeftListView.ItemsSource = Books;            
            ItemsListView.ItemsSource = ItemsList;
        }

        private void SetUp()
        {
            currentBook = new Book("My first book");
            Book secondBook = new Book("Second book");
            Book thirdBook = new Book("Third book");

            Category chocolateCategory = new Category(currentBook, "Chocolate");
            Category flourCategory = new Category(currentBook, "Flour");
            Category eggsCategory = new Category(currentBook, "Eggs");
            Category potatoCategory = new Category(currentBook, "Potato");
            Category tomatoCategory = new Category(currentBook, "Tomato");
            Category chickenCategory = new Category(currentBook, "Chicken");

            Item chocolateBiscuit = new Item(currentBook, "Chocolate biscuit", Description.CreateDescription("The best chocolate cake ever"), new ObservableCollection<Category>() { chocolateCategory, flourCategory, eggsCategory });
            Item salatWithPotatoAndTomato = new Item(currentBook, "Salat with potat, tomatos and eggs", Description.CreateDescription("Very healthy salat"), new ObservableCollection<Category>() { potatoCategory, tomatoCategory, eggsCategory });
            Item chicken = new Item(currentBook, "Chicken", Description.CreateDescription("Chicken like in KFC"), new ObservableCollection<Category>() { chickenCategory, eggsCategory });
        }

        private void BtnText_Click(object sender, RoutedEventArgs e)
        {
            //BtnText.Content = ModelsLibrary.Class1.Method();
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
        }
        private void MenuItemHello_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((MenuItem)sender).Header.ToString());
        }

        private void ItemsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TBItem.Text = (ItemsListView.SelectedItem as Item).Title;
        }

      

        private void TextBoxFindItem_GotFocus(object sender, RoutedEventArgs e)
        {            
            TextBoxFindItem.Text = String.Empty;         
        }

        private void TextBoxFindItem_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxFindItem.Text = "Find item";
            ItemsListView.ItemsSource = MainWindow.currentBook.ItemsOfBook;
        }

        private void TextBoxFindItem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxFindItem.IsFocused)
            {
                if (String.IsNullOrWhiteSpace(TextBoxFindItem.Text))
                    ItemsListView.ItemsSource = MainWindow.currentBook.ItemsOfBook;
                else
                {
                    ItemsListView.ItemsSource = MainWindow.currentBook?.FindItems(TextBoxFindItem.Text) ?? null;
                }
            }                   
        }
    }
}
