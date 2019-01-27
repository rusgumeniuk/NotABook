namespace NotABookLibraryStandart.Models.Roles
{
    public interface IAuthenticationService
    {
        User GetUser(string username);
        User GetUser(string username, string password);
        bool IsExistUser(string username);
        void AddUser(User user);        
        void RemoveUser(User user);
    }
}
