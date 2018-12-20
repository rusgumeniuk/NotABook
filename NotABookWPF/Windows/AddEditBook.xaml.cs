using NotABookLibraryStandart.Models;
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

namespace NotABookWPF.Windows
{
    /// <summary>
    /// Interaction logic for AddEditBook.xaml
    /// </summary>
    public partial class AddEditBook : Window
    {
        Book Book = null;
        public AddEditBook(Book book = null)
        {
            InitializeComponent();
            Book = book;
            DataContext = Book;
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
                    Book.Books.Add(new Book(BookTitleTextBox.Text));
                }
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
