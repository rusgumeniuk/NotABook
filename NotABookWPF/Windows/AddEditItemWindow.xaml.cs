using System;
using System.Collections.Generic;
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
    public partial class AddEditItemWindow : Window
    {
        readonly Book currentBook;
        public AddEditItemWindow(Book currentBook)
        {
            InitializeComponent();
            this.currentBook = currentBook;
            Note newNote = new Note(MainWindow.currentBook);
            currentBook.Notes.Add(newNote);

            this.DataContext = newNote;
        }
        public AddEditItemWindow(Book curBook, Note note)
        {
            InitializeComponent();
            currentBook = curBook;
            this.DataContext = note;
            this.Title = note.Title;
            foreach (var content in note.Contents)
            {
                StackPanelContent.Children.Add(content is TextContent ?
                    new TextBox() { Text = (content as TextContent).Content as string }
                    : (UIElement)new Image() { Source = BytesToImage((content as PhotoContent).BytesOfPhoto) });
            }
        }

        private void TBDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            //(DataContext as Note).Contents.Clear();
            //foreach (var control in StackPanelContent.Children)
            //{
            //    (DataContext as Note).AddContent(control as IContent);
            //}
        }
        private void TBEditItemTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            (this.DataContext as Note).Title = TBEditItemTitle.Text;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext != null)
            {
                UpdateInfo();
                Note note = DataContext as Note;
                if (String.IsNullOrWhiteSpace(note.Title) && note.IsHasNotContent)
                {
                    MessageBox.Show("Ooops, item should be non-empty", "Empty item");
                    currentBook.RemoveBookElement(note);
                }
                else if (String.IsNullOrWhiteSpace(note.Title))
                {
                    note.Title = note.TitleFromContent;
                }
            }
        }

        private void UpdateInfo()
        {
            Note note = DataContext as Note;
            note.Title = TBEditItemTitle.Text;
            IList<IContent> contents = new List<IContent>();
            
            foreach (var control in StackPanelContent.Children)
            {
                IContent cont = null;
                if (control is Image)
                {
                    cont = new PhotoContent() { Content = ImageToBytes(control as Image) };                   
                    
                }
                else if (control is TextBox)
                {
                    cont = new TextContent() { Content = (control as TextBox).Text };
                }
                contents.Add(cont);
            }
            if (!contents.Equals(note.Contents))
            {                
                note.Contents = contents;
            }
            DataContext = note;
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

                    (this.DataContext as Note).Contents
                        .Add(new PhotoContent()
                        { Content = imageByte, ImageTitle = name }
                        );

                    MessageBox.Show("GOT IT");
                }
                else
                {
                    MessageBox.Show("FALSE");
                }
            }
        }

        private void RemoveEmptyContents()
        {
            //for (byte i = 0; i < StackPanelContent.Children.Count; ++i)
            //{
            //    if((StackPanelContent.Children[i] as IContent).IsEmptyContent())
            //        StackPanelContent.Children.Remo
            //}
            foreach (var control in StackPanelContent.Children)
            {
               if((control as TextBox)?.Text.Length < 1)
                {
                    StackPanelContent.Children.Remove(control as UIElement);
                }
            }
        }

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
    }
}
