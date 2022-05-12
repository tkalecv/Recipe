namespace Recipe.Models.Common
{
    public interface IUserData
    {
        string FirebaseUserID { get; set; }
        int UserDataID { get; set; }
        string Address { get; set; }
        string City { get; set; }
    }
}