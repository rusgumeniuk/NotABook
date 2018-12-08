using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Models.Roles
{
    public abstract class User : Entity, IUser
    {
        public abstract bool LogIn(string email, string password);
        public abstract bool SignUp(string email, string password);
    }
}
