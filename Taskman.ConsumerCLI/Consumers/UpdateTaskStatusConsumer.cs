using MassTransit;
using Microsoft.Extensions.Logging;
using Taskman.ConsumerCLI.Services;
using Taskman.ConsumerCLI.Services.Implementations;
using Taskman.Messages;

namespace Taskman.ConsumerCLI.Consumers
{
    internal class UpdateTaskStatusConsumer(ILogger<UpdateTaskStatusConsumer> logger, ITaskService taskService) : IConsumer<UpdateTaskStatus>
    {
        public async Task Consume(ConsumeContext<UpdateTaskStatus> context)
        {
            logger.LogInformation($"Received UpdateTaskStatus for task {context.Message.Id}");

            try
            {
                await taskService.UpdateTaskStatus(context.Message.Id, context.Message.Status);
                await context.Publish<TaskStatusUpdated>(new { context.Message.Id, context.Message.Status });
            }
            catch (TaskServiceException ex)
            {
                logger.LogWarning(ex.Message);
            }
        }
    }
}
