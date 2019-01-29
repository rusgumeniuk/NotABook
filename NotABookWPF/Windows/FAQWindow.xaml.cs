using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using NotABookViewModels;

namespace NotABookWPF.Windows
{
    /// <summary>
    /// Interaction logic for FAQWindow.xaml
    /// </summary>
    public partial class FAQWindow : Window, IWindow
    {
        readonly ObservableCollection<Node> Nodes;
        private static readonly IDictionary<string, string> bookElementFAQ = new Dictionary<string, string>()
        {
            ["book"] = "hello, from book",
            ["note"] = "this is note, write somtaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\t\t\naaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        };

        private static readonly IDictionary<string, IDictionary<string, string>> dict = new Dictionary<string, IDictionary<string, string>>()
        {
            ["how it works"] = new Dictionary<string, string>() { ["idk"] = "no, i know how" },

            ["Book elements"] = bookElementFAQ
        };

        public ViewModelCustomBase ViewModel
        {
            get { return DataContext as ViewModelCustomBase; }
            set { DataContext = value; }
        }

        public FAQWindow(FAQWindowViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            Nodes = new ObservableCollection<Node>();

            foreach (var item in dict)
            {
                Node parent = new Node()
                {
                    Name = item.Key
                };

                foreach (var childNode in item.Value)
                {
                    Node child = new Node() { Name = childNode.Key, Value = childNode.Value };
                    parent.Nodes.Add(child);
                }
                Nodes.Add(parent);
            }
            FAQTree.ItemsSource = Nodes;

        }

        private void FAQTree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            faqTextBox.Text = (FAQTree.SelectedItem as Node).Value;
        }

        public void ProcessMessage(string message)
        {
            throw new System.NotImplementedException();
        }
    }
    public class Node
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public ObservableCollection<Node> Nodes { get; set; } = new ObservableCollection<Node>();
    }
}
