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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

using NotABookLibraryStandart.Models;

namespace NotABookWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LeftListView.ItemsSource = new List<int>() { 10, 0, 2 };
            CBStack.ItemsSource = new List<string>() { "first", "second", "third" };
            Book book = new Book("This book");
            ItemsListView.ItemsSource = new ObservableCollection<Item>() { new Item(book, "First"), new Item(book, "Second") };
        }

        private void BtnText_Click(object sender, RoutedEventArgs e)
        {
            //BtnText.Content = ModelsLibrary.Class1.Method();
        }
    }
}
