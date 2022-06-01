using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.DAL.Scripts
{
    public static class ScriptReferences
    {
        public static class Recipe
        {
            public static string SP_CreateRecipe = nameof(SP_CreateRecipe);
            public static string SP_RetrieveRecipe = nameof(SP_RetrieveRecipe);
            public static string SP_DeleteRecipe = nameof(SP_DeleteRecipe);
            public static string SP_UpdateRecipe = nameof(SP_UpdateRecipe);
        }

        public static class UserData
        {
            public static string SP_CreateUserData = nameof(SP_CreateUserData);
            public static string SP_RetrieveUserData = nameof(SP_RetrieveUserData);
        }

        public static class Category
        {
            public static string SP_CreateCategory = nameof(SP_CreateCategory);
            public static string SP_RetrieveCategory = nameof(SP_RetrieveCategory);
        }

        public static class Subcategory
        {
            public static string SP_CreateSubcategory = nameof(SP_CreateSubcategory);
            public static string SP_RetrieveSubcategory = nameof(SP_RetrieveSubcategory);
        }
    }
}
