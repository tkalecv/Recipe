using System.Collections.Generic;

namespace Recipe.Models.Common
{
    public interface IUserData
    {
        string FirebaseUserID { get; set; }
        int UserDataID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Address { get; set; }
        string City { get; set; }
        ICollection<IRecipe> Recipes { get; set; }
    }
}