using Microsoft.Extensions.Hosting;

namespace Taskman.ConsumerCLI
{
    using Taskman.Data;
    using Taskman.MassTransit.RabbitMq;
    using Taskman.Messages;
    using Taskman.Data.Sqlite;
    using Taskman.ConsumerCLI.Consumers;
    using Microsoft.Extensions.DependencyInjection;
    using Taskman.ConsumerCLI.Services.Implementations;
    using Taskman.ConsumerCLI.Services;
    using Mapster;

    internal class Program
    {
        public static string Identifier = DateTime.Now.ToString();

        static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMapster();

                    services.AddServiceHub(hostContext.Configuration, busConfig =>
                    {
                        busConfig.AddConsumer<AddTaskConsumer>();
                        busConfig.AddConsumer<UpdateTaskStatusConsumer>();
                        busConfig.AddConsumer<GetTaskListConsumer>();
                    });

                    services.AddDataServicesSqlite(hostContext.Configuration);

                    services.AddTransient<ITaskService, TaskService>();
                });

            var app = builder.UseConsoleLifetime().Build();
            app.Services.InitializeDatabase();

            await app.RunAsync();
        }
    }
}
