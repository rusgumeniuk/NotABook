using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace NotABookLibraryStandart.Models.Roles
{
    public class AuthenticationService : IAuthenticationService
    {
        private static readonly List<User> _users = new List<User>()
        {
            new User("Ruslan", "rus.gumeniuk@gmail.com",
            "TySiVs1JHrD5R7etJorugFp5HcDMknAbZi1UK0KyPzw=", new string[] { "Administrators" }),
            new User("User", "user@company.com",
            "LLArj1vn/AaU39wa7ngdAeSsD941vgqZXaadmktfImI=", new string[] { "Users" })
        };
        public User AuthenticateUser(string username, string password)
        {            
            return _users.FirstOrDefault(u => u.Username.Equals(username) && u.HashedPassword.Equals(CalculateHash(password, u.Username))) 
                ?? throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");
        }
        internal string CalculateHash(string clearTextPassword, string salt)
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