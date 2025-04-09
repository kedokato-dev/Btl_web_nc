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
        private bool _isRunning;
        private static TimeSpan _sendTime = new TimeSpan(2, 06, 0); // Giờ gửi mặc định: 8:00 sáng
        private Task _backgroundTask;

        public EmailBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _isRunning = false;
        }

        public bool IsRunning => _isRunning;

        public void StartService()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _backgroundTask = Task.Run(() => ExecuteAsync(CancellationToken.None)); // Chạy dịch vụ trong một Task riêng
            }
        }

        public void StopService()
        {
            _isRunning = false;
        }

        public static void UpdateSendTime(TimeSpan sendTime)
        {
            _sendTime = sendTime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested && _isRunning)
            {
                var now = DateTime.Now;
                var nextRunTime = DateTime.Today.Add(_sendTime); // Sử dụng giờ gửi được thiết lập
                if (now > nextRunTime)
                {
                    nextRunTime = nextRunTime.AddDays(1); // Nếu đã qua giờ gửi, chạy vào ngày hôm sau
                }

                var delay = nextRunTime - now;
                await Task.Delay(delay, stoppingToken);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var emailNotificationService = scope.ServiceProvider.GetRequiredService<EmailNotificationService>();

                    try
                    {
                        await emailNotificationService.SendEmailsForNewsletterAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending emails: {ex.Message}");
                    }
                }
            }
        }
    }
}