using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Auth.ViewModels
{
    public class RegisterUserVM
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
