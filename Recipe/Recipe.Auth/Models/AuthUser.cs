using Recipe.Auth.ModelsCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Auth.Models
{
    public class AuthUser : IAuthUser
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
    }
}
