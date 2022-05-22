using System;
using Recipe.Models.Common;

namespace Recipe.Models
{
    public class RecipeAttributes : IRecipeAttributes
    {
        public int RecipeAttributesID { get; set; }
        public IRecipe Recipe { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Person { get; set; }
        public TimeSpan PrepareTime { get; set; }
        public string Serving { get; set; }
        public string Advice { get; set; }
        public int Stars { get; set; }
        public int Likes { get; set; }
    }
}
