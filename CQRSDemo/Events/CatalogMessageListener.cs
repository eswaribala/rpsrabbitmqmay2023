using CQRSDemo.Repositories;
using MongoDB.Bson;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System.Diagnostics;

using System.Reflection;
using System.Text;


namespace CQRSDemo.Events
{
    public class CatalogMessageListener
    {
        private CatalogMongoRepository _repository;
        public CatalogMessageListener(CatalogMongoRepository repository)
        {
            _repository = repository;
            Debug.WriteLine("Message Listener.....................");
        }
        public void Start()
        {
            
            Debug.WriteLine("Message Listener.....................");
            ConnectionFactory connectionFactory = new ConnectionFactory
            {
               
                RequestedHeartbeat = 15,                
                AutomaticRecoveryEnabled = true,
               
            };
            connectionFactory.HostName = "localhost";
            connectionFactory.Port = 5672;
            connectionFactory.UserName = "guest";
            connectionFactory.Password = "guest";


            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();
            builder.Build().GetSection("amqp").Bind(connectionFactory);
            connectionFactory.AutomaticRecoveryEnabled = true;
            connectionFactory.NetworkRecoveryInterval = TimeSpan.FromSeconds(15);
            using (IConnection conn = connectionFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    DeclareQueues(channel);
                    var subscriptionCreated = new Subscription(channel, Constants.QUEUE_CART_CREATED, false);
                    var subscriptionUpdated = new Subscription(channel, Constants.QUEUE_CART_UPDATED, false);
                    var subscriptionDeleted = new Subscription(channel, Constants.QUEUE_CART_DELETED, false);
                    while (true)
                    {
                        // Sleeps for 5 sec before trying again
                        Thread.Sleep(5000);
                        new Thread(() =>
                        {
                            ListenCreated(subscriptionCreated);
                        }).Start();
                        new Thread(() =>
                        {
                            ListenUpdated(subscriptionUpdated);
                        }).Start();
                        new Thread(() =>
                        {
                            ListenDeleted(subscriptionDeleted);
                        }).Start();
                    }
                }
            }
        }
        private void ListenDeleted(Subscription subscriptionDeleted)
        {
            BasicDeliverEventArgs eventArgsDeleted = subscriptionDeleted.Next();
            if (eventArgsDeleted != null)
            {
                string messageContent = Encoding.UTF8.GetString(eventArgsDeleted.Body);
                var bsonDocument = BsonDocument.Parse(messageContent);
                CatalogDeletedEvent _deleted = JsonConvert.DeserializeObject<CatalogDeletedEvent>(messageContent);
                _repository.Remove(_deleted.CatalogId);
                subscriptionDeleted.Ack(eventArgsDeleted);
            }
        }
        private void ListenUpdated(Subscription subscriptionUpdated)
        {
            BasicDeliverEventArgs eventArgsUpdated = subscriptionUpdated.Next();
            if (eventArgsUpdated != null)
            {
                string messageContent = Encoding.UTF8.GetString(eventArgsUpdated.Body);
                CatalogUpdatedEvent _updated = JsonConvert.DeserializeObject<CatalogUpdatedEvent>(messageContent);
                _repository.Update(_updated.ToCartEntity(_repository.GetCatalog(_updated.CatalogId)));
                subscriptionUpdated.Ack(eventArgsUpdated);
            }
        }
        private void ListenCreated(Subscription subscriptionCreated)
        {
            Debug.WriteLine("Message Listener.....................");
            BasicDeliverEventArgs eventArgsCreated = subscriptionCreated.Next();
            if (eventArgsCreated != null)
            {
                string messageContent = Encoding.UTF8.GetString(eventArgsCreated.Body);
                CatalogCreatedEvent _created = JsonConvert.DeserializeObject<CatalogCreatedEvent>(messageContent);
                _repository.Create(_created.ToCartEntity());
                subscriptionCreated.Ack(eventArgsCreated);
            }
        }
        private static void DeclareQueues(IModel channel)
        {
            channel.QueueDeclare(
                queue: Constants.QUEUE_CART_CREATED,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
            channel.QueueDeclare(
                queue: Constants.QUEUE_CART_UPDATED,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
            channel.QueueDeclare(
                queue: Constants.QUEUE_CART_DELETED,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }
    }
}
