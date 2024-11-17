using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Taskman.MassTransit.RabbitMq
{
    public static class DependencyInjectionRegistrationExtensions
    {
        public static IServiceCollection AddServiceHub(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator>? configure = null, Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? configureRabbitMq = null)
        {
            // Add MassTransit. Call configuration action to provide consumers and other application specific configuration settings.
            services.AddMassTransit(x =>
            {
                configure?.Invoke(x);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);

                    configureRabbitMq?.Invoke(context, cfg);
                });
            });

            // Configure RabbitMQ transport.
            services.Configure<RabbitMQConfiguration>(configuration.GetRabbitMQConfiguration());
            services.AddOptions<RabbitMqTransportOptions>().Configure<IOptions<RabbitMQConfiguration>>((rmqOptions, rmqConfigOption) =>
            {
                if (rmqConfigOption.Value == null)
                {
                    throw new Exception("Should not happen - Missing RabbitMQ configuration.");
                }

                var config = rmqConfigOption.Value;
                config.EnsureValid();

                rmqOptions.Host = config.Host;
                rmqOptions.VHost = config.VHost;
                rmqOptions.User = config.Username;
                rmqOptions.Pass = config.Password;
            });

            return services;
        }
    }
}
