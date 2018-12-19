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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.ComponentModel;
using NotABookLibraryStandart.Models;
using System.IO;
using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.BookElements.Contents;

namespace NotABookWPF.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Init
        #region Lists etc
        public static Book currentBook = null;

        public static ObservableCollection<Book> Books
        {
            get => Book.Books;
            set => Book.Books = value;
        }
        public static ObservableCollection<Note> Notes => currentBook?.Notes;
        public static ObservableCollection<Item> ItemsList => currentBook?.ItemsOfBook;
        public static ObservableCollection<Category> CategoriesList => currentBook?.CategoriesOfBook;
        public static ObservableCollection<CategoryInItem> CategoryInItemsList => currentBook?.CategoryInItemsOfBook;

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            SetUpModels();

            ListViewBooks.ItemsSource = Books;
            ComboBoxCurrentBook.ItemsSource = Books;

            UpdateCurrentBook();
        }

        private void SetUpModels()
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

            currentBook.CategoriesOfBook.Add(chocolateCategory);
            currentBook.CategoriesOfBook.Add(flourCategory);
            currentBook.CategoriesOfBook.Add(eggsCategory);
            currentBook.CategoriesOfBook.Add(potatoCategory);
            currentBook.CategoriesOfBook.Add(tomatoCategory);
            currentBook.CategoriesOfBook.Add(chickenCategory);

            currentBook.CategoriesOfBook.Add(new Category(currentBook, "test1"));
            currentBook.CategoriesOfBook.Add(new Category(currentBook, "test2"));
            currentBook.CategoriesOfBook.Add(new Category(currentBook, "test3"));
            currentBook.CategoriesOfBook.Add(new Category(currentBook, "test4"));
            currentBook.CategoriesOfBook.Add(new Category(currentBook, "test5"));
            currentBook.CategoriesOfBook.Add(new Category(currentBook, "test6"));
            currentBook.CategoriesOfBook.Add(new Category(currentBook, "test7"));


            Note chocolateBiscuit = new Note(currentBook, "Chocolate biscuit", new List<IContent>() { new TextContent() { Content = "The best chocolate cake ever" } }, new List<Category>() { chocolateCategory, flourCategory, eggsCategory });
            Note salatWithPotatoAndTomato = new Note(currentBook, "Salat with potat, tomatos and eggs", new List<IContent>() { new TextContent() { Content = "Very healthy salat" } }, new ObservableCollection<Category>() { potatoCategory, tomatoCategory, eggsCategory });
            Note chicken = new Note(currentBook, "Chicken", new List<IContent>() { new TextContent() { Content = "Chicken like in KFC" } }, new ObservableCollection<Category>() { chickenCategory, eggsCategory });
            currentBook.Notes.Add(chocolateBiscuit);
            currentBook.Notes.Add(salatWithPotatoAndTomato);
            currentBook.Notes.Add(chicken);

            UpdateCurrentBook();
        }
        #endregion

        private void UpdateCurrentBook()
        {
            ListBoxItems.ItemsSource = MainWindow.currentBook?.Notes;
            CategoryInNoteListBox.ItemsSource = new ObservableCollection<Category>();
            AllCategoriesListBox.ItemsSource = CategoriesList;
            ComboBoxCurrentBook.SelectedItem = MainWindow.currentBook;
            ListViewBooks.SelectedItem = MainWindow.currentBook;
            TextBlockCountOfItems.Text = MainWindow.currentBook?.Notes.Count.ToString() + " ";
            TBCurrentBook.Text = currentBook?.Title ?? "Undefind";
        }

        #region Menu panel
        private void MenuItemFAQ_Click(object sender, RoutedEventArgs e)
        {
            (new FAQWindow() { Title = "FAQ" }).Show();
        }
        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello! \n I Ruslan Humeniuk.\n I am KPI student and this is my first WPF project");
        }
        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnNewNote_Click(object sender, RoutedEventArgs e)
        {
            (new AddEditItemWindow(currentBook) { Title = "Creating of item" }).Show();
        }
        #endregion

        #region Books stack panel
        private void TBEditBookTitle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (sender as TextBox).IsEnabled = true;
            MessageBox.Show("asd");
        }
        private void ListViewBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewBooks.SelectedItem != null && ListViewBooks.SelectedItem as Book != currentBook)
            {
                currentBook = ListViewBooks.SelectedItem as Book;
                UpdateCurrentBook();
            }
        }
        private void TBEditBookTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).IsEnabled = false;
            //((sender as TextBox) as Book).Title = (sender as TextBox).Text;
            (ListViewBooks.SelectedItem as Book).Title = (sender as TextBox).Text;
        }
        #region context menu
        private void MenuItemRemoveItems_Click(object sender, RoutedEventArgs e)
        {
            Book.ClearItemsList(((sender as MenuItem).CommandParameter as Book));
        }
        private void MenuItemRemoveCategories_Click(object sender, RoutedEventArgs e)
        {
            Book.ClearCaregoriesList(((sender as MenuItem).CommandParameter as Book));
        }
        private void MenuItemRemoveAllConnections_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItemRemoveAllElements_Click(object sender, RoutedEventArgs e)
        {
            Book.RemoveAllElementsOfBook(((sender as MenuItem).CommandParameter as Book));
        }
        private void MenuItemDeleteBook_Click_1(object sender, RoutedEventArgs e)
        {
            ((sender as MenuItem).CommandParameter as Book).Delete();
        }
        #endregion

        #endregion

        #region Notes stack panel       

        #region Find text box
        private void TextBoxFindItem_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxFindItem.Text = String.Empty;
        }
        private void TextBoxFindItem_LostFocus(object sender, RoutedEventArgs e)
        {
            if(ListBoxItems.Items.Count < 1)
            {
                TextBoxFindItem.Text = "Find note";
                UpdateCurrentBook();
                HideNotePanel();
            }            
        }
        private void TextBoxFindItem_LostMouseCapture(object sender, MouseEventArgs e)
        {
            if (ListBoxItems.Items.Count < 1)
            {
                TextBoxFindItem.Text = "Find note";
                UpdateCurrentBook();
                HideNotePanel();
            }
        }
        private void TextBoxFindItem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxFindItem.IsFocused)
            {
                HideNotePanel();
                if (String.IsNullOrWhiteSpace(TextBoxFindItem.Text))
                    ListBoxItems.ItemsSource = MainWindow.currentBook.Notes;
                else
                {
                    IList<Note> result = MainWindow.currentBook?.FindNotes(TextBoxFindItem.Text);
                    TextBlockCountOfItems.Text = (result?.Count ?? 0).ToString() + " ";
                    ListBoxItems.ItemsSource = result;
                }
            }
        }
        #endregion

        #region Note selection
        private void ListBoxItems_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(ListBoxItems.SelectedItem != null)
            {
                StackPanelItemPanel.DataContext = ListBoxItems.SelectedItem as Note;
                CategoryInNoteListBox.ItemsSource = (ListBoxItems.SelectedItem as Note).Categories;
                StackPanelItemPanel.Visibility = Visibility.Visible;
            }           
        }
        private void ListBoxItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (new AddEditItemWindow(currentBook, ListBoxItems.SelectedItem as Note) { Title = "Editing note" }).Show();
        }
        #endregion
        private void ComboBoxCurrentBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxCurrentBook.SelectedItem != null && ComboBoxCurrentBook.SelectedItem as Book != currentBook)
            {
                MainWindow.currentBook = ComboBoxCurrentBook.SelectedItem as Book;
                UpdateCurrentBook();
            }
        }
        #endregion

        #region Note stack panel

        #region categories lists panel              
        private void CategoryInNoteListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            (CategoryInNoteListBox.ItemsSource as IList<Category>).Remove(CategoryInNoteListBox.SelectedItem as Category);
            CategoryInNoteListBox.ItemsSource = new ObservableCollection<Category>(CategoryInNoteListBox.ItemsSource as IEnumerable<Category>);
        }
        private void AllCategoriesListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CategoryInNoteListBox.Items.Contains(AllCategoriesListBox.SelectedItem))
                MessageBox.Show("Note already marked by this category!");
            else
            {
                (CategoryInNoteListBox.ItemsSource as IList<Category>).Add(AllCategoriesListBox.SelectedItem as Category);
                CategoryInNoteListBox.ItemsSource = new ObservableCollection<Category>(CategoryInNoteListBox.ItemsSource as IEnumerable<Category>);
            }
        }
        private void BtnCreateCategory_Click(object sender, RoutedEventArgs e)
        {
            (new AddEditCategoryWindow(currentBook) { Title = "Creating of category" }).Show();
        }
        #endregion

        #endregion

       private void HideNotePanel()
        {
            StackPanelItemPanel.Visibility = Visibility.Collapsed;
        }
    }
}
