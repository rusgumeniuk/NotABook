using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.BookElements.Contents;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotABookWPF
{
    public class DataContext : DbContext
    {
        public DataContext() : base("NotabookConnection") { }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books{ get; set; }
        public DbSet<TextContent> TextContents{ get; set; }
        public DbSet<PhotoContent> PhotoContents { get; set; }
        public IList<IContent> Contents
        {
            get
            {
                List<IContent> contents = new List<IContent>();
                contents.AddRange(TextContents);
                contents.AddRange(PhotoContents);
                return contents;
            }
        }
    }
}
