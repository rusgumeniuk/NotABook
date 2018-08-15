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

using NotABookLibraryStandart.Models;

namespace NotABookWPF.Windows
{
    /// <summary>
    /// Interaction logic for AddEditItemWindow.xaml
    /// </summary>
    public partial class AddEditItemWindow : Window
    {
        public AddEditItemWindow()
        {
            InitializeComponent();
            Item newItem = new Item(MainWindow.currentBook)
            {
                Description = Description.CreateDescription(String.Empty)
            };
            this.DataContext = newItem;
        }
        public AddEditItemWindow(Item item)
        {
            InitializeComponent();
            this.DataContext = item;
            this.Title = item.Title;
        }

        private void TBDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            (this.DataContext as Item).DescriptionText = TBDescription.Text;
        }
        private void TBEditItemTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            (this.DataContext as Item).Title = TBEditItemTitle.Text;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(DataContext != null)
            {
                Item item = DataContext as Item;
                if(item.Title == null || item.Title == String.Empty)
                {
                    MessageBox.Show("Ooops", "wrong title");
                    return;
                }
            }
        }
    }
}
