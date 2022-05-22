namespace Recipe.Models.Common
{
    public interface IPreparationStep
    {
        string Description { get; set; }
        int Number { get; set; }
        int PreparationStepID { get; set; }
        IRecipe Recipe { get; set; }
    }
}