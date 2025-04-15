using AutoMapper;
using Lotto.Domain.Entities.Games;
using Lotto.Service.Services.Footballs;
using Lotto.WebApi.Models.Footballs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.Footballs
{
public class FootballApiService : IFootballApiService
    {
        private readonly IFootballService _footballService;
        private readonly IMapper _mapper;

        public FootballApiService(IFootballService footballService, IMapper mapper)
        {
            _footballService = footballService;
            _mapper = mapper;
        }

        public async ValueTask<FootballViewModel> CreateAsync(FootballCreateModel createModel)
        {
            var football = _mapper.Map<Football>(createModel);
            var createdFootball = await _footballService.CreateAsync(football);
            return _mapper.Map<FootballViewModel>(createdFootball);
        }

        public async ValueTask<PlayFootballViewModel> PlayAsync(PlayFootballCreateModel createModel)
        {
            var playFootball = _mapper.Map<PlayFootball>(createModel);
            var createdPlayFootball = await _footballService.PlayAsync(playFootball);
            return _mapper.Map<PlayFootballViewModel>(createdPlayFootball);
        }

        public async ValueTask<FootballResultViewModel> AddResultAsync(FootballResultCreateModel createModel)
        {
            var footballResult = _mapper.Map<FootballResult>(createModel);
            var createdFootballResult = await _footballService.AddResultAsync(footballResult);
            return _mapper.Map<FootballResultViewModel>(createdFootballResult);
        }

        public async ValueTask AnnounceResultsAsync(long footballId)
        {
            await _footballService.AnnounceResultsAsync(footballId);
        }

        public async ValueTask<IEnumerable<PlayFootballViewModel>> GetUserPlaysAsync(long userId)
        {
            var plays = await _footballService.GetUserPlaysAsync(userId);
            return _mapper.Map<IEnumerable<PlayFootballViewModel>>(plays);
        }
    }
}
