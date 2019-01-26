namespace NotABookLibraryStandart.Models.Roles
{
    public class User : Entity
    {
        public User(string username, string email, string[] roles)
        {
            Username = username;
            Email = email;
            Roles = roles;
        }
        public User(string username, string email, string hashedPassword, string[] roles) : this(username, email, roles)
        {           
            HashedPassword = hashedPassword;
        }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string HashedPassword { get; private set; }
        public string[] Roles { get; private set; }
    }
}

