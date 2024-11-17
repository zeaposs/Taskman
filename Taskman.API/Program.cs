
using Mapster;
using MassTransit;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using Taskman.API.Consumer;


namespace Taskman.API
{
    public static class Extensions
    {
        public static IConfiguration GetRabbitMQConfiguration(this IConfiguration configuration)
            => configuration.GetSection("RabbitMQ") ?? throw new Exception("Missing RabbitMQ configuration.");
    }

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

    public record TestMessage
    {
        public required string ImportantValue { get; init; }
    }

    public class TestMessageConsumer(ILogger<TestMessageConsumer> logger) : IConsumer<TestMessage>
    {
        public async Task Consume(ConsumeContext<TestMessage> context)
        {
            logger.LogInformation($"Received TestMessage {context.Message.ImportantValue}");
            await Task.CompletedTask;
        }
    }

    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure RabbitMQ transport.
            builder.Services.Configure<RabbitMQConfiguration>(builder.Configuration.GetRabbitMQConfiguration());
            builder.Services.AddOptions<RabbitMqTransportOptions>().Configure<IOptions<RabbitMQConfiguration>>((rmqOptions, rmqConfigOption) =>
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

            // Add services to the container.

            builder.Services.AddMapster();
            builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMassTransit(busConfig =>
            {
                // Events - if we want events to be fan-out, we should provide unique InstanceId for each bus. Example below:
                //busConfig.AddConsumer<TaskAddedConsumer>().Endpoint(endpoint => endpoint.InstanceId = Guid.NewGuid().ToString());
                busConfig.AddConsumer<TaskAddedConsumer>();
                busConfig.AddConsumer<TaskStatusUpdatedConsumer>();

                busConfig.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
