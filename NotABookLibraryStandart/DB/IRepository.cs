using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.BookElements.Contents;
using NotABookLibraryStandart.Models.Roles;

using System.Collections.Generic;

namespace NotABookLibraryStandart.DB
{
    public interface IRepository
    {
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetAdmins();
        IEnumerable<Book> GetBooks(User user);
        IEnumerable<Note> GetNotes(User user);
        IEnumerable<Category> GetCategories(User user);
        IEnumerable<LinkNoteCategory> GetLinksNoteCategory(User user);                
        IEnumerable<Note> GetNotesByBook(User user, Book book);        
        IEnumerable<LinkNoteCategory> GetLinksNoteCategory(User user, Book book);
        IEnumerable<LinkNoteCategory> GetLinksNoteCategory(User user, Note note);
        IEnumerable<LinkNoteCategory> GetLinksNoteCategory(User user, Category category);
        IEnumerable<Content> GetContentsByNote(User user, Note note);
        IEnumerable<Category> GetCategoriesByNote(User user, Note note);

        void Add(User user);
        void Add(User user, Book book);
        void Add(User user, Book book, Note note);
        void Add(User user, Category category);
        void Add(User user, LinkNoteCategory linkNoteCategory);
        void Add(User user, Note note, Category category);
        void Add(User user, Note note, Content content);

        void Remove(User user);
        void Remove(User user, Book book);
        void Remove(User user, Note note);
        void Remove(User user, Category category);
        void Remove(User user, LinkNoteCategory linkNoteCategory);
        void Remove(User user, Note note, Content content);

        int Save();
        bool IsExistUser(string username);
        User GetUser(string username);
        User GetUser(string username, string password);
    }
}
