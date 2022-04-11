namespace Recipe.Models.Common
{
    public interface IUserLikedRecipe
    {
        int RecipeID { get; set; }
        int UserID { get; set; }
    }
}