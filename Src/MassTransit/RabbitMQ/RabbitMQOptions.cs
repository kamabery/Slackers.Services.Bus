namespace Slackers.Services.Bus.MassTran.RabbitMQ
{
    public class RabbitMQOptions
    {
        public string VirtualHost { get; set; }

        public string Server { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string HostName { get; set; }
    }
}