using Recipe.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Models
{
    public class Ingredient : IIngredient
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }
    }
}
