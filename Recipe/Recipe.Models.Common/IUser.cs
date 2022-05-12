namespace Recipe.Models.Common
{
    public interface IUser
    {
        string FirebaseUserID { get; set; }
        string Password { get; set; }
        int UserID { get; set; }
        string Address { get; set; }
        string City { get; set; }
    }
}