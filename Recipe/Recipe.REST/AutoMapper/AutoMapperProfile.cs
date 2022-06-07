using AutoMapper;
using Firebase.Auth;
using Recipe.Auth.Models;
using Recipe.Auth.ModelsCommon;
using Recipe.Models;
using Recipe.Models.Common;
using Recipe.REST.ViewModels.Category;
using Recipe.REST.ViewModels.Recipe;
using Recipe.REST.ViewModels.Subcategory;
using Recipe.REST.ViewModels.User;
using Recipe.REST.ViewModels.UserData;

namespace Recipe.REST.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region User
            CreateMap<UserRegisterVM, IAuthUser>();
            CreateMap<UserRegisterVM, AuthUser>();

            CreateMap<UserLoginVM, IAuthUser>();
            CreateMap<UserLoginVM, AuthUser>();

            CreateMap<FirebaseAuthLink, UserReturnVM>();
            #endregion

            #region UserData
            CreateMap<UserDataRecipeVM, UserData>();
            CreateMap<UserDataRecipeVM, IUserData>();
            #endregion

            #region Category
            CreateMap<CategoryPostPutVM, Category>();
            CreateMap<CategoryPostPutVM, ICategory>();

            CreateMap<CategorySubcategoryVM, Category>();
            CreateMap<CategorySubcategoryVM, ICategory>();
            #endregion

            #region Subcategory
            CreateMap<SubcategoryRecipeVM, Subcategory>();
            CreateMap<SubcategoryRecipeVM, ISubcategory>();

            CreateMap<SubcategoryPostPutVM, Subcategory>()
             .BeforeMap((src, dest) => { dest.Category = new Category(); })
             .ForPath(dest => dest.Category.CategoryID, input => input.MapFrom(i => i.CategoryID));
            CreateMap<SubcategoryPostPutVM, ISubcategory>()
            .BeforeMap((src, dest) => { dest.Category = new Category(); })
            .ForPath(dest => dest.Category.CategoryID, input => input.MapFrom(i => i.CategoryID));;

            CreateMap<Subcategory, SubcategoryReturnVM>()
            .ForPath(dest => dest.Category.Name, input => input.MapFrom(i => i.Category.Name))
            .ForPath(dest => dest.Category.CategoryID, input => input.MapFrom(i => i.Category.CategoryID));

            CreateMap<ISubcategory, SubcategoryReturnVM>()
            .ForPath(dest => dest.Category.Name, input => input.MapFrom(i => i.Category.Name))
            .ForPath(dest => dest.Category.CategoryID, input => input.MapFrom(i => i.Category.CategoryID));
            #endregion

            #region Recipe
            CreateMap<RecipePostPutVM, Models.Recipe>()
            .BeforeMap((src, dest) => { dest.UserData = new UserData(); dest.Subcategory = new Subcategory(); })
            .ForPath(dest => dest.UserData.UserDataID, input => input.MapFrom(i => i.UserData.UserDataID))
            .ForPath(dest => dest.Subcategory.SubcategoryID, input => input.MapFrom(i => i.Subcategory.SubcategoryID));

            CreateMap<RecipePostPutVM, IRecipe>()
            .BeforeMap((src, dest) => { dest.UserData = new UserData(); dest.Subcategory = new Subcategory(); })
            .ForPath(dest => dest.UserData.UserDataID, input => input.MapFrom(i => i.UserData.UserDataID))
            .ForPath(dest => dest.Subcategory.SubcategoryID, input => input.MapFrom(i => i.Subcategory.SubcategoryID));

            CreateMap<Models.Recipe, RecipeReturnVM>()
            .ForPath(dest => dest.UserData.UserDataID, input => input.MapFrom(i => i.UserData.UserDataID))
            .ForPath(dest => dest.Subcategory.SubcategoryID, input => input.MapFrom(i => i.Subcategory.SubcategoryID))
            .ForPath(dest => dest.Subcategory.Name, input => input.MapFrom(i => i.Subcategory.Name));

            CreateMap<IRecipe, RecipeReturnVM>()
            .ForPath(dest => dest.UserData.UserDataID, input => input.MapFrom(i => i.UserData.UserDataID))
            .ForPath(dest => dest.Subcategory.SubcategoryID, input => input.MapFrom(i => i.Subcategory.SubcategoryID))
            .ForPath(dest => dest.Subcategory.Name, input => input.MapFrom(i => i.Subcategory.Name));
            #endregion
        }
    }
}
