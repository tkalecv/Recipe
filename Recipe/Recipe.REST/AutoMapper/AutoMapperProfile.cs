using AutoMapper;
using Recipe.Auth.Models;
using Recipe.Auth.ModelsCommon;
using Recipe.Models;
using Recipe.Models.Common;
using Recipe.REST.ViewModels;
using Recipe.REST.ViewModels.Ingredient;
using Recipe.REST.ViewModels.User;

namespace Recipe.REST.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IngredientVM, IIngredient>().ReverseMap();
            CreateMap<IngredientVM, Ingredient>().ReverseMap();

            CreateMap<IngredientPostVM, IIngredient>().ReverseMap();
            CreateMap<IngredientPostVM, Ingredient>().ReverseMap();

            CreateMap<RegisterUserVM, IAuthUser>().ReverseMap();
            CreateMap<RegisterUserVM, AuthUser>().ReverseMap();

            CreateMap<LoginUserVM, IAuthUser>().ReverseMap();
            CreateMap<LoginUserVM, AuthUser>().ReverseMap();
        }
    }
}
