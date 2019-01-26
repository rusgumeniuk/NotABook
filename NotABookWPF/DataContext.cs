using NotABookLibraryStandart.Models;
using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.BookElements.Contents;
using NotABookLibraryStandart.Models.Roles;
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
        public DbSet<LinkNoteCategory> LinkNoteCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public IList<Content> Contents
        {
            get
            {
                List<Content> result = new List<Content>(TextContents);
                result.AddRange(PhotoContents);
                return result;
            }          
        }
    }
}
