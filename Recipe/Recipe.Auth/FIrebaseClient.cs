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
        public FirebaseAuthProvider AuthProvider { get; set; }

        public FirebaseAdmin.Auth.FirebaseAuth Admin { get; set; }

        public FirebaseClient(IConfiguration configuration)
        {
            
            AuthProvider = ConfigureFirebaseAuth(configuration["Firebase:FirebaseWEBApikey"]);


            Admin = ConfigureFirebaseAdmin(configuration["Firebase:FirebasePrivateKeyPath"]);
        }

        private FirebaseAuthProvider ConfigureFirebaseAuth(string webApiKey) 
        {
            //You should add your firebase web api key in appsettings.json in Recipe.REST project
            return new FirebaseAuthProvider(
                            new FirebaseConfig(webApiKey));
        }

        private FirebaseAdmin.Auth.FirebaseAuth ConfigureFirebaseAdmin(string privateKeyPath)
        {
            FirebaseAdmin.Auth.FirebaseAuth defaultInstance = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;

            //This verification is needed because once you crate the app you have to use default instace
            if (defaultInstance == null)
            {
                FirebaseApp FirebaseAdminApp = FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(privateKeyPath) //You should put path to your firebase private key token file
                                                                           //in appsettings.json in Recipe.REST project
                });

                return FirebaseAdmin.Auth.FirebaseAuth.GetAuth(FirebaseAdminApp);
            }
            else
                return defaultInstance;
        }
    }
}
