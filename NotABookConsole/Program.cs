using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using NotABookLibraryStandart.Models;

namespace NotABookConsole
{
    class Program
    {
        static Book CurrentBook = null;
        static Item FirstItem = null;
        static Item SecondItem = null;
        static Category FirstCategory = null;
        static Category SecondCategory = null;


        static void Main(string[] args)
        {
            StartFunction();
            try
            {
                Category category = new Category(CurrentBook);
                
                string text = category.GetStringCountOfItemsWithCategory;
                Console.WriteLine(text);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n\n");
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine("end");

            Console.ReadKey();
        }

        private static void StartFunction()
        {
            CurrentBook = new Book("CurBook");

            FirstCategory = new Category(CurrentBook, "Sweet");
            SecondCategory = new Category(CurrentBook, "Salt");

            FirstItem = new Item(CurrentBook, "1 item", Description.CreateDescription("desc1"), new ObservableCollection<Category>() { FirstCategory });
            SecondItem = new Item(CurrentBook, "2 item", Description.CreateDescription("descript 2"), new ObservableCollection<Category>() { SecondCategory, FirstCategory });
        }
    }
}
