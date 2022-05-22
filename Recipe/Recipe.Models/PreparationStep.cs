using Recipe.Models.Common;

namespace Recipe.Models
{
    public class PreparationStep : IPreparationStep
    {
        public int PreparationStepID { get; set; }
        public IRecipe Recipe { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
    }
}
