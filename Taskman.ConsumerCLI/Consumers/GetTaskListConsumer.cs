using MassTransit;
using Taskman.ConsumerCLI.Services;
using Taskman.Messages;
using System.Linq;
using MapsterMapper;
using Mapster;
using Taskman.Common.Models;

namespace Taskman.ConsumerCLI.Consumers
{
    internal class GetTaskListConsumer(ITaskService taskService, IMapper mapper) : IConsumer<GetTaskList>
    {
        public async Task Consume(ConsumeContext<GetTaskList> context)
        {
            await context.RespondAsync(mapper.Map<IEnumerable<TaskDto>>(await taskService.GetAllTasks()).ToArray());
        }
    }
}
