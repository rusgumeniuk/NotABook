using System.Security.Principal;

namespace NotABookLibraryStandart.Models.Roles
{
    public class Identity : IIdentity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public string AuthenticationType => "NotABook authentification";
        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(Name);
        public Identity(string name, string email, string roles)
        {
            Name = name;
            Email = email;
            Roles = roles;
        }
    }
}
