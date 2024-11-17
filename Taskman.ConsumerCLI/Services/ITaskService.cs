using Taskman.Common;
using Taskman.Data.Models;

namespace Taskman.ConsumerCLI.Services
{
    internal interface ITaskService
    {
        Task<TaskEntity> AddTask(TaskEntity taskEntity);
        Task<IEnumerable<TaskEntity>> GetAllTasks();
        Task UpdateTaskStatus(int taskId, TaskEntityStatus status);
    }
}
