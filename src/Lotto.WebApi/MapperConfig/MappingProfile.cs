using AutoMapper;
using Lotto.Domain.Entities.Games;
using Lotto.WebApi.Models.Numbers;

namespace Lotto.WebApi.MapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Number, NumberViewModel>();
            CreateMap<NumberCreateModel, Number>();
            CreateMap<NumberUpdateModel, Number>();
        }
    }
}
