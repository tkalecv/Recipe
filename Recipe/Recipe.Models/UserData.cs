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
        public string Address { get; set; }
        public string City { get; set; }
    }
}
