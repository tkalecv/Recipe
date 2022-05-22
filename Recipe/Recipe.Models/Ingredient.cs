using Recipe.Models.Common;
using System.Collections.Generic;

namespace Recipe.Models
{
    public class Ingredient : IIngredient
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }
        public ICollection<IMeasuringUnit> MeasuringUnits { get; set; }
    }
}
