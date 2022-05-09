using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Auth;

namespace Recipe.Auth
{
    public class FirebaseClient : IFirebaseClient
    {
        public FirebaseAuthProvider FirebaseAuthProvider { get; set; }

        public FirebaseAdmin.Auth.FirebaseAuth FirebaseAdminDefault { get; set; }

        public FirebaseClient(IConfiguration configuration)
        {
            //You should add your firebase web api key in appsettings.json in Recipe.REST project
            FirebaseAuthProvider = new FirebaseAuthProvider(
                            new FirebaseConfig(configuration["Firebase:FirebaseWEBApikey"]));

            FirebaseApp FirebaseAdminApp = FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(configuration["Firebase:FirebasePrivateKeyPath"]) //You should put path to your firebase private key token file
                                                                                                        //in appsettings.json in Recipe.REST project
            });

            FirebaseAdminDefault = FirebaseAdmin.Auth.FirebaseAuth.GetAuth(FirebaseAdminApp);
        }
    }
}
