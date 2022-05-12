using System.Text.Json.Serialization;
using Recipe.Models.Common;

namespace Recipe.Models
{
    public class User : Firebase.Auth.User, IUser
    {
        public int UserID { get; set; }
        [JsonIgnore]
        public string FirebaseUserID { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
