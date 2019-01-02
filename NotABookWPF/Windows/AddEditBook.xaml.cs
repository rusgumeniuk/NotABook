using NotABookLibraryStandart.Models.BookElements;

using System;
using System.Windows;

namespace NotABookWPF.Windows
{
    /// <summary>
    /// Interaction logic for AddEditBook.xaml
    /// </summary>
    public partial class AddEditBook : Window
    {
        readonly DataContext db;
        readonly Book Book = null;
        public AddEditBook(DataContext dataContext, Book book = null)
        {
            InitializeComponent();
            Book = book;
            DataContext = Book;
            db = dataContext;
            BookTitleTextBox.Text = Book?.Title ?? String.Empty;

        }
        private void BtnSaveBook_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(BookTitleTextBox.Text))
            {
                if (Book != null)
                {
                    Book.Title = BookTitleTextBox.Text;
                }
                else
                {
                    db.Books.Add(new Book(BookTitleTextBox.Text));
                }
                db.SaveChanges();
                Close();
            }
            else MessageBox.Show("Ooop, empty text box");
        }

        private void BtnCancelSave_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
