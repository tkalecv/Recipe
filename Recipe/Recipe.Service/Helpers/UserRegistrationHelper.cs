using Microsoft.Extensions.Configuration;
using Recipe.Service.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Recipe.Service.Helpers
{
    internal class UserRegistrationHelper : IUserRegistrationHelper
    {
        private readonly IConfiguration _configuration;

        public UserRegistrationHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Method verifies if E-mail address and password are correct and returns corresponding error message if they are not.
        /// </summary>
        /// <param name="emailAddress">E-mail address you want to verify</param>
        /// <param name="password">Password you want to verify</param>
        /// <returns>(bool, string)</returns>
        public (bool, string) ValidateEmailAndPassword(string emailAddress, string password)
        {
            string PasswordErrorMessage = string.Empty;
            string EmailErrorMessage = string.Empty;
            bool IsPasswordValidated;
            bool IsEmailValidated;

            (IsEmailValidated, EmailErrorMessage) = ValidateEmail(emailAddress);

            (IsPasswordValidated, PasswordErrorMessage) = ValidatePassword(password);

            if (!IsEmailValidated || !IsPasswordValidated)
                return (false, EmailErrorMessage + PasswordErrorMessage);

            return (true, "");
        }

        /// <summary>
        /// Method verifies if E-mail address is correct and returns corresponding error message if it is not.
        /// </summary>
        /// <param name="emailAddress">E-mail address you want to verify</param>
        /// <returns>(bool, string)</returns>
        public (bool, string) ValidateEmail(string emailAddress)
        {
            Regex EmailVerificationRegex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                                      + "@"
                                                      + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");

            if (EmailVerificationRegex.IsMatch(emailAddress))
                return (true, "");

            return (false, "The specified E-mail is not in the required form. \n");
        }

        /// <summary>
        /// Method verifies if password has expected strength and returns corresponding error message if it does not.
        /// You can control password strength in "appsettings.json" file
        /// </summary>
        /// <param name="password">Password you want to verify</param>
        /// <returns>(bool, string)</returns>
        public (bool, string) ValidatePassword(string password)
        {
            string ErrorMessage = string.Empty;
            bool ValidationPassed = true;

            if (string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Password should not be empty. \n";
                return (false, ErrorMessage);
            }

            bool VerifyHasNumber = ParseToBoolean(_configuration["PasswordStrength:HasNumber"]);
            bool VerifyHasUpperChar = ParseToBoolean(_configuration["PasswordStrength:HasUpperChar"]);
            bool VerifyHasLowerChar = ParseToBoolean(_configuration["PasswordStrength:HasLowerChar"]);
            bool VerifyHasSymbols = ParseToBoolean(_configuration["PasswordStrength:HasSymbol"]);

            string[] MinMaxCharsArr =
                _configuration["PasswordStrength:HasMinMaxChars"].Split(",").Length != 2
                ? new string[] { "8", "15" }
                : _configuration["PasswordStrength:HasMinMaxChars"].Split(",");

            Regex HasNumber = new Regex(@"[0-9]+");
            Regex HasUpperChar = new Regex(@"[A-Z]+");
            Regex HasMinMaxChars = new Regex(@".{" + String.Join(",", MinMaxCharsArr) + "}");
            Regex HasLowerChar = new Regex(@"[a-z]+");
            Regex HasSymbol = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (VerifyHasLowerChar && !HasLowerChar.IsMatch(password))
            {
                ErrorMessage += "Password should contain at least one lower case letter. \n";
            }
            if (VerifyHasUpperChar && !HasUpperChar.IsMatch(password))
            {
                ErrorMessage += "Password should contain at least one upper case letter. \n";
            }
            if (!HasMinMaxChars.IsMatch(password))
            {
                ErrorMessage += $"Password should not be less than {MinMaxCharsArr[0]} or greater than {MinMaxCharsArr[1]} characters. \n";
            }
            if (VerifyHasNumber && !HasNumber.IsMatch(password))
            {
                ErrorMessage += "Password should contain at least one numeric value. \n";
            }
            if (VerifyHasSymbols && !HasSymbol.IsMatch(password))
            {
                ErrorMessage += "Password should contain at least one special case character.";
            }

            if (ErrorMessage != string.Empty)
                ValidationPassed = false;

            return (ValidationPassed, ErrorMessage);
        }

        /// <summary>
        /// Method checks if string value can be parsed to boolean or not. If not "true" is returned as default value.
        /// </summary>
        /// <param name="value">String value that you want to parse</param>
        /// <returns>bool</returns>
        private bool ParseToBoolean(string value)
        {
            if (value.ToLower().Equals("true") || value.ToLower().Equals("false"))
                return bool.Parse(value);

            return true;
        }

    }
}
