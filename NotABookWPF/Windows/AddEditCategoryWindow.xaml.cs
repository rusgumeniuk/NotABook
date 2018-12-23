using NotABookLibraryStandart.Models;
using NotABookLibraryStandart.Models.BookElements;
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
        DataContext db;        
        Category Category = null;
        public AddEditCategoryWindow(DataContext dataContext)
        {            
            InitializeComponent();
            DataContext = Category;            
            db = dataContext;
        }
        public AddEditCategoryWindow(DataContext dataContext, Category category)
        {
            InitializeComponent();
            Category = category;
            DataContext = Category;            
            db = dataContext;
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
                    db.Categories.Add(new Category(CategoryTitleTextBox.Text));                    
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
