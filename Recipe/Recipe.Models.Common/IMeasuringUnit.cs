using System.Collections.Generic;

namespace Recipe.Models.Common
{
    public interface IMeasuringUnit
    {
        string Abbreviation { get; set; }
        int MeasuringUnitID { get; set; }
        string Name { get; set; }
        ICollection<IIngredient> Ingredients { get; set; }
    }
}