using Recipe.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Models
{
    public class Category : ICategory
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public ICollection<ISubcategory> Subcategories { get; set; }
    }
}
