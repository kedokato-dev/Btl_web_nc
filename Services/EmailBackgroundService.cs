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

        public EmailBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
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
                        // Log lỗi nếu cần
                        Console.WriteLine($"Error sending emails: {ex.Message}");
                    }
                }

                // Chờ 24 giờ trước khi chạy lại
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}