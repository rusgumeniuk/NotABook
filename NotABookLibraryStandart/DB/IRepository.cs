using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.BookElements.Contents;
using NotABookLibraryStandart.Models.Roles;

using System.Collections.Generic;

namespace NotABookLibraryStandart.DB
{
    public interface IRepository
    {
        IEnumerable<User> GetUsers();
        IEnumerable<Book> GetBooks();
        IEnumerable<Note> GetNotes();
        IEnumerable<Category> GetCategories();
        IEnumerable<LinkNoteCategory> GetLinksNoteCategory();
        IEnumerable<Content> GetContents();

        IEnumerable<User> GetAdmins();
        IEnumerable<Book> GetBooksByUser(User user);
        IEnumerable<Note> GetNotesByBook(Book book);
        IEnumerable<Category> GetCategoriesByUser(User user);
        IEnumerable<LinkNoteCategory> GetLinksNoteCategory(Book book);
        IEnumerable<LinkNoteCategory> GetLinksNoteCategory(Note note);
        IEnumerable<LinkNoteCategory> GetLinksNoteCategory(Category category);
        IEnumerable<Content> GetContentsByNote(Note note);
        IEnumerable<Category> GetCategoriesByNote(Note note);

        void Add(User user);
        void Add(Book book);
        void Add(Note note);
        void Add(Category category);
        void Add(LinkNoteCategory linkNoteCategory);
        void Add(Note note, Category category);
        void Add(Content content);

        void Remove(User user);
        void Remove(Book book);
        void Remove(Note note);
        void Remove(Category category);
        void Remove(LinkNoteCategory linkNoteCategory);
        void Remove(Content content);

        int Save();
        bool IsExistUser(string username);
        User GetUser(string username);
        User GetUser(string username, string password);
    }
}
