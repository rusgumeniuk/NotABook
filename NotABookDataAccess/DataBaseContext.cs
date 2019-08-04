using Microsoft.EntityFrameworkCore;
using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.BookElements.Contents;
using NotABookLibraryStandart.Models.Roles;


namespace NotABookDataAccess
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
        {
            Database.EnsureCreated();            
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
    }
}
