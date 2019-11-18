using GmailAPI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.WebProject.BackgroundServices
{
    public class BackgroundService : IHostedService, IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private Timer _timer;

        public BackgroundService(ILogger<BackgroundService> logger, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(SyncGmailAsync, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private async void SyncGmailAsync(object state)
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var systemService = scope.ServiceProvider.GetRequiredService<IGmailAPIService>();

                await systemService.GmailSync();
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
