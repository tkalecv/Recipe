using Recipe.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Models
{
    public class Subcategory : ISubcategory
    {
        public int SubcategoryID { get; set; }
        public string Name { get; set; }
        public ICategory Category { get; set; }
    }
}
