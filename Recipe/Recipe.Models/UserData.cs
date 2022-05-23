using System.Collections.Generic;
using System.Text.Json.Serialization;
using Recipe.Models.Common;

namespace Recipe.Models
{
    public class UserData : IUserData
    {
        [JsonIgnore]
        public int UserDataID { get; set; }
        [JsonIgnore]
        public string FirebaseUserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public ICollection<IRecipe> Recipes { get; set; }
    }
}
