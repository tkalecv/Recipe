using Recipe.Models.Common;
namespace Recipe.Models
{
    public class IngredientMeasuringUnit : IIngredientMeasuringUnit
    {
        public int IngredientID { get; set; }
        public int MeasuringUnitID { get; set; }
    }
}
