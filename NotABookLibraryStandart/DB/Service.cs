using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.BookElements.Contents;
using NotABookLibraryStandart.Models.Roles;

using System;
using System.Collections.Generic;

namespace NotABookLibraryStandart.DB
{
    public class Service : IService
    {
        private readonly IRepository _repository;
        public Service(IRepository repository)
        {
            this._repository = repository;
        }

        #region Find
        public IList<User> FindAdmins()
        {
            return _repository.GetAdmins() as IList<User>;
        }
        public IList<Book> FindBooks()
        {            
            return _repository.GetBooks() as IList<Book>;
        }
        public IList<Book> FindBooksByUser(User user)
        {
            return _repository.GetBooksByUser(user) as IList<Book>;
        }
        public IList<Category> FindCategories()
        {
            return _repository.GetCategories() as IList<Category>;
        }
        public IList<Category> FindCategoriesByUser(User user)
        {
            return _repository.GetCategoriesByUser(user) as IList<Category>;
        }
        public IList<Content> FindContents()
        {
            return _repository.GetContents() as IList<Content>;
        }
        public IList<Content> FindContentsByNote(Note note)
        {
            return _repository.GetContentsByNote(note) as IList<Content>;
        }
        public IList<LinkNoteCategory> FindLinksNoteCategory()
        {
            return _repository.GetLinksNoteCategory() as IList<LinkNoteCategory>;
        }
        public IList<LinkNoteCategory> FindLinksNoteCategory(Category category)
        {
            return _repository.GetLinksNoteCategory(category) as IList<LinkNoteCategory>;
        }
        public IList<LinkNoteCategory> FindLinksNoteCategory(Note note)
        {
            return _repository.GetLinksNoteCategory(note) as IList<LinkNoteCategory>;
        }
        public IList<Note> FindNotes()
        {
            return _repository.GetNotes() as IList<Note>;
        }
        public IList<Note> FindNotesByBook(Book book)
        {
            return _repository.GetNotesByBook(book) as IList<Note>;
        }
        public IList<User> FindUsers()
        {
            return _repository.GetUsers() as IList<User>;
        }
        public IList<Category> FindCategoriesByNote(Note note)
        {
            return _repository.GetCategoriesByNote(note) as IList<Category>;
        }
        #endregion

        #region Add
        public void AddBook(Book book)
        {
            _repository.Add(book);
        }
        public void AddCategory(Category category)
        {
            _repository.Add(category);
        }
        public void AddContent(Content content)
        {
            _repository.Add(content);
        }
        public void AddLinkNoteCategory(LinkNoteCategory linkNoteCategory)
        {
            _repository.Add(linkNoteCategory);
        }
        public void AddLinkNoteCategory(Note note, Category category)
        {
            _repository.Add(note, category);
        }
        public void AddNote(Note note)
        {
            _repository.Add(note);
        }

        #endregion

        #region Remove
        public void RemoveBook(Book book)
        {
            _repository.Remove(book);
        }
        public void RemoveCategory(Category category)
        {
            _repository.Remove(category);
        }
        public void RemoveContent(Content content)
        {
            _repository.Remove(content);
        }
        public void RemoveLinkNoteCategory(LinkNoteCategory linkNoteCategory)
        {
            _repository.Remove(linkNoteCategory);
        }
        public void RemoveNote(Note note)
        {
            _repository.Remove(note);
        }
        #endregion

        #region Authentifiaction service
        public void AddUser(User user)
        {
            _repository.Add(user);
        }
        public bool IsExistUser(string username)
        {
            return _repository.IsExistUser(username);
        }
        public User GetUser(string username)
        {
            return _repository.GetUser(username);
        }
        public User GetUser(string username, string password)
        {
            return _repository.GetUser(username, User.CalculateHash(password, username))
                ?? throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");
        }
        public void RemoveUser(User user)
        {
            _repository.Remove(user);
        }
        #endregion

        public int SaveChanges()
        {
            return _repository.Save();
        }        
    }
}
