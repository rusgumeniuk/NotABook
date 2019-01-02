﻿using Microsoft.Win32;

using NotABookLibraryStandart.Models;
using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.BookElements.Contents;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NotABookWPF.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Init
        readonly DataContext db;

        #region Lists etc
        public ObservableCollection<Book> Books;
        public Book currentBook;
        public ObservableCollection<Note> Notes;
        public ObservableCollection<Category> CategoriesList;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            db = new DataContext();

            InitLoadFromDb();

            Books = db.Books.Local;
            currentBook = Books.Count > 0 ? Books[0] : null;

            UpdateDataFromDb();

            UpdateCurrentBook();
        }
        #endregion
        private void InitLoadFromDb()
        {
            db.Books.ToArray();
            db.Categories.ToArray();
            db.Notes.ToArray();
            db.Contents.ToArray();
            var res = db.LinkNoteCategories.ToArray();
        }
        private void UpdateDataFromDb()
        {
            Books = db.Books.Local;
            Notes = currentBook.Notes;
            CategoriesList = db.Categories.Local;
        }
        private void UpdateCurrentBook(Note note = null)
        {
            UpdateDataFromDb();
            ListViewBooks.ItemsSource = Books;
            ComboBoxCurrentBook.ItemsSource = Books;
            ListBoxItems.ItemsSource = Notes;
            //CategoryInNoteListBox.ItemsSource = note == null ? null : db.LinkNoteCategories.Where(nt => nt.Id.Equals(note.Id));
            AllCategoriesListBox.ItemsSource = CategoriesList;

            ComboBoxCurrentBook.SelectedItem = currentBook;
            ListViewBooks.SelectedItem = currentBook;

            TextBlockCountOfItems.Text = currentBook?.Notes.Count.ToString() + " ";
            TBCurrentBook.Text = currentBook?.Title ?? "Undefind";
            HideNotePanel();
        }

        #region Menu panel
        private void MenuItemCreateNote_Click(object sender, RoutedEventArgs e)
        {
            (new AddEditINoteWindow(currentBook, db) { Title = "Creating of note" }).Show();
        }
        private void MenuItemCreateBook_Click(object sender, RoutedEventArgs e)
        {
            (new AddEditBookElement(db, new Book(String.Empty))).Show();
        }
        private void MenuItemCreateCategory_Click(object sender, RoutedEventArgs e)
        {
            (new AddEditBookElement(db, new Category(String.Empty))).Show();
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
            (new AddEditINoteWindow(currentBook, db) { Title = "Creating of item" }).Show();
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
            new AddEditBookElement(db, ListViewBooks.SelectedItem as Book).Show();            
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
            db.Books.Remove(ListViewBooks.SelectedItem as Book);
            db.SaveChanges();
        }
        private void MenuItemEditBook_Click(object sender, RoutedEventArgs e)
        {
            new AddEditBookElement(db, ((sender as MenuItem).CommandParameter as Book)).Show();            
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

        private void TextBoxFindItem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxFindItem.IsFocused)
            {
                HideNotePanel();
                if (String.IsNullOrWhiteSpace(TextBoxFindItem.Text))
                    ListBoxItems.ItemsSource = currentBook.Notes;
                else
                {
                    IList<Note> result = currentBook?.FindNotes(TextBoxFindItem.Text, db.LinkNoteCategories.Local);
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
                SaveNoteData();
                HideNotePanel();
                StackPanelItemPanel.DataContext = ListBoxItems.SelectedItem as Note;

                InputContentsToStackPanel(ListBoxItems.SelectedItem as Note);
                AddTextBoxIfNoContent();
                StackPanelItemPanel.Visibility = Visibility.Visible;
                CategoryInNoteListBox.ItemsSource = db.LinkNoteCategories.Local.Where(conn => conn.Note.Id.Equals((ListBoxItems.SelectedItem as Note).Id)).Select(conn => conn.Category);
            }
        }

        private void SaveNoteData()
        {
            if (StackPanelContent.DataContext != null && CategoryInNoteListBox.ItemsSource != null)
            {
                Note note = StackPanelContent.DataContext as Note;
                note.Title = TBEditItemTitle.Text;

                IList<Content> contents = GetContentFromChildren(StackPanelContent.Children);
                IList<Content> addContent = new List<Content>();
                bool hasDiffences = false;
                foreach (var newContent in contents)
                {
                    bool exist = false;
                    foreach (var existingContent in note.NoteContents)
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
                        hasDiffences = true;
                        addContent.Add(newContent);
                    }
                }
                if (hasDiffences)
                {
                    foreach (var content in note.NoteContents)
                    {
                        db.Contents.Remove(content);
                    }
                    note.NoteContents.Clear();
                    note.NoteContents = addContent;
                    db.SaveChanges();
                }
                DataContext = note;
            }
        }
        private IList<Content> GetContentFromChildren(UIElementCollection children)
        {
            IList<Content> contents = new List<Content>();
            AddTextBoxIfNoContent();
            foreach (var control in children)
            {
                Content cont = null;
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
            (new AddEditINoteWindow(currentBook, db, ListBoxItems.SelectedItem as Note) { Title = "Editing note" }).Show();
        }
        #endregion
        private void ComboBoxCurrentBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxCurrentBook.SelectedItem != null && ComboBoxCurrentBook.SelectedItem as Book != currentBook)
            {
                currentBook = ComboBoxCurrentBook.SelectedItem as Book;
                UpdateCurrentBook();
            }
        }
        #endregion

        #region Note stack panel

        #region categories lists panel              
        private void CategoryInNoteListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CategoryInNoteListBox.SelectedItem != null)
            {
                db.LinkNoteCategories.Local.Remove(
                    db.LinkNoteCategories.Local
                    .First(
                        link => link.Note.Id.Equals((ListBoxItems.SelectedItem as Note).Id)
                        && link.Category.Id.Equals((CategoryInNoteListBox.SelectedItem as Category).Id))
               );
                db.SaveChanges();
                CategoryInNoteListBox.ItemsSource = db.LinkNoteCategories.Local.Where(conn => conn.Note.Id.Equals((ListBoxItems.SelectedItem as Note).Id)).Select(conn => conn.Category);
            }
        }
        private void AllCategoriesListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (AllCategoriesListBox.SelectedItem != null)
            {
                if (CategoryInNoteListBox.Items.Contains(AllCategoriesListBox.SelectedItem))
                    MessageBox.Show("Note already marked by this category!");
                else
                {
                    db.LinkNoteCategories.Local.Add(new LinkNoteCategory(ListBoxItems.SelectedItem as Note, AllCategoriesListBox.SelectedItem as Category));
                    CategoryInNoteListBox.ItemsSource = db.LinkNoteCategories.Local.Where(conn => conn.Note.Id.Equals((ListBoxItems.SelectedItem as Note).Id)).Select(conn => conn.Category);
                    db.SaveChanges();
                }
            }
        }
        private void BtnCreateCategory_Click(object sender, RoutedEventArgs e)
        {
            new AddEditBookElement(db, new Category(String.Empty)).Show();            
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
            foreach (var content in note.NoteContents)
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
            SaveNoteData();
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

                    (StackPanelItemPanel.DataContext as Note).NoteContents
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
            SaveNoteData();
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (StackPanelItemPanel.DataContext != null)
            {
                SaveNoteData();
            }
        }

        private void MenuItemInNotesRemoveNote_Click(object sender, RoutedEventArgs e)
        {
            db.Notes.Remove(ListBoxItems.SelectedItem as Note);
            db.SaveChanges();
        }
    }
}