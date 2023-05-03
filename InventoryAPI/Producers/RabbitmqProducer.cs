using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace InventoryAPI.Producers
{
    public class RabbitmqProducer : IRabbitmqProducer
    {
        private ConnectionFactory connectionFactory;
        private IConfiguration configuration;
        private IConnection connection;
        private IModel channel;

        public RabbitmqProducer(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            //this.configuration = new ConfigurationBuilder()
                //           .AddJsonFile("appsettings.json")
                 //          .Build();
            connectionFactory = new ConnectionFactory();
            connectionFactory.UserName = configuration["amqp:username"];
            connectionFactory.Password = configuration["amqp:password"];
            connectionFactory.VirtualHost = configuration["amqp:virtualhost"];
            connectionFactory.HostName = configuration["amqp:hostname"];
            connectionFactory.Port = AmqpTcpEndpoint.UseDefaultPort;
            //create the connection
             connection = connectionFactory.CreateConnection();
            //create the channel
            channel = connection.CreateModel();
            channel.QueueDeclare("CatalogQueue", false, true, true);
        }


        public void SendMessage<T>(T message)
        {
            
            var json=JsonConvert.SerializeObject(message);
            var body=Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange:"",routingKey:"CatalogQueue",body:body);
           // channel.Close();


        }
    }
}
