using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace NotABookLibraryStandart.Models.Roles
{
    public class AuthenticationService : IAuthenticationService
    {
        private static readonly List<InternalUserData> _users = new List<InternalUserData>()
        {
            new InternalUserData("Ruslan", "rus.gumeniuk@gmail.com",
            "TySiVs1JHrD5R7etJorugFp5HcDMknAbZi1UK0KyPzw=", new string[] { "Administrators" }),
            new InternalUserData("User", "user@company.com",
            "xT50saU4tyGOzWY48EMaXSI7Bv89hJJn32sagaMuJzo=", new string[] { })
        };
        public User AuthenticateUser(string username, string password)
        {
            InternalUserData userData = _users.FirstOrDefault(u => u.Username.Equals(username) && u.HashedPassword.Equals(CalculateHash(password, u.Username)));
            return userData != null ? new User(userData.Username, userData.Email, userData.Roles) : throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");
        }

        private string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }

        private class InternalUserData
        {
            public InternalUserData(string username, string email, string hashedPassword, string[] roles)
            {
                Username = username;
                Email = email;
                HashedPassword = hashedPassword;
                Roles = roles;
            }
            public string Username { get; private set; }
            public string Email { get; private set; }
            public string HashedPassword { get; private set; }
            public string[] Roles { get; private set; }
        }
    }
}