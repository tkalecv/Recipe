using System;
using System.Collections.Generic;
using System.Text;
using Recipe.Models.Common;

namespace Recipe.Models
{
    public class Recipe : IRecipe
    {
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
    }
}
