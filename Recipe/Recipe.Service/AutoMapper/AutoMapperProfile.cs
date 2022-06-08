using AutoMapper;
using Firebase.Auth;
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

            #region UserRecordArgs
            CreateMap<UserRecordArgs, AuthUser>().ReverseMap();
            CreateMap<UserRecordArgs, IAuthUser>().ReverseMap();
            #endregion

            #region UserData
            CreateMap<UserData, AuthUser>()
                .ForMember(dest => dest.Uid, input => input.MapFrom(i => i.FirebaseUserID))
                .ReverseMap();

            CreateMap<IUserData, IAuthUser>()
                .ForMember(dest => dest.Uid, input => input.MapFrom(i => i.FirebaseUserID))
                .ReverseMap();

            CreateMap<IUserData, AuthUser>()
                .ForMember(dest => dest.Uid, input => input.MapFrom(i => i.FirebaseUserID))
                .ReverseMap();
            #endregion

            #region UserRecord
            CreateMap<UserRecord, AuthUser>();
            CreateMap<UserRecord, IAuthUser>();
            #endregion

            #region FirebaseUser
            CreateMap<User, IAuthUser>()
            .ForMember(dest => dest.Uid, input => input.MapFrom(i => i.LocalId));
            CreateMap<User, AuthUser>()
            .ForMember(dest => dest.Uid, input => input.MapFrom(i => i.LocalId));

            #endregion
        }
    }
}
