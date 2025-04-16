using AutoMapper;
using Lotto.Domain.Entities.Advertisements;
using Lotto.Domain.Entities.Games;
using Lotto.Domain.Entities.Users;
using Lotto.WebApi.Models.Advertisements;
using Lotto.WebApi.Models.Footballs;
using Lotto.WebApi.Models.Numbers;
using Lotto.WebApi.Models.Permissions;
using Lotto.WebApi.Models.PlayNumbers;
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


            // Number
            CreateMap<NumberCreateModel, Number>();
            CreateMap<Number, NumberViewModel>();
            CreateMap<PlayNumberCreateModel, PlayNumber>();
            CreateMap<PlayNumber, PlayNumberViewModel>();


            //Advertisement
            CreateMap<Advertisement, AdvertisementViewModel>();
            CreateMap<AdvertisementCreateModel, Advertisement>();
            CreateMap<AdvertisementUpdateModel, Advertisement>();


            // Football
            CreateMap<FootballCreateModel, Football>();
            CreateMap<Football, FootballViewModel>();
            CreateMap<PlayFootballCreateModel, PlayFootball>();
            CreateMap<PlayFootball, PlayFootballViewModel>();
            CreateMap<FootballResultCreateModel, FootballResult>();
            CreateMap<FootballResult, FootballResultViewModel>();
            CreateMap<GoalDetailCreateModel, GoalDetail>();
            CreateMap<GoalDetail, GoalDetailViewModel>();

        }
    }
}
