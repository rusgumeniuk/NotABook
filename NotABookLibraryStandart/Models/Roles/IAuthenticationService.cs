namespace NotABookLibraryStandart.Models.Roles
{
    public interface IAuthenticationService
    {
        User AuthenticateUser(string username, string password);
        bool IsExistUser(string username, string password);
        void AddUser(string username, string email, string hashedPassword, string[] roles);
        bool RemoveUser(string username, string password);
        bool RemoveUser(User user);
    }
}
