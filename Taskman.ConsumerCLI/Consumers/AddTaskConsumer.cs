using MapsterMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Taskman.ConsumerCLI.Services;
using Taskman.Data.Models;
using Taskman.Messages;

namespace Taskman.ConsumerCLI.Consumers
{
    internal class AddTaskConsumer(ILogger<AddTaskConsumer> logger, IMapper mapper, ITaskService taskService) : IConsumer<AddTask>
    {
        public async Task Consume(ConsumeContext<AddTask> context)
        {
            logger.LogInformation($"Received TaskDTO {context.Message.Name}");

            var newTask = await taskService.AddTask(mapper.Map<TaskEntity>(context.Message));

            await context.Publish<TaskAdded>(new { Id = newTask.ID });
        }
    }
}
