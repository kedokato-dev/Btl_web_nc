using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Btl_web_nc.Services
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private CancellationTokenSource _cts;

        public bool IsRunning { get; private set; }

        public EmailBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void StartService()
        {
            if (IsRunning) return;

            _cts = new CancellationTokenSource();
            ExecuteAsync(_cts.Token);
            IsRunning = true;
        }

        public void StopService()
        {
            if (!IsRunning) return;

            _cts.Cancel();
            IsRunning = false;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var emailNotificationService = scope.ServiceProvider.GetRequiredService<EmailNotificationService>();

                    try
                    {
                        await emailNotificationService.SendPeriodicEmailsAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending emails: {ex.Message}");
                    }
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}