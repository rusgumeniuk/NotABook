using NotABookLibraryStandart.Models;
using NotABookLibraryStandart.Models.BookElements;

using System;
using System.Windows;

namespace NotABookWPF.Windows
{
    /// <summary>
    /// Interaction logic for AddEditBookElement.xaml
    /// </summary>
    public partial class AddEditBookElement : Window
    {
        readonly bool isCreating;
        readonly DataContext db;
        public AddEditBookElement(DataContext database, BookElement bookElement)
        {
            InitializeComponent();
            db = database;
            DataContext = bookElement;
            isCreating = bookElement.Title == null;
            TitleTextBox.Text = (DataContext as BookElement)?.Title ?? String.Empty;
            Title = !isCreating ? ("Editing '" + bookElement.Title + "'") : ("Adding new " + bookElement?.GetType().Name.ToLowerInvariant());
        }
        private void BtnCancelSave_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnSaveCategory_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TitleTextBox.Text.Trim()))
            {
                if (!isCreating)
                {
                    (DataContext as BookElement).Title = TitleTextBox.Text.Trim();
                }
                else
                {
                    if (DataContext is Category)
                    {
                        db.Categories.Add(new Category(TitleTextBox.Text));
                    }
                    else if (DataContext is Book)
                    {
                        db.Books.Add(new Book(TitleTextBox.Text));
                    }
                }
                db.SaveChanges();
                Close();
            }
            else
                MessageBox.Show("Oooops, title can not be empty!");
        }
    }
}
