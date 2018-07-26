using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotABook.Models;
using System.Collections.ObjectModel;

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
                StartFunction();
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n\n");
                Console.WriteLine(ex.StackTrace);
            }

            

            Console.ReadKey();
        }

        private static void StartFunction()
        {
            CurrentBook = new Book("CurBook");

            FirstCategory = new Category(CurrentBook, "Sweet");
            SecondCategory = new Category(CurrentBook, "Salt");

            FirstItem = new Item(CurrentBook, "1 item", "desc1", new ObservableCollection<Category>() { FirstCategory });
            SecondItem = new Item(CurrentBook, "2 item", "descript 2", new ObservableCollection<Category>() { SecondCategory, FirstCategory });
        }
    }
}
