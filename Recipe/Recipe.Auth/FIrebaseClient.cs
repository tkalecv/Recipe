using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Auth
{
    public class FirebaseClient : IFirebaseClient
    {
        public FirebaseAuthProvider FirebaseAuthProvider { get; set; }

        public FirebaseClient(IConfiguration configuration)
        {
            //You should add your firebase web api key in appsettings.json in Recipe.REST project
            FirebaseAuthProvider = new FirebaseAuthProvider(
                            new FirebaseConfig(configuration["Firebase:FirebaseWEBApikey"]));
        }
    }
}
