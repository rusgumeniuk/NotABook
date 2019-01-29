using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.BookElements.Contents;
using NotABookLibraryStandart.Models.Roles;

using System.Collections.Generic;

namespace NotABookLibraryStandart.DB
{
    public interface IService : IAuthenticationService
    {
        IList<User> FindUsers();
        IList<Book> FindBooks();
        IList<Note> FindNotes();
        IList<Category> FindCategories();
        IList<LinkNoteCategory> FindLinksNoteCategory();
        IList<Content> FindContents();

        IList<User> FindAdmins();
        IList<Book> FindBooksByUser(User user);
        IList<Note> FindNotesByBook(Book book);
        IList<Category> FindCategoriesByUser(User user);
        IList<LinkNoteCategory> FindLinksNoteCategory(Note note);
        IList<LinkNoteCategory> FindLinksNoteCategory(Category category);
        IList<Content> FindContentsByNote(Note note);

        void AddBook(Book book);
        void AddNote(Note note);
        void AddCategory(Category category);
        void AddLinkNoteCategory(LinkNoteCategory linkNoteCategory);
        void AddLinkNoteCategory(Note note, Category category);
        void AddContent(Content content);

        void RemoveBook(Book book);
        void RemoveNote(Note note);
        void RemoveCategory(Category category);
        void RemoveLinkNoteCategory(LinkNoteCategory linkNoteCategory);
        void RemoveContent(Content content);

        int SaveChanges();
    }
}
