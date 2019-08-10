using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.BookElements.Contents;
using NotABookLibraryStandart.Models.Roles;

using System;
using System.Collections.Generic;
using System.Linq;

namespace NotABookLibraryStandart.DB
{
    public class Service : IService
    {
        private readonly IRepository _repository;
        public Service(IRepository repository)
        {
            this._repository = repository;
            AddAdminIfNoOne();
        }

        #region Find
        public IList<User> FindAdmins()
        {
            return _repository.GetAdmins().ToList();
        }
        public IList<Book> FindBooks(User user)
        {
            return _repository.GetBooks(user).ToList();
        }
        public IList<Category> FindCategories(User user)
        {
            return _repository.GetCategories(user).ToList();
        }
        public IList<Content> FindContentsByNote(User user, Note note)
        {
            return _repository.GetContentsByNote(user, note).ToList();
        }
        public IList<LinkNoteCategory> FindLinksNoteCategory(User user)
        {
            return _repository.GetLinksNoteCategory(user).ToList();
        }
        public IList<LinkNoteCategory> FindLinksNoteCategory(User user, Book book)
        {
            return _repository.GetLinksNoteCategory(user, book).ToList();
        }
        public IList<LinkNoteCategory> FindLinksNoteCategory(User user, Category category)
        {
            return _repository.GetLinksNoteCategory(user, category).ToList();
        }
        public IList<LinkNoteCategory> FindLinksNoteCategory(User user, Note note)
        {
            return _repository.GetLinksNoteCategory(user, note).ToList();
        }
        public IList<Note> FindAllNotesByWord(User user, string text)
        {
            return _repository.GetLinksNoteCategory(user)
                .Where(link => link.Note.IsContainsText(text) || link.Category.IsContainsText(text))
                .Select(link => link.Note)
                .Union(_repository.GetNotes(user).Where(note => note.IsContainsText(text)))
                .ToList();
        }
        public IList<Note> FindNotes(User user)
        {
            return _repository.GetNotes(user).ToList();
        }
        public IList<Note> FindNotesByBook(User user, Book book)
        {
            return _repository.GetNotesByBook(user, book).ToList();
        }
        public IList<User> FindUsers()
        {
            return _repository.GetUsers().ToList();
        }
        public IList<Category> FindCategoriesByNote(User user, Note note)
        {
            return _repository.GetCategoriesByNote(user, note).ToList();
        }
        #endregion

        #region Add
        public void AddBook(User user, Book book)
        {
            _repository.Add(user, book);
        }
        public void AddCategory(User user, Category category)
        {
            _repository.Add(user, category);
        }
        public void AddContent(User user, Note note, Content content)
        {
            _repository.Add(user, note, content);
        }
        public void AddLinkNoteCategory(User user, LinkNoteCategory linkNoteCategory)
        {
            _repository.Add(user, linkNoteCategory);
        }
        public void AddLinkNoteCategory(User user, Note note, Category category)
        {
            _repository.Add(user, note, category);
        }
        public void AddNote(User user, Book book, Note note)
        {
            _repository.Add(user, book, note);
        }

        #endregion

        #region Remove
        public void RemoveBook(User user, Book book)
        {
            _repository.Remove(user, book);
        }
        public void RemoveCategory(User user, Category category)
        {
            _repository.Remove(user, category);
        }
        public void RemoveContent(User user, Note note, Content content)
        {
            _repository.Remove(user, note, content);
        }
        public void RemoveLinkNoteCategory(User user, LinkNoteCategory linkNoteCategory)
        {
            _repository.Remove(user, linkNoteCategory);
        }
        public void RemoveNote(User user, Note note)
        {
            _repository.Remove(user, note);
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
        public User GetUserByEmail(string email)
        {
            return String.IsNullOrEmpty(email) ? null : _repository.GetUsers().FirstOrDefault(user => user.Email.Equals(email));
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

        private void AddAdminIfNoOne()
        {
            if (_repository.GetUser("Ruslan") == null)
            {
                AddUser(new User("Ruslan", "rus.gumeniuk@gmail.com", "TySiVs1JHrD5R7etJorugFp5HcDMknAbZi1UK0KyPzw=", "Administrators"));
                SaveChanges();
            }
        }
    }
}
