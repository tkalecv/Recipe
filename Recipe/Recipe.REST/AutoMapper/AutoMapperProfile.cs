using AutoMapper;
using Firebase.Auth;
using Recipe.Auth.Models;
using Recipe.Auth.ModelsCommon;
using Recipe.Models;
using Recipe.Models.Common;
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
            //User
            CreateMap<UserRegisterVM, IAuthUser>();
            CreateMap<UserRegisterVM, AuthUser>();

            CreateMap<UserLoginVM, IAuthUser>();
            CreateMap<UserLoginVM, AuthUser>();

            CreateMap<FirebaseAuthLink, UserReturnVM>();

            //UserData
            CreateMap<UserDataRecipeVM, UserData>();
            CreateMap<UserDataRecipeVM, IUserData>();

            //Subcategory
            CreateMap<SubcategoryRecipeVM, Subcategory>();
            CreateMap<SubcategoryRecipeVM, ISubcategory>();

            //Recipe
            CreateMap<RecipePostPutVM, Models.Recipe>()
            .BeforeMap((src, dest) => { dest.UserData = new UserData(); dest.Subcategory = new Subcategory(); })
            .ForPath(dest => dest.UserData.UserDataID, input => input.MapFrom(i => i.UserData.UserDataID))
            .ForPath(dest => dest.Subcategory.SubcategoryID, input => input.MapFrom(i => i.Subcategory.SubcategoryID));

            CreateMap<RecipePostPutVM, IRecipe>()
            .BeforeMap((src, dest) => { dest.UserData = new UserData(); dest.Subcategory = new Subcategory(); })
            .ForPath(dest => dest.UserData.UserDataID, input => input.MapFrom(i => i.UserData.UserDataID))
            .ForPath(dest => dest.Subcategory.SubcategoryID, input => input.MapFrom(i => i.Subcategory.SubcategoryID));

            CreateMap<Models.Recipe, RecipeReturnVM>();
            CreateMap<IRecipe, RecipeReturnVM>();
        }
    }
}
