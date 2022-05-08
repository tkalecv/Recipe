using System.Text.Json.Serialization;
using Recipe.Models.Common;

namespace Recipe.Models
{
    //TODO: expand interface and model with firebase.auth.user class and remove unnecessary fields. Add Address field. Remove fields from sql script.
    public class User : IUser
    {
        public int UserID { get; set; }
        [JsonIgnore]
        public string FirebaseUserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
