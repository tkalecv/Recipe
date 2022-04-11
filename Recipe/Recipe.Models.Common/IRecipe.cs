namespace Recipe.Models.Common
{
    public interface IRecipe
    {
        string Description { get; set; }
        string Name { get; set; }
        int RecipeID { get; set; }
        int UserID { get; set; }
    }
}