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
    /// Interaction logic for AddEditCategory.xaml
    /// </summary>
    public partial class AddEditCategoryWindow : Window
    {
        Book Book = null;
        Category Category = null;
        public AddEditCategoryWindow(Book book)
        {
            InitializeComponent();
            DataContext = Category;
            Book = book;
        }
        public AddEditCategoryWindow(Category category, Book book)
        {
            InitializeComponent();
            Category = category;
            DataContext = Category;
            Book = book;
        }

        private void BtnSaveCategory_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(CategoryTitleTextBox.Text))
            {
                if (Category != null)
                {
                    Category.Title = CategoryTitleTextBox.Text;
                }
                else
                {
                    Book.CategoriesOfBook.Add(new Category(Book, CategoryTitleTextBox.Text));
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
