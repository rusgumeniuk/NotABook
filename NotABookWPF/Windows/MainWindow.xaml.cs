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
        public static ObservableCollection<Category> CategoriesList { get; set; } = new ObservableCollection<Category>();

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

            Book.Books.Add(currentBook);
            Book.Books.Add(secondBook);
            Book.Books.Add(thirdBook);

            Category chocolateCategory = new Category(currentBook, "Chocolate");
            Category flourCategory = new Category(currentBook, "Flour");
            Category eggsCategory = new Category(currentBook, "Eggs");
            Category potatoCategory = new Category(currentBook, "Potato");
            Category tomatoCategory = new Category(currentBook, "Tomato");
            Category chickenCategory = new Category(currentBook, "Chicken");

            CategoriesList.Add(chocolateCategory);
            CategoriesList.Add(flourCategory);
            CategoriesList.Add(eggsCategory);
            CategoriesList.Add(potatoCategory);
            CategoriesList.Add(tomatoCategory);
            CategoriesList.Add(chickenCategory);

            CategoriesList.Add(new Category(currentBook, "test1"));
            CategoriesList.Add(new Category(currentBook, "test2"));
            CategoriesList.Add(new Category(currentBook, "test3"));
            CategoriesList.Add(new Category(currentBook, "test4"));
            CategoriesList.Add(new Category(currentBook, "test5"));
            CategoriesList.Add(new Category(currentBook, "test6"));
            CategoriesList.Add(new Category(currentBook, "test7"));


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
            HideNotePanel();
        }

        #region Menu panel
        private void MenuItemCreateNote_Click(object sender, RoutedEventArgs e)
        {
            (new AddEditINoteWindow(currentBook) { Title = "Creating" }).Show();
        }
        private void MenuItemCreateBook_Click(object sender, RoutedEventArgs e)
        {
            (new AddEditBook() { Title = "Creating" }).Show();
        }
        private void MenuItemCreateCategory_Click(object sender, RoutedEventArgs e)
        {
            (new AddEditCategoryWindow(currentBook)).Show();
        }
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
            (new AddEditINoteWindow(currentBook) { Title = "Creating of item" }).Show();
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
        private void ListViewBooks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (new AddEditBook(ListViewBooks.SelectedItem as Book) { Title = "Creating" }).Show();
        }
        private void TBEditBookTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).IsEnabled = false;
            (ListViewBooks.SelectedItem as Book).Title = (sender as TextBox).Text;
        }

        #region context menu
        private void MenuItemRemoveItems_Click(object sender, RoutedEventArgs e)
        {
            Book.ClearNotesList(((sender as MenuItem).CommandParameter as Book));
        }
        private void MenuItemDeleteBook_Click_1(object sender, RoutedEventArgs e)
        {
            ((sender as MenuItem).CommandParameter as Book).Delete();
        }
        private void MenuItemEditBook_Click(object sender, RoutedEventArgs e)
        {
            (new AddEditBook(((sender as MenuItem).CommandParameter as Book)) { Title = "Editing" }).Show();
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
            if (ListBoxItems.Items.Count < 1)
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
            if (ListBoxItems.SelectedItem != null)
            {
                HideNotePanel();
                StackPanelItemPanel.DataContext = ListBoxItems.SelectedItem as Note;
                CategoryInNoteListBox.ItemsSource = (ListBoxItems.SelectedItem as Note).Categories;
                InputContentsToStackPanel(ListBoxItems.SelectedItem as Note);
                AddTextBoxIfNoContent();
                StackPanelItemPanel.Visibility = Visibility.Visible;
            }
        }

        private void UpdateNoteData()
        {
            Note note = StackPanelContent.DataContext as Note;
            note.Title = TBEditItemTitle.Text;
            note.Categories = CategoryInNoteListBox.ItemsSource as ObservableCollection<Category>;
            IList<IContent> contents = GetContentFromChildren(StackPanelContent.Children);
            IList<IContent> addContent = new List<IContent>();
            foreach (var newContent in contents)
            {
                bool exist = false;
                foreach (var existingContent in note.Contents)
                {
                    if (newContent.Equals(existingContent))
                    {
                        exist = true;
                        addContent.Add(existingContent);
                        break;
                    }
                }
                if (exist)
                    continue;
                else
                {
                    addContent.Add(newContent);
                }

            }
            note.Contents = addContent;
            DataContext = note;
            this.UpdateLayout();
        }
        private IList<IContent> GetContentFromChildren(UIElementCollection children)
        {
            IList<IContent> contents = new List<IContent>();
            AddTextBoxIfNoContent();
            foreach (var control in children)
            {
                IContent cont = null;
                if (control is Image)
                {
                    cont = new PhotoContent() { Content = ImageToBytes(control as Image), ImageTitle = (control as Image).Source.GetHashCode().ToString() };
                }
                else if (control is TextBox)
                {
                    cont = new TextContent() { Content = (control as TextBox).Text };
                }
                contents.Add(cont);
            }
            return contents;
        }

        private void ListBoxItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (new AddEditINoteWindow(currentBook, ListBoxItems.SelectedItem as Note) { Title = "Editing note" }).Show();
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

        private void HideNotePanel()
        {
            StackPanelItemPanel.Visibility = Visibility.Collapsed;
            StackPanelContent.Children.Clear();
            StackPanelItemPanel.DataContext = null;
        }

        #region Content
        private void InputContentsToStackPanel(Note note)
        {
            foreach (var content in note.Contents)
            {
                StackPanelContent.Children.Add(content is TextContent ?
                    new TextBox() { Text = (content as TextContent).Content as string, BorderBrush = Brushes.White, MinLines = 5 }
                    : (UIElement)new Image() { Source = BytesToImage((content as PhotoContent).BytesOfPhoto) });
            }
        }
        private void RemoveEmptyContents()
        {
            for (int i = 0; i < StackPanelContent.Children.Count; i++)
            {
                if ((StackPanelContent.Children[i] as TextBox)?.Text.Length < 1)
                {
                    StackPanelContent.Children.RemoveAt(i);
                }
            }
        }
        private void StackPanelContent_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var lol = e.Source is TextBox ? (e.Source as TextBox).Text : (e.Source as Image).Source.ToString();
            var result = MessageBox.Show("Delete content " + lol + "?", "Remove content", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                StackPanelContent.Children.Remove(e.Source as UIElement);
                AddTextBoxIfNoContent();
            }
            UpdateNoteData();
        }
        private void AddTextBoxIfNoContent()
        {
            if (!IsContentsHasTextBox())
            {
                StackPanelContent.Children.Add(new TextBox() { BorderBrush = Brushes.White, MinLines = 5 });
            }
        }
        private bool IsContentsHasTextBox()
        {
            foreach (var control in StackPanelContent.Children)
            {
                if (control is TextBox)
                    return true;
            }
            return false;
        }
        private void BtnAttachImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                var extension = System.IO.Path.GetExtension(dialog.FileName);

                if (IsImageExtension(extension))
                {
                    Image myImage = new Image();
                    byte[] imageByte = File.ReadAllBytes(dialog.FileName);
                    myImage.Source = BytesToImage(imageByte);
                    string name = System.IO.Path.GetFileName(dialog.FileName);
                    RemoveEmptyContents();

                    StackPanelContent.Children.Add(myImage);

                    (StackPanelItemPanel.DataContext as Note).Contents
                        .Add(new PhotoContent()
                        { Content = imageByte, ImageTitle = name }
                        );
                    StackPanelContent.Children.Add(new TextBox() { BorderBrush = Brushes.White, MinLines = 5 });
                }
                else
                {
                    MessageBox.Show("FALSE");
                }
            }
            UpdateNoteData();
        }
        #region Image/byte
        private static bool IsImageExtension(string extension)
        {
            return extension.Equals(".jpg") || extension.Equals(".png");
        }
        private static BitmapImage BytesToImage(byte[] bytes)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = new MemoryStream(bytes);
            bitmapImage.EndInit();
            return bitmapImage;
        }
        private static byte[] ImageToBytes(Image image)
        {
            if (image.Source is BitmapSource bitmapSource)
            {
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    return stream.ToArray();
                }
            }
            return null;
        }



        #endregion

        #endregion

        #endregion        
    }
}
