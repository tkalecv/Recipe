using System.Collections.Generic;

namespace Recipe.Models.Common
{
    public interface IRecipe
    {
        string Description { get; set; }
        string Name { get; set; }
        int RecipeID { get; set; }
        IUserData UserData { get; set; }
        ICollection<IPicture> Pictures { get; set; }
        ICollection<IPreparationStep> PreparationSteps { get; set; }
        IRecipeAttributes RecipeAttributes { get; set; }
        ISubcategory Subcategory { get; set; }
    }
}