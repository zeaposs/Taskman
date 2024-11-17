using Microsoft.Extensions.Configuration;

namespace Taskman.MassTransit.RabbitMq
{
    internal static class ConfigurationExtensions
    {
        public static IConfiguration GetRabbitMQConfiguration(this IConfiguration configuration)
            => configuration.GetSection("RabbitMQ") ?? throw new Exception("Missing RabbitMQ configuration.");
    }
}
