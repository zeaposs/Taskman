namespace Taskman.MassTransit.RabbitMq
{
    public class RabbitMQConfiguration
    {
        public required string Host { get; set; }
        public required string VHost { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }

        public RabbitMQConfiguration EnsureValid()
        {
            if (string.IsNullOrEmpty(Host) || string.IsNullOrEmpty(VHost) || string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                throw new Exception("Check RabbitMQ configuration - key values missing.");
            }
            return this;
        }
    }
}
