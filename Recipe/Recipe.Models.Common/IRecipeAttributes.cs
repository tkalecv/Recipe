using System;

namespace Recipe.Models.Common
{
    public interface IRecipeAttributes
    {
        string Advice { get; set; }
        DateTime CreatedDate { get; set; }
        int Likes { get; set; }
        int Person { get; set; }
        TimeSpan PrepareTime { get; set; }
        int RecipeAttributesID { get; set; }
        IRecipe Recipe { get; set; }
        string Serving { get; set; }
        int Stars { get; set; }
    }
}