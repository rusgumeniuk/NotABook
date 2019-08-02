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
            AddAdminIfNoOne();
        }      
        public DbSet<User> Users { get; set; }       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NotABookDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TextContent>()
                .HasBaseType<Content>();
            modelBuilder.Entity<PhotoContent>()
                .HasBaseType<Content>();

            modelBuilder.Entity<User>()
                .HasMany(user => user.Books)
                .WithOne();
            modelBuilder.Entity<User>()
                .HasMany(user => user.Categories)
                .WithOne();
            modelBuilder.Entity<User>()
                .HasMany(user => user.LinkNoteCategories)
                .WithOne();
            modelBuilder.Entity<Book>()
               .HasMany(book => book.Notes)
               .WithOne();
            modelBuilder.Entity<Note>()
                .HasMany(note => note.NoteContents)
                .WithOne();
                      
           
        }
        private void AddAdminIfNoOne()
        {
            if (Users.Local.Count == 0)
            {
                Users.Local.Add(new User("Ruslan", "rus.gumeniuk@gmail.com", "TySiVs1JHrD5R7etJorugFp5HcDMknAbZi1UK0KyPzw=", "Administrators"));
                SaveChanges();
            }
        }
    }
}
