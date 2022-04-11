using Recipe.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Models
{
    public class IngredientMeasuringUnit : IIngredientMeasuringUnit
    {
        public int IngredientID { get; set; }
        public int MeasuringUnitID { get; set; }
    }
}
