using System;
using System.Collections.Generic;
using System.Text;

namespace NotABookLibraryStandart.Models.Roles
{
    public class StandartUser : User
    {
        public override bool LogIn(string email, string password)
        {
            throw new NotImplementedException();
        }

        public override bool SignUp(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
