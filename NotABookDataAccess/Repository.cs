using NotABookLibraryStandart.DB;
using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.BookElements.Contents;
using NotABookLibraryStandart.Models.Roles;

using System.Collections.Generic;
using System.Linq;

namespace NotABookDataAccess
{
    public class Repository : IRepository
    {
        private readonly DataBaseContext db;
        public Repository(DataBaseContext database)
        {
            this.db = database;
            InitDB();
        }
        public void InitDB()
        {
            db.Users.ToArray();
            db.Books.ToArray();
            db.Notes.ToArray();
            db.Categories.ToArray();
            db.LinkNoteCategories.ToArray();
            db.Contents.ToArray();
        }

        public void Add(User user)
        {
            db.Users.Local.Add(user);
            Save();
        }
        public void Add(Book book)
        {
            db.Books.Local.Add(book);
            Save();
        }
        public void Add(Note note)
        {
            db.Notes.Local.Add(note);
            Save();
        }
        public void Add(Category category)
        {
            db.Categories.Local.Add(category);
            Save();
        }
        public void Add(LinkNoteCategory linkNoteCategory)
        {
            db.LinkNoteCategories.Local.Add(linkNoteCategory);
            Save();
        }
        public void Add(Note note, Category category)
        {
            db.LinkNoteCategories.Local.Add(new LinkNoteCategory(note, category));
            Save();
        }
        public void Add(Content content)
        {
            db.Contents.Add(content);
            Save();
        }

        public IEnumerable<User> GetAdmins()
        {
            return db.Users.Local.Where(user => user.Roles.Contains("Administators"));
        }
        public IEnumerable<Book> GetBooks()
        {
            return db.Books.Local;
        }
        public IEnumerable<Book> GetBooksByUser(User user)
        {
            return db.Users.Local.FirstOrDefault(us => us.Equals(user))?.Books;
        }
        public IEnumerable<Category> GetCategories()
        {
            return db.Categories.Local;
        }
        public IEnumerable<Category> GetCategoriesByUser(User user)
        {
            return db.Users.Local.FirstOrDefault(us => us.Equals(user))?.Categories;
        }
        public IEnumerable<Content> GetContents()
        {
            return db.Contents;
        }
        public IEnumerable<Content> GetContentsByNote(Note note)
        {
            return db.Notes.Local.FirstOrDefault(nt => nt.Equals(note))?.NoteContents;
        }
        public IEnumerable<LinkNoteCategory> GetLinksNoteCategory()
        {
            return db.LinkNoteCategories.Local;
        }
        public IEnumerable<LinkNoteCategory> GetLinkNoteCategory(Book book)
        {
            return db.LinkNoteCategories.Local.Where(link => book.Notes.Contains(link.Note));
        }
        public IEnumerable<LinkNoteCategory> GetLinksNoteCategory(Note note)
        {
            return db.LinkNoteCategories.Local.Where(link => link.Note.Equals(note));
        }
        public IEnumerable<LinkNoteCategory> GetLinksNoteCategory(Category category)
        {
            return db.LinkNoteCategories.Local.Where(link => link.Category.Equals(category));
        }
        public IEnumerable<Note> GetNotes()
        {
            return db.Notes.Local;
        }
        public IEnumerable<Note> GetNotesByBook(Book book)
        {
            return db.Books.Local.FirstOrDefault(bk => bk.Equals(book))?.Notes;
        }
        public IEnumerable<User> GetUsers()
        {
            return db.Users.Local;
        }
        public IEnumerable<Category> GetCategoriesByNote(Note note)
        {
            return db.LinkNoteCategories.Local.Where(link => link.Note.Id == note.Id).Select(conn => conn.Category);
        }

        public User GetUser(string username)
        {
            return db.Users.Local.FirstOrDefault(user => user.Username.Equals(username));
        }
        public User GetUser(string username, string password)
        {
            return db.Users.Local.FirstOrDefault(user => user.Username.Equals(username) && user.HashedPassword.Equals(password));
        }

        public bool IsExistUser(string username)
        {
            return GetUser(username) != null;
        }

        public void Remove(User user)
        {
            db.Users.Local.Remove(user);
            Save();
        }
        public void Remove(Book book)
        {
            db.Books.Local.Remove(book);
            Save();
        }
        public void Remove(Note note)
        {
            db.Notes.Local.Remove(note);
            Save();
        }
        public void Remove(Category category)
        {
            db.Categories.Local.Remove(category);
            Save();
        }
        public void Remove(LinkNoteCategory linkNoteCategory)
        {
            db.LinkNoteCategories.Local.Remove(linkNoteCategory);
            Save();
        }
        public void Remove(Content content)
        {
            db.Contents.Remove(content);
            Save();
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
