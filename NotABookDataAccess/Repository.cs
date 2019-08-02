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
        }

        public void Add(User user)
        {
            db.Users.Local.Add(user);
            Save();
        }
        public void Add(User user, Book book)
        {
            db.Users.Local.FirstOrDefault(us => us.Equals(user)).Books.Add(book);
            Save();
        }
        public void Add(User user, Book book, Note note)
        {
            db.Users.Local.FirstOrDefault(us => us.Equals(user)).Books.FirstOrDefault(bk => bk.Equals(book)).Notes.Add(note);
            Save();
        }
        public void Add(User user, Category category)
        {
            db.Users.Local.FirstOrDefault(us => us.Equals(user)).Categories.Add(category);
            Save();
        }
        public void Add(User user, LinkNoteCategory linkNoteCategory)
        {
            db.Users.Local.FirstOrDefault(us => us.Equals(user)).LinkNoteCategories.Add(linkNoteCategory);
            Save();
        }
        public void Add(User user, Note note, Category category)
        {
            db.Users.Local.FirstOrDefault(us => us.Equals(user)).LinkNoteCategories.Add(new LinkNoteCategory(note, category));
            Save();
        }
        public void Add(User user, Note note, Content content)
        {
            db.Users.Local.FirstOrDefault(us => us.Equals(user)).Books.FirstOrDefault(book => book.Notes.Contains(note)).Notes.First(nt => nt.Equals(note)).AddContent(content);
            Save();
        }

        public IEnumerable<User> GetUsers()
        {
            return db.Users.Local;
        }       
        public IEnumerable<User> GetAdmins()
        {
            return db.Users.Local.Where(user => user.Roles.Contains("Administators"));
        }
        public IEnumerable<Book> GetBooks(User user)
        {
            return db.Users.Local.FirstOrDefault(us => us.Equals(user)).Books;
        }        
        public IEnumerable<Note> GetNotes(User user)
        {
            return db.Users.Local.FirstOrDefault(us => us.Equals(user)).GetAllNotes;
        }
        public IEnumerable<Category> GetCategories(User user)
        {
            return db.Users.Local.FirstOrDefault(us => us.Equals(user)).Categories;
        }
        public IEnumerable<LinkNoteCategory> GetLinksNoteCategory(User user)
        {
            return db.Users.Local.FirstOrDefault(us => us.Equals(user)).LinkNoteCategories;
        }
        public IEnumerable<Note> GetNotesByBook(User user, Book book)
        {
            return db.Users.Local.FirstOrDefault(us => us.Equals(user)).Books.FirstOrDefault(bk => bk.Equals(book)).Notes;
        }
        public IEnumerable<LinkNoteCategory> GetLinksNoteCategory(User user, Book book)
        {
            return db.Users.Local.FirstOrDefault(us => us.Equals(user)).LinkNoteCategories.Where(link => book.Notes.Contains(link.Note));
        }
        public IEnumerable<LinkNoteCategory> GetLinksNoteCategory(User user, Note note)
        {
            return db.Users.Local.FirstOrDefault(us => us.Equals(user)).LinkNoteCategories.Where(link => link.Note.Equals(note));
        }
        public IEnumerable<LinkNoteCategory> GetLinksNoteCategory(User user, Category category)
        {
            return db.Users.Local.FirstOrDefault(us => us.Equals(user)).LinkNoteCategories.Where(link => link.Category.Equals(category));
        }
        public IEnumerable<Content> GetContentsByNote(User user, Note note)
        {
            return db.Users.Local.FirstOrDefault(us => us.Equals(user)).Books.First(bk => bk.Notes.Contains(note)).Notes.First(nt => nt.Equals(note)).NoteContents;
        }
        public IEnumerable<Category> GetCategoriesByNote(User user, Note note)
        {
            return db.Users.Local.FirstOrDefault(us => us.Equals(user)).LinkNoteCategories.Where(link => link.Note.Id == note.Id).Select(link => link.Category);
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
        public void Remove(User user, Book book)
        {
            db.Users.Local.FirstOrDefault(us => us.Equals(user)).Books.Remove(book);
            Save();
        }
        public void Remove(User user, Note note)
        {
            db.Users.Local.FirstOrDefault(us => us.Equals(user)).Books.First(bk => bk.Notes.Contains(note)).Notes.Remove(note);
            Save();
        }
        public void Remove(User user, Category category)
        {
            db.Users.Local.FirstOrDefault(us => us.Equals(user)).Categories.Remove(category);
            Save();
        }
        public void Remove(User user, LinkNoteCategory linkNoteCategory)
        {
            db.Users.Local.FirstOrDefault(us => us.Equals(user)).LinkNoteCategories.Remove(linkNoteCategory);
            Save();
        }
        public void Remove(User user, Note note, Content content)
        {
            db.Users.Local.FirstOrDefault(us => us.Equals(user)).Books.First(bk => bk.Notes.Contains(note)).Notes.First(nt => nt.Equals(note)).RemoveContent(content);
            Save();
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
