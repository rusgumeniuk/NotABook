using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Microsoft.Win32;
using NotABookLibraryStandart.Models;
using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.BookElements.Contents;

namespace NotABookWPF.Windows
{
    /// <summary>
    /// Interaction logic for AddEditItemWindow.xaml
    /// </summary>
    public partial class AddEditINoteWindow : Window
    {
        #region Init
        DataContext db;
        readonly Book currentBook;
        bool creating = true;

        public AddEditINoteWindow(Book curBook, DataContext dataContext, Note note = null)
        {
            InitializeComponent();
            db = dataContext;
            currentBook = curBook;
            AllCategoriesListBox.ItemsSource = dataContext.Categories.Local;
            Note newNote = note ?? new Note();
            creating = note == null;
            this.DataContext = newNote;
            this.Title = newNote?.Title ?? String.Empty;
            CategoryInNoteListBox.ItemsSource = db.LinkNoteCategories.Local.Where(link => link.Note.Id.Equals(note?.Id))?.Select(conn => conn.Category);
            InputContentsToStackPanel(newNote);
            AddTextBoxIfNoContent();
        }
        #endregion

        private void InputContentsToStackPanel(Note note)
        {
            foreach (var content in note.NoteContents)
            {
                StackPanelContent.Children.Add(content is TextContent ?
                    new TextBox() { Text = (content as TextContent).Content as string, BorderBrush = Brushes.White, MinLines = 5 }
                    : (UIElement)new Image() { Source = BytesToImage((content as PhotoContent).BytesOfPhoto) });
            }
        }

        #region LostFocuc, close Window, Update infp
        private void TBEditItemTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            (this.DataContext as Note).Title = TBEditItemTitle.Text;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext as Note != null)
            {
                Note note = DataContext as Note;
                RemoveEmptyContents();
                SaveNoteData();
                if (String.IsNullOrWhiteSpace(note.Title) && note.IsHasNotContent)
                {
                    return;
                }
                else if (!String.IsNullOrWhiteSpace(note.Title))
                {
                    note.Title = note.Title;
                }
                else
                {
                    note.Title = note.TitleFromContent;
                }
                if (creating)
                    currentBook.Notes.Add(note);
                db.SaveChanges();
            }
        }

        private void SaveNoteData()
        {
            Note note = DataContext as Note;
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
            if (hasDiffences || !contents.Count.Equals(note.NoteContents))
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
        #endregion
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

                    (this.DataContext as Note).NoteContents
                        .Add(new PhotoContent()
                        { Content = ImageToBytes(myImage), ImageTitle = name }
                        );
                    StackPanelContent.Children.Add(new TextBox() { BorderBrush = Brushes.White, MinLines = 5 });
                }
                else
                {
                    MessageBox.Show("FALSE");
                }
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

        private void StackPanelContent_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var lol = e.Source is TextBox ? (e.Source as TextBox).Text : (e.Source as Image).Source.ToString();
            var result = MessageBox.Show("Delete content " + lol + "?", "Remove content", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                StackPanelContent.Children.Remove(e.Source as UIElement);
                AddTextBoxIfNoContent();
            }

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

        private void BtnCreateCategory_Click(object sender, RoutedEventArgs e)
        {
            (new AddEditCategoryWindow(db) { Title = "Create category" }).Show();
        }

        private void CategoryInNoteListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CategoryInNoteListBox.SelectedItem != null)
            {
                db.LinkNoteCategories.Local.Remove(
                    db.LinkNoteCategories.Local
                    .First(
                        link => link.Note.Id.Equals((DataContext as Note).Id)
                        && link.Category.Id.Equals((CategoryInNoteListBox.SelectedItem as Category).Id))
               );
                CategoryInNoteListBox.ItemsSource = db.LinkNoteCategories.Local.Where(conn => conn.Note.Id.Equals((DataContext as Note).Id)).Select(conn => conn.Category);
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
                    db.LinkNoteCategories.Local.Add(new LinkNoteCategory(DataContext as Note, AllCategoriesListBox.SelectedItem as Category));
                    CategoryInNoteListBox.ItemsSource = db.LinkNoteCategories.Local.Where(conn => conn.Note.Id.Equals((DataContext as Note).Id)).Select(conn => conn.Category);
                }
            }
        }
    }
}
