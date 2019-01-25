namespace NotABookLibraryStandart.Models.Roles
{
    public class AnonymusIdentity : Identity
    {
        public AnonymusIdentity() : base(string.Empty, string.Empty, new string[] { }) { }
    }
}
