using Recipe.Models.Common;

namespace Recipe.Models
{
    public class Ingredient : IIngredient
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }
    }
}
