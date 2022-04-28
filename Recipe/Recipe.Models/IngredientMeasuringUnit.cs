using Recipe.Models.Common;
namespace Recipe.Models
{
    public class IngredientMeasuringUnit : IIngredientMeasuringUnit
    {
        public int IngredientMeasuringUnitID { get; set; }
        public int MeasuringUnitID { get; set; }
    }
}
