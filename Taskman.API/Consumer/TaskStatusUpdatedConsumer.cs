using MassTransit;
using Taskman.Messages;

namespace Taskman.API.Consumer
{
    internal class TaskStatusUpdatedConsumer(ILogger<TaskStatusUpdatedConsumer> logger) : IConsumer<TaskStatusUpdated>
    {
        public Task Consume(ConsumeContext<TaskStatusUpdated> context)
        {
            logger.LogInformation($"Task {context.Message.Id} status changed to {context.Message.Status}.");

            return Task.CompletedTask;
        }
    }
}
