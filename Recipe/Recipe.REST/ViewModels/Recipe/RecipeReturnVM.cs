using Recipe.Models.Common;

namespace Recipe.REST.ViewModels.Recipe
{
    public class RecipeReturnVM
    {
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ISubcategory Subcategory { get; set; }
        public IUserData UserData { get; set; }
    }
}
