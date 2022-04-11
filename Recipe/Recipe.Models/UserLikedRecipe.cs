using System;
using System.Collections.Generic;
using System.Text;
using Recipe.Models.Common;

namespace Recipe.Models
{
    internal class UserLikedRecipe : IUserLikedRecipe
    {
        public int UserID { get; set; }
        public int RecipeID { get; set; }
    }
}
