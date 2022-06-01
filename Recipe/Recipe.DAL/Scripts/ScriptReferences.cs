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
            public static string SP_DeleteUserData = nameof(SP_DeleteUserData);
            public static string SP_UpdateUserData = nameof(SP_UpdateUserData);
        }

        public static class Category
        {
            public static string SP_CreateCategory = nameof(SP_CreateCategory);
            public static string SP_RetrieveCategory = nameof(SP_RetrieveCategory);
            public static string SP_DeleteCategory = nameof(SP_DeleteCategory);
            public static string SP_UpdateCategory = nameof(SP_UpdateCategory);
        }

        public static class Subcategory
        {
            public static string SP_CreateSubcategory = nameof(SP_CreateSubcategory);
            public static string SP_RetrieveSubcategory = nameof(SP_RetrieveSubcategory);
            public static string SP_DeleteSubcategory = nameof(SP_DeleteSubcategory);
            public static string SP_UpdateSubcategory = nameof(SP_UpdateSubcategory);
        }
    }
}
