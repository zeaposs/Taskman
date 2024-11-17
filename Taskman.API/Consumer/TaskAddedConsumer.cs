using MassTransit;
using Taskman.Messages;

namespace Taskman.API.Consumer
{
    internal class TaskAddedConsumer(ILogger<TaskAddedConsumer> logger) : IConsumer<TaskAdded>
    {
        public Task Consume(ConsumeContext<TaskAdded> context)
        {
            logger.LogInformation($"Task {context.Message.Id} has been added.");

            return Task.CompletedTask;
        }
    }
}
