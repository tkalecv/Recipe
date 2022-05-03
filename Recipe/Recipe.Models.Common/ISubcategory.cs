namespace Recipe.Models.Common
{
    public interface ISubcategory
    {
        int CategoryID { get; set; }
        string Name { get; set; }
        int SubcategoryID { get; set; }
    }
}