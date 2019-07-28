using NotABookLibraryStandart.Models.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NotABookTests.ModelTests
{
    public class UserTests
    {
        private readonly User user;
        public UserTests()
        {
            user = new User();        
        }

        [Fact]
        public void CreateUser_CreateDefaultBook_ReturnsNotEmpty()
        {
            User newUser = new User();
            Assert.NotEmpty(newUser.Books);
        }

    }
}
