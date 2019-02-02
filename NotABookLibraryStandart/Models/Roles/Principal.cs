using System.Security.Principal;
using System.Linq;

namespace NotABookLibraryStandart.Models.Roles
{
    public class Principal : IPrincipal
    {
        private Identity identity;
        public Identity Identity
        {
            get => identity ?? new AnonymusIdentity();
            set => identity = value;
        }
        IIdentity IPrincipal.Identity => this.Identity;
        public bool IsInRole(string role)
        {
            return true;// identity.Roles.Contains(role);
        }
    }
}
