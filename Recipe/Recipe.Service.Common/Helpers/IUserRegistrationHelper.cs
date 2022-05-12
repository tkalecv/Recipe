namespace Recipe.Service.Common.Helpers
{
    public interface IUserRegistrationHelper
    {
        (bool, string) ValidateEmail(string emailAddress);
        (bool, string) ValidateEmailAndPassword(string emailAddress, string password);
        (bool, string) ValidatePassword(string password);
    }
}