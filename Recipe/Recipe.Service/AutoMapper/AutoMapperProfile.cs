using AutoMapper;
using FirebaseAdmin.Auth;
using Recipe.Auth.Models;
using Recipe.Auth.ModelsCommon;
using Recipe.Models;
using Recipe.Models.Common;

namespace Recipe.Service.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Ingredient, IIngredient>().ReverseMap();
            CreateMap<Subcategory, ISubcategory>().ReverseMap();
            CreateMap<UserData, IUserData>().ReverseMap();

            CreateMap<UserRecordArgs, AuthUser>().ReverseMap();
            CreateMap<UserRecordArgs, IAuthUser>().ReverseMap();

            CreateMap<UserData, AuthUser>()
                .ForMember(dest => dest.Uid, input => input.MapFrom(i => i.FirebaseUserID))
                .ReverseMap();

            CreateMap<UserData, IAuthUser>()
                .ForMember(dest => dest.Uid, input => input.MapFrom(i => i.FirebaseUserID))
                .ReverseMap();
        }
    }
}
