using Recipe.Models.Common;
using System.Collections.Generic;

namespace Recipe.Models
{
    public class Recipe : IRecipe
    {
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IUserData UserData { get; set; }
        public ISubcategory Subcategory { get; set; }
        public ICollection<IPicture> Pictures { get; set; }
        public ICollection<IPreparationStep> PreparationSteps { get; set; }
        public IRecipeAttributes RecipeAttributes { get; set; }
    }
}
