using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ceras;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace QueueClient
{
    public class Client : IDisposable
    {
        private CerasSerializer _serializer;
        private IConnection _connection;
        private IModel _channel;

        private Queue<Tuple<string, object>> queue = new Queue<Tuple<string, object>>();
        public Client(string hostName)
        {
            _serializer = new CerasSerializer();

            var factory = new ConnectionFactory() { HostName = hostName };
            factory.AutomaticRecoveryEnabled = true;
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            SendQueueAsync();
        }

        private async Task SendQueueAsync()
        {
            while (true)
            {
                await Task.Delay(10000).ConfigureAwait(false);

                if (!queue.Any()) continue;
                while (queue.Any())
                {
                    var item = queue.Dequeue();
                    TryPublish(item.Item1, item.Item2);
                }
            }
        }

        public bool TryPublish<T>(string queueName, T message)
        {
            try
            {
                _channel.QueueDeclare(queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                byte[] body;
                lock (_serializer)
                {
                    body = _serializer.Serialize(message);
                }

                _channel.BasicPublish(exchange: "",
                    routingKey: queueName,
                    basicProperties: null,
                    body: body);

                return true;
            }
            catch
            {
                lock (queue)
                {
                    queue.Enqueue(new Tuple<string, object>(queueName, message));
                }

                return false;
            }
        }

        public void Subscribe<T>(string queueName, Action<T> action)
        {
            _channel.QueueDeclare(queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();

                var message = _serializer.Deserialize<T>(body);

                action.Invoke(message);
            };

            _channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Dispose();
        }
    }
}
