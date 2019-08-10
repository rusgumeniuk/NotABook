namespace NotABookLibraryStandart.Models.Roles
{
    public interface IAuthenticationService
    {
        User GetUser(string username);
        User GetUser(string username, string password);
        User GetUserByEmail(string email);
        bool IsExistUser(string username);
        void AddUser(User user);        
        void RemoveUser(User user);
    }
}
