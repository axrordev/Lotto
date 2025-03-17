using Lotto.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;



namespace Lotto.Service.Helpers
{
    public class GameCompletionService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public GameCompletionService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var now = DateTime.UtcNow;
               var expiredGames = await unitOfWork.NumberRepository
                .SelectAsQueryable(n => !n.IsCompleted && n.Deadline <= now)
                .ToListAsync();
                

                foreach (var game in expiredGames)
                {
                    game.IsCompleted = true;
                    await unitOfWork.NumberRepository.UpdateAsync(game);
                }

                await unitOfWork.SaveAsync();

                // Har 1 soniyada tekshiradi
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
    }

}
