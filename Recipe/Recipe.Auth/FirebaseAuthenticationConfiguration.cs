using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Auth
{
    public static class FirebaseAuthenticationConfiguration
    {
        public static void ConfigureFirebaseAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //You should add your firebase project id in appsettings.json in Recipe.REST project
            string firebaseProjectId = configuration["Firebase:FirebaseProjectId"];

            services
             .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.IncludeErrorDetails = true;

                 options.Authority = $"https://securetoken.google.com/{firebaseProjectId}";
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidIssuer = $"https://securetoken.google.com/{firebaseProjectId}",
                     ValidateAudience = true,
                     ValidAudience = firebaseProjectId,
                     ValidateLifetime = true
                 };
             });
        }
    }
}
