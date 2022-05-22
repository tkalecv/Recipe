using Recipe.Models.Common;
using System.Collections.Generic;

namespace Recipe.Models
{
    public class MeasuringUnit : IMeasuringUnit
    {
        public int MeasuringUnitID { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public ICollection<IIngredient> Ingredients { get; set; }
    }
}
