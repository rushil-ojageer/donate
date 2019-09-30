using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Donate.FundService.Data;
using Donate.Shared.Data.Extensions;
using Donate.Shared.Eventing;
using Donate.Shared.Eventing.TransactionEvents;
using Donate.Shared.IntegrationQueue;
using Donate.Shared.IntegrationQueue.Models;
using Donate.Shared.Logging;
using Donate.Shared.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Donate.FundService.TransactionFeed.Services
{
    public class TransactionGeneratorService : IHostedService, IDisposable
    {
        private readonly IServiceScope _serviceScope;
        private readonly IIntegrationEventQueue _transactionFeedService;
        private readonly FundContext _db;
        private readonly IDictionary<Guid, string> _merchants;
        private Timer _timer;
        private IApiLogger _logger;

        public TransactionGeneratorService(IServiceProvider serviceProvider)
        {
            _serviceScope = serviceProvider.CreateScope();
            var loggerFactory = _serviceScope.ServiceProvider.GetServiceOrThrow<ILoggerFactory>();
            _logger = loggerFactory.GetLogger<TransactionGeneratorService>();
            _transactionFeedService = _serviceScope.ServiceProvider.GetService(typeof(IIntegrationEventQueue)) as IIntegrationEventQueue;
            _db = _serviceScope.ServiceProvider.GetService(typeof(FundContext)) as FundContext;
            _merchants = new Dictionary<Guid, string>()
            {
                { Guid.NewGuid(), "Merchant 1" },
                { Guid.NewGuid(), "Merchant 2" },
                { Guid.NewGuid(), "Merchant 3" },
                { Guid.NewGuid(), "Merchant 4" },
                { Guid.NewGuid(), "Merchant 5" },
                { Guid.NewGuid(), "Merchant 6" },
                { Guid.NewGuid(), "Merchant 7" },
                { Guid.NewGuid(), "Merchant 8" },
                { Guid.NewGuid(), "Merchant 9" },
                { Guid.NewGuid(), "Merchant 10" }
            };
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.Info("Starting background services...");
            SendTransaction(null);
            _timer = new Timer(SendTransaction, null, TimeSpan.Zero, TimeSpan.FromMinutes(60));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.Info("Stopping background services...");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void SendTransaction(object obj)
        {
            _logger.Info("Sending transaction...");

            var donorTransactionSources = _db.DonorTransactionSources
                .FilterDeletedItems()
                .ToList();

            var random = new Random(Guid.NewGuid().GetHashCode());
            foreach (var ts in donorTransactionSources)
            {
                var merchantIndex = random.Next(0, 10);
                var merchant = _merchants.ElementAt(merchantIndex);

                var transactionEvent = new TransactionEvent
                {
                    Amount = random.Next(1, 10000),
                    Currency = "ZAR",
                    MerchantIdentifier = merchant.Key.ToString(),
                    MerchantName = merchant.Value,
                    TransactionDateTimeUtc = DateTime.UtcNow,
                    TransactionIdentifier = Guid.NewGuid().ToString(),
                    TransactionSourceIdentifier = ts.Identifier
                };

                var integrationEvent = new IntegrationEvent<TransactionEvent>
                {
                    Body = transactionEvent,
                    DateTimeUtc = DateTime.UtcNow,
                    Event = EventNames.NewTransaction.ToString(),
                    Service = ServiceNames.TransactionFeed.ToString()
                };

                _transactionFeedService.Post(integrationEvent);
            }
        }
        public void Dispose()
        {
            _timer?.Dispose();
            _serviceScope?.Dispose();
        }
    }
}
