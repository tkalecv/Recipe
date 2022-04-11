using System;
using System.Collections.Generic;
using System.Text;
using Recipe.Models.Common;

namespace Recipe.Models
{
    public class PreparationStep : IPreparationStep
    {
        public int PreparationStepID { get; set; }
        public int RecipeID { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
    }
}
