using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotABook.Models;
namespace NotABookConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Book book = new Book("trt");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            Console.ReadKey();
        }
    }
}
