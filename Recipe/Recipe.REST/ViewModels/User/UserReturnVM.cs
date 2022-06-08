using Newtonsoft.Json;
using System;

namespace Recipe.REST.ViewModels.User
{
    public class UserReturnVM
    {
        public string Uid { get; set; }

        [JsonProperty("idToken")]
        public string FirebaseToken { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty("expiresIn")]
        public int ExpiresIn { get; set; }

        public DateTime Created { get; set; }
    }
}
