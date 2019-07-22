using NotABookLibraryStandart.Models.BookElements;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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
        public IList<Book> Books = new ObservableCollection<Book>();
        public IList<Category> Categories = new ObservableCollection<Category>();
        public User()
        {
            Id = Guid.NewGuid();
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
    }
}

