using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.REST.ViewModels.User
{
    public class RegisterUserVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
