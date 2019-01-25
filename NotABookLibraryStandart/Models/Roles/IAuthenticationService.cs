namespace NotABookLibraryStandart.Models.Roles
{
    public interface IAuthenticationService
    {
        User AuthenticateUser(string username, string password);
    }
}
