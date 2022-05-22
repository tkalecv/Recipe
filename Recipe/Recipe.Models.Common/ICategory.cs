using System.Collections.Generic;

namespace Recipe.Models.Common
{
    public interface ICategory
    {
        int CategoryID { get; set; }
        string Name { get; set; }
        ICollection<ISubcategory> Subcategories { get; set; }
    }
}