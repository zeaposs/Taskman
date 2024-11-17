using Microsoft.EntityFrameworkCore;
using Taskman.Common;
using Taskman.Data;
using Taskman.Data.Models;

namespace Taskman.ConsumerCLI.Services.Implementations
{
    internal class TaskService(TaskDbContext taskDbContext) : ITaskService
    {
        public async Task<TaskEntity> AddTask(TaskEntity taskEntity)
        {
            await taskDbContext.AddAsync(taskEntity);
            await taskDbContext.SaveChangesAsync();

            return taskEntity;
        }

        public Task<IEnumerable<TaskEntity>> GetAllTasks()
        {
            return Task.FromResult(taskDbContext.Set<TaskEntity>() as IEnumerable<TaskEntity>);
        }

        public async Task UpdateTaskStatus(int taskId, TaskEntityStatus status)
        {
            var task = await taskDbContext.FindAsync<TaskEntity>(taskId);
            if (task != null)
            {
                if (task.Status == status)
                {
                    throw new TaskServiceException($"Task {task.ID} already is set to the state {status}.");
                }

                task.Status = status;
                await taskDbContext.SaveChangesAsync();
            }
            else
            {
                throw new TaskServiceException($"Task {taskId} doesn't exist.");
            }
        }
    }
}
