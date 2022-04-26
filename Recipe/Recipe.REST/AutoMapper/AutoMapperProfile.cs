using AutoMapper;
using Recipe.Models;
using Recipe.Models.Common;
using Recipe.REST.ViewModels;

namespace Recipe.REST.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IngredientPostVM, IIngredient>().ReverseMap();
            CreateMap<IngredientPostVM, Ingredient>().ReverseMap();
        }
    }
}
