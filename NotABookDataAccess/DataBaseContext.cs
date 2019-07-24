using Microsoft.EntityFrameworkCore;
using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.BookElements.Contents;
using NotABookLibraryStandart.Models.Roles;
using System.Collections.Generic;


namespace NotABookDataAccess
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<TextContent> TextContents { get; set; }
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NotABookDB;Trusted_Connection=True;");
        }
    }
}
