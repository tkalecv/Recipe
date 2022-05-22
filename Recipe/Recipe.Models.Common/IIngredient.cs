using System.Collections.Generic;

namespace Recipe.Models.Common
{
    public interface IIngredient
    {
        int IngredientID { get; set; }
        string Name { get; set; }
        ICollection<IMeasuringUnit> MeasuringUnits { get; set; }
    }
}