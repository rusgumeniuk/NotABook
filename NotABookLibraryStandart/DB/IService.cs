using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.BookElements.Contents;
using NotABookLibraryStandart.Models.Roles;

using System.Collections.Generic;

namespace NotABookLibraryStandart.DB
{
    public interface IService : IAuthenticationService
    {
        IList<User> FindUsers();
        IList<Book> FindBooks(User user);
        IList<Note> FindNotes(User user);
        IList<Category> FindCategories(User user);
        IList<LinkNoteCategory> FindLinksNoteCategory(User user);        

        IList<User> FindAdmins();        
        IList<Note> FindNotesByBook(User user, Book book);        
        IList<LinkNoteCategory> FindLinksNoteCategory(User user, Book book);
        IList<LinkNoteCategory> FindLinksNoteCategory(User user, Note note);
        IList<LinkNoteCategory> FindLinksNoteCategory(User user, Category category);
        IList<Note> FindAllNotesByWord(User user, string text);
        IList<Content> FindContentsByNote(User user, Note note);
        IList<Category> FindCategoriesByNote(User user, Note note);

        void AddBook(User user, Book book);
        void AddNote(User user, Book book, Note note);
        void AddCategory(User user, Category category);
        void AddLinkNoteCategory(User user, LinkNoteCategory linkNoteCategory);
        void AddLinkNoteCategory(User user, Note note, Category category);
        void AddContent(User user, Note note, Content content);

        void RemoveBook(User user, Book book);
        void RemoveNote(User user, Note note);
        void RemoveCategory(User user, Category category);
        void RemoveLinkNoteCategory(User user, LinkNoteCategory linkNoteCategory);
        void RemoveContent(User user, Note note, Content content);

        int SaveChanges();
    }
}
