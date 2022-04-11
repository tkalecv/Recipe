namespace Recipe.Models.Common
{
    public interface IUser
    {
        string Email { get; set; }
        string FirebaseUserID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int UserID { get; set; }
        string UserName { get; set; }
    }
}