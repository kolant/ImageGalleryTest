using ImageGalleryTest.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ImageGalleryTest.Services
{
    public class TimedPictureSeedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<TimedPictureSeedService> _logger;
        private readonly IOptions<AppSettings> _appSettings;
        private Timer _timer;

        public TimedPictureSeedService(
            ILogger<TimedPictureSeedService> logger,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Picture Seed Service running.");

            _timer = new Timer(DoWorkAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(_appSettings.Value.PictureCacheStoringTimeInSeconds));

            return Task.CompletedTask;
        }

        private async void DoWorkAsync(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                "Timed Picture Seed Service is working. Count: {Count}", count);

            try
            {
                await "https://localhost:5001/api/images/load".GetAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Wasn't able to load pictures from api: {ex.Message}");
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Picture Seed Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
