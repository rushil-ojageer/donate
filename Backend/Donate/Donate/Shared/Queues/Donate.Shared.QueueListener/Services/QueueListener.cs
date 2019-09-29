using System;
using System.Threading;
using System.Threading.Tasks;
using Donate.Shared.IntegrationQueue.Models;
using Donate.Shared.Logging;
using Donate.Shared.QueueListener.EventHandler;
using Donate.Shared.QueueListener.Exceptions;
using Donate.Shared.Queues;
using Donate.Shared.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Donate.Shared.QueueListener.Services
{
    public class QueueListener : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IQueueSettings _settings;
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;
        private readonly IApiLogger _logger;

        public QueueListener(ILoggerFactory loggerFactory,
            IQueueChannelManager queueChannelManager,
            IQueueSettingsResolver queueSettingsResolver,
            IQueueFactory queueFactory,
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = loggerFactory.GetLogger<QueueListener>();
            _settings = queueSettingsResolver.GetSettings(nameof(QueueListener));
            _channel = queueChannelManager.GetChannel();
            _consumer = new EventingBasicConsumer(_channel);
            queueFactory.SetupQueue(_settings);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            _consumer.Received += async (s,e) => await ConsumerOnReceived(s,e);
            _consumer.Shutdown += OnConsumerShutdown;
            _consumer.Registered += OnConsumerRegistered;
            _consumer.Unregistered += OnConsumerUnregistered;
            _consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(_settings.QueueName, false, _consumer);
            return Task.CompletedTask;
        }

        private async Task ConsumerOnReceived(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                CheckEventRequeueLimit(e);
                var content = System.Text.Encoding.UTF8.GetString(e.Body);
                TryGetMessageType(e, out var messageType);
                await HandleMessage(content, messageType);
                _channel.BasicAck(e.DeliveryTag, false);
            }
            catch (EventRequeueLimitReachedException ex)
            {
                _logger.Error(ex.Message, ex);
                _channel.BasicReject(e.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.Error($"Unable to process event '{e.DeliveryTag}': {ex.Message}", ex);
                _channel.BasicReject(e.DeliveryTag, true);
            }
        }

        private void TryGetMessageType(BasicDeliverEventArgs e, out ServiceEvent serviceEvent)
        {
            try
            {
                var messageType = e.BasicProperties.MessageId;

                if (string.IsNullOrEmpty(messageType))
                {
                    throw new UnsupportedMessageException(e.DeliveryTag);
                }

                serviceEvent = JsonConvert.DeserializeObject<ServiceEvent>(messageType);
            }
            catch (Exception ex)
            {
                _logger.Error($"Unable to get message type: {ex.Message}", ex);
                throw new UnsupportedMessageException(e.DeliveryTag);
            }
        }

        private void CheckEventRequeueLimit(BasicDeliverEventArgs e)
        {
            //const int limit = 10;

            //if (e.DeliveryTag < limit)
            //{
            //    return;
            //}

            //throw new EventRequeueLimitReachedException(e.DeliveryTag, limit);
        }

        private async Task HandleMessage(string content, ServiceEvent messageType)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var eventHandlerCollection = scope.ServiceProvider.GetServiceOrThrow<IEventHandlerCollection>();
                var eventHandlers = eventHandlerCollection.Get(messageType.Service, messageType.Event);

                foreach (var eventHandler in eventHandlers)
                {
                    await eventHandler.HandleEvent(content);
                }
            }
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            base.Dispose();
        }
    }
}
