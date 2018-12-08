using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Models.Roles
{
    interface IUser
    {
        bool LogIn(string email, string password);
        bool SignUp(string email, string password);
    }
}
