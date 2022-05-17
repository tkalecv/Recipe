namespace Recipe.Auth.ModelsCommon
{
    public interface IAuthUser
    {
        string Address { get; set; }
        string City { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }

        bool Disabled { get; set; }
        string DisplayName { get; set; }
        string Email { get; set; }
        bool EmailVerified { get; set; }
        string Password { get; set; }
        string PhoneNumber { get; set; }
        string PhotoUrl { get; set; }
        string Uid { get; set; }
    }
}