using Recipe.REST.ViewModels.Category;

namespace Recipe.REST.ViewModels.Subcategory
{
    public class SubcategoryReturnVM
    {
        public int SubcategoryID { get; set; }
        public string Name { get; set; }
        public CategorySubcategoryVM Category { get; set; }
    }
}
