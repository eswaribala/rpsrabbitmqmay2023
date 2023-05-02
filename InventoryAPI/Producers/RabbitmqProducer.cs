using RabbitMQ.Client;

namespace InventoryAPI.Producers
{
    public class RabbitmqProducer : IRabbitmqProducer
    {
        private ConnectionFactory connectionFactory;
        private IConfiguration configuration;

        public RabbitmqProducer(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.configuration = new ConfigurationBuilder()
                           .AddJsonFile("appsettings.json")
                           .Build();
            connectionFactory = new ConnectionFactory();
            connectionFactory.UserName = configuration["amqp:username"];
            connectionFactory.Password = configuration["amqp:password"];
            connectionFactory.VirtualHost = configuration["amqp:virtualhost"];
            connectionFactory.HostName = configuration["amqp:hostname"];
            connectionFactory.Port = AmqpTcpEndpoint.UseDefaultPort;

        }


        public void SendMessage<T>(T message)
        {
            throw new NotImplementedException();
        }
    }
}
