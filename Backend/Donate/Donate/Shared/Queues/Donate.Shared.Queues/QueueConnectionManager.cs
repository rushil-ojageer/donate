using System;
using System.Threading.Tasks;
using Donate.Shared.Logging;
using Donate.Shared.Queues.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Donate.Shared.Queues
{
    public class QueueConnectionManager : IQueueConnectionManager
    {
        private readonly IApiLogger _logger;
        private readonly IOptions<QueueConnectionSettings> _queueConnectionSettings;
        private IConnection _connection;

        public QueueConnectionManager(IOptions<QueueConnectionSettings> queueConnectionSettings,
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLogger<QueueConnectionManager>();
            _queueConnectionSettings = queueConnectionSettings;
            Connect();
        }

        public IConnection GetConnection()
        {
            return _connection;
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
        }

        private void Connect()
        {
            _logger.Info("Attempting initial connection to RabbitMQ...");

            var connectionRetries = 0;
            var connected = ConnectToQueue();

            if (!connected)
            {
                _logger.Info($"Initial connection to RabbitMQ failed. Beginning retries [MAX = {_queueConnectionSettings.Value.MaxConnectionRetries}]...");
            }

            while (CanRetry(connected, connectionRetries))
            {
                connectionRetries++;
                _logger.Info($"Attempting {connectionRetries} connection to RabbitMQ...");

                connected = ConnectToQueue();

                if (CanRetry(connected, connectionRetries))
                {
                    _logger.Info($"Connection attempt {connectionRetries} to RabbitMQ failed. Will retry in {_queueConnectionSettings.Value.ConnectionRetryWaitSeconds} seconds...");
                    var task = Task.Delay(new TimeSpan(0, 0, 0, _queueConnectionSettings.Value.ConnectionRetryWaitSeconds));
                    task.Wait();
                }
                else
                {
                    _logger.Info($"Connection attempt {connectionRetries} to RabbitMQ failed. Max retries of {_queueConnectionSettings.Value.MaxConnectionRetries} reached. Will not retry.");
                }
            }

            if (!connected)
            {
                _logger.Info("Application could not successfully connected to RabbitMQ.");
                throw new Exception("Unable to connect to RabbitMQ");
            }

            _logger.Info("Application has successfully connected to RabbitMQ.");
        }

        private bool CanRetry(bool connected, int connectionRetries)
        {
            return !connected && connectionRetries < _queueConnectionSettings.Value.MaxConnectionRetries;
        }

        private bool ConnectToQueue()
        {
            try
            {
                _logger.Info($"HOST={_queueConnectionSettings.Value.HostName}");
                _logger.Info($"PORT={_queueConnectionSettings.Value.Port}");
                _logger.Info($"USERNAME={_queueConnectionSettings.Value.Username}");
                _logger.Info($"PASSWORD={_queueConnectionSettings.Value.Password}");
                
                var factory = new ConnectionFactory
                {
                    HostName = _queueConnectionSettings.Value.HostName,
                    Port = _queueConnectionSettings.Value.Port,
                    UserName = _queueConnectionSettings.Value.Username,
                    Password = _queueConnectionSettings.Value.Password
                };
                _connection = factory.CreateConnection();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"Unable to connect to Response Queue: {ex.Message}", ex);
                return false;
            }
        }
    }
}