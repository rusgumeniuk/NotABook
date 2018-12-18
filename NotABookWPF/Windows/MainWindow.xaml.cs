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



            Note chocolateBiscuit = new Note(currentBook, "Chocolate biscuit", new List<IContent>() { new TextContent() { Content = "The best chocolate cake ever" } }, new List<Category>() { chocolateCategory, flourCategory, eggsCategory });
            Note salatWithPotatoAndTomato = new Note(currentBook, "Salat with potat, tomatos and eggs", new List<IContent>() { new TextContent() { Content = "Very healthy salat" } }, new ObservableCollection<Category>() { potatoCategory, tomatoCategory, eggsCategory });
            Note chicken = new Note(currentBook, "Chicken", new List<IContent>() { new TextContent() { Content = "Chicken like in KFC" } }, new ObservableCollection<Category>() { chickenCategory, eggsCategory });
            currentBook.Notes.Add(chocolateBiscuit);
            currentBook.Notes.Add(salatWithPotatoAndTomato);
            currentBook.Notes.Add(chicken);
        }

        private void UpdateCurrentBook()
        {
            ListBoxItems.ItemsSource = MainWindow.currentBook?.Notes;
            ComboBoxCurrentBook.SelectedItem = MainWindow.currentBook;
            ListViewBooks.SelectedItem = MainWindow.currentBook;
            TextBlockCountOfItems.Text = MainWindow.currentBook?.Notes.Count.ToString() + " ";
            TBCurrentBook.Text = currentBook?.Title ?? "Undefind";
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

        private void ListBoxItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //StackPanelItemPanel.DataContext = ListBoxItems.SelectedItem as Note;
            (new AddEditItemWindow(currentBook, ListBoxItems.SelectedItem as Note) { Title = "Editing note" }).Show();
        }

        private void TextBoxFindItem_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxFindItem.Text = String.Empty;
        }
        private void TextBoxFindItem_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxFindItem.Text = "Find note";
            UpdateCurrentBook();
        }
        private void TextBoxFindItem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxFindItem.IsFocused)
            {
                if (String.IsNullOrWhiteSpace(TextBoxFindItem.Text))
                    ListBoxItems.ItemsSource = MainWindow.currentBook.Notes;
                else
                {
                    throw new NotImplementedException();
                    //ObservableCollection<Note> notes = MainWindow.currentBook?.FindItems(currentBook, TextBoxFindItem.Text);
                    //TextBlockCountOfItems.Text = (items?.Count ?? 0).ToString() + " ";
                    //ListBoxItems.ItemsSource = items;
                }
            }
        }

        private void ComboBoxCurrentBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxCurrentBook.SelectedItem != null && ComboBoxCurrentBook.SelectedItem as Book != currentBook)
            {
                MainWindow.currentBook = ComboBoxCurrentBook.SelectedItem as Book;
                UpdateCurrentBook();
            }
        }




        private void TBEditItemTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            if (StackPanelItemPanel.DataContext != null)
            {
                if((StackPanelItemPanel.DataContext as Note).IndexOfTextContent != -1)
                {
                    (StackPanelItemPanel.DataContext as Note).SetTextToFirstTextContent = TBDescription.Text;
                }                
            }
        }
        private void TBDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            if (StackPanelItemPanel.DataContext != null)
            {
                if ((StackPanelItemPanel.DataContext as Note).IndexOfTextContent != -1)
                {
                    (StackPanelItemPanel.DataContext as Note).SetTextToFirstTextContent = TBDescription.Text;
                }
            }
        }

        #region Book list view
        private void LeftListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewBooks.SelectedItem != null && ListViewBooks.SelectedItem as Book != currentBook)
            {
                currentBook = ListViewBooks.SelectedItem as Book;
                UpdateCurrentBook();
            }
        }




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

        private void TBEditBookTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).IsEnabled = false;
            //((sender as TextBox) as Book).Title = (sender as TextBox).Text;
            (ListViewBooks.SelectedItem as Book).Title = (sender as TextBox).Text;
        }

        #endregion

        private void TBEditBookTitle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (sender as TextBox).IsEnabled = true;
            MessageBox.Show("asd");
        }

        private void BtnNewItem_Click(object sender, RoutedEventArgs e)
        {
            (new AddEditItemWindow(currentBook) { Title = "Creating of item" }).Show();
        }

        private void MenuItemDeleteBook_Click_1(object sender, RoutedEventArgs e)
        {
            ((sender as MenuItem).CommandParameter as Book).Delete();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var note in Notes)
            {
                sb.AppendLine(note.Title + " : " + note.Contents.Count);
            }
            MessageBox.Show(sb.ToString());
        }

        private void ListBoxItems_MouseEnter(object sender, MouseEventArgs e)
        {
            //StackPanelItemPanel.DataContext = ListBoxItems.SelectedItem as Note;
        }
    }
}
