using Recipe.Models.Common;
using Recipe.REST.ViewModels.Subcategory;
using Recipe.REST.ViewModels.UserData;

namespace Recipe.REST.ViewModels.Recipe
{
    public class RecipeReturnVM
    {
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SubcategoryRecipeVM Subcategory { get; set; }
        public UserDataRecipeVM UserData { get; set; }
    }
}
