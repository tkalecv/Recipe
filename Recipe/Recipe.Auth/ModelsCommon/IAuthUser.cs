namespace Recipe.Auth.ModelsCommon
{
    public interface IAuthUser
    {
        string Address { get; set; }
        string City { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string DisplayName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string PhotoUrl { get; set; }
    }
}