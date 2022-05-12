using AutoMapper;
using Recipe.Models;
using Recipe.Models.Common;

namespace Recipe.Service.AutoMapper
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Ingredient, IIngredient>().ReverseMap();
        }
    }
}
