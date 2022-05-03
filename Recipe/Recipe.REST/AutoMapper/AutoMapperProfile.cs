using AutoMapper;
using Recipe.Models;
using Recipe.Models.Common;
using Recipe.REST.ViewModels;
using Recipe.REST.ViewModels.Ingredient;

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
        }
    }
}
