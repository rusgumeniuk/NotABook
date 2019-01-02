using NotABookLibraryStandart.Models.BookElements;

using System;
using System.Windows;

namespace NotABookWPF.Windows
{
    /// <summary>
    /// Interaction logic for AddEditCategory.xaml
    /// </summary>
    public partial class AddEditCategoryWindow : Window
    {
        readonly DataContext db;
        readonly Category Category = null;
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
