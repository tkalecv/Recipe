namespace Recipe.Models.Common
{
    public interface ISubcategory
    {
        ICategory Category { get; set; }
        string Name { get; set; }
        int SubcategoryID { get; set; }
    }
}