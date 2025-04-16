using AutoMapper;
using Lotto.Domain.Entities.Games;
using Lotto.Service.Services.Footballs;
using Lotto.WebApi.Models.Footballs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.Footballs
{
public class FootballApiService(IFootballService _footballService, IMapper mapper) : IFootballApiService
    {
        public async ValueTask<FootballViewModel> CreateAsync(FootballCreateModel createModel)
        {
            var football = mapper.Map<Football>(createModel);
            var createdFootball = await _footballService.CreateAsync(football);
            return mapper.Map<FootballViewModel>(createdFootball);
        }

        public async ValueTask<PlayFootballViewModel> PlayAsync(PlayFootballCreateModel createModel)
        {
            var playFootball = mapper.Map<PlayFootball>(createModel);
            var createdPlayFootball = await _footballService.PlayAsync(playFootball);
            return mapper.Map<PlayFootballViewModel>(createdPlayFootball);
        }

        public async ValueTask<FootballResultViewModel> AddResultAsync(FootballResultCreateModel createModel)
        {
            var footballResult = mapper.Map<FootballResult>(createModel);
            var createdFootballResult = await _footballService.AddResultAsync(footballResult);
            return mapper.Map<FootballResultViewModel>(createdFootballResult);
        }

        public async ValueTask AnnounceResultsAsync(long footballId)
        {
            await _footballService.AnnounceResultsAsync(footballId);
        }

        public async ValueTask<IEnumerable<PlayFootballViewModel>> GetUserPlaysAsync(long userId)
        {
            var plays = await _footballService.GetUserPlaysAsync(userId);
            return mapper.Map<IEnumerable<PlayFootballViewModel>>(plays);
        }
    }
}
