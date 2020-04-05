using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using charity.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace charity.FCM
{
    public class MessagingWorker : BackgroundService
    {
        private readonly ILogger<MessagingWorker> _logger;
        private readonly DataContext _db;
        public MessagingWorker(ILogger<MessagingWorker> logger, DataContext db)
        {
            _logger = logger;
            _db = db;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var delay = TimeSpan.FromMinutes(1);

            _logger.LogInformation("Starting MessagingWorker background service.");
            while (!stoppingToken.IsCancellationRequested)
            {
                var sent = await SendAllDelayedNotifications();
                _logger.LogInformation($"Notified others about {sent} call(s).");
                await Task.Delay(delay, stoppingToken);
            }
        }

        private async Task<int> SendAllDelayedNotifications()
        {
            var queue = NotificationFactory.DelayedNotificationQueue;
            var now = DateTime.Now;
            var sent = 0;
            while (!queue.IsEmpty)
            {
                queue.TryPeek(out var delayedCall);
                if (delayedCall.CallAt > now)
                    break;

                await NotificationFactory.NotifyAboutHelpAsync(delayedCall.Call);
                sent += 1;

                //remove
                queue.TryDequeue(out _);
            }

            return sent;
        }
    }
}
