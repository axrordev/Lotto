using AutoMapper;
using Lotto.Domain.Entities.Advertisements;
using Lotto.Domain.Entities.Games;
using Lotto.Domain.Entities.Users;
using Lotto.WebApi.Models.Advertisements;
using Lotto.WebApi.Models.AdvertisementViews;
using Lotto.WebApi.Models.Numbers;
using Lotto.WebApi.Models.Permissions;
using Lotto.WebApi.Models.UserRolePermissions;
using Lotto.WebApi.Models.UserRoles;
using Lotto.WebApi.Models.Users;

namespace Lotto.WebApi.MapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

             // User and related entities
            CreateMap<UserRegisterModel, User>();
            CreateMap<UserUpdateModel, User>();
            CreateMap<User, UserViewModel>();
            CreateMap<User, LoginViewModel>();

            CreateMap<UserRole, UserRoleCreateModel>().ReverseMap();
            CreateMap<UserRole, UserRoleUpdateModel>().ReverseMap();
            CreateMap<UserRole, UserRoleViewModel>().ReverseMap();

            CreateMap<PermissionCreateModel, Permission>();
            CreateMap<PermissionUpdateModel, Permission>();
            CreateMap<Permission, PermissionViewModel>();

            CreateMap<UserRolePermissionCreateModel, UserRolePermission>();
            CreateMap<UserRolePermissionUpdateModel, UserRolePermission>();
            CreateMap<UserRolePermission, UserRolePermissionViewModel>();



            CreateMap<Number, NumberViewModel>();
            CreateMap<NumberCreateModel, Number>();
            CreateMap<NumberUpdateModel, Number>();

             // 🔹 Advertisement Mapping
        CreateMap<AdvertisementCreateModel, Advertisement>()
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));

        CreateMap<AdvertisementUpdateModel, Advertisement>()
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

        CreateMap<Advertisement, AdvertisementViewModel>()
            .ForMember(dest => dest.File, opt => opt.Ignore()); // File ni API orqali yuborish uchun

        // 🔹 AdvertisementView Mapping
        CreateMap<AdvertisementView, AdvertisementViewViewModel>().ReverseMap();
        }
    }
}
