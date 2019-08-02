using NotABookLibraryStandart.Models.BookElements;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace NotABookLibraryStandart.Models.Roles
{
    public class User : Entity
    {
        [Required(ErrorMessage = "Username can not be empty!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email can not be empty!")]
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Roles { get; set; }

        public IList<Book> Books { get; set; } = new ObservableCollection<Book>();
        public IList<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public IList<LinkNoteCategory> LinkNoteCategories { get; set; } = new ObservableCollection<LinkNoteCategory>();
        public IEnumerable<Note> GetAllNotes
        {
            get
            {
                ISet<Note> notes = new HashSet<Note>();

                foreach (var book in Books)
                {
                    notes.UnionWith(book.Notes);
                }

                return notes;
            }
        }

        public User()
        {
            Id = Guid.NewGuid();
            Books.Add(new Book("Your first book"));            
        }
        public User(string username, string email, string roles) : this()
        {
            Username = username;
            Email = email;
            Roles = roles;
        }
        public User(string username, string email, string hashedPassword, string roles) : this(username, email, roles)
        {
            HashedPassword = hashedPassword;
        }
        public User(string username, string email, string hashedPassword, string roles, IList<Book> books, IList<Category> categories) : this(username, email, roles, hashedPassword)
        {
            Books = books ?? new ObservableCollection<Book>();
            Categories = categories ?? new ObservableCollection<Category>();
        }

        public static string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }

        public void RemoveBook(Book book)
        {
            Books.Remove(book);
            if(Books.Count == 0)
                Books.Add(new Book("Default book"));            
        }
        public bool Add(Book book)
        {
            if (Books.Contains(book))
                return false;
            Books.Add(book);            
            return Books.Contains(book);
        }      
    }
}

