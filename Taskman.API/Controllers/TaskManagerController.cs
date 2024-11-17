using MapsterMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Taskman.Common;
using Taskman.Common.Models;
using Taskman.Messages;

namespace Taskman.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TaskManagerController(IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetList(IRequestClient<GetTaskList> requestClient)
        {
            var result = await requestClient.Create(new GetTaskList()).GetResponse<TaskDto[]>();
            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task AddTask(IPublishEndpoint publishEndpoint, NewTaskDto taskDto)
        {
            await publishEndpoint.Publish(mapper.Map<AddTask>(taskDto));
        }

        [HttpPost]
        public async Task UpdateTaskStatus(IPublishEndpoint publishEndpoint, int taskId, TaskEntityStatus newStatus)
        {
            await publishEndpoint.Publish(new UpdateTaskStatus() { Id = taskId, Status = newStatus });
        }
    }
}
