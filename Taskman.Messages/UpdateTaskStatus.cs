using Taskman.Common;

namespace Taskman.Messages
{
    public record UpdateTaskStatus
    {
        public int Id { get; set; }

        public TaskEntityStatus Status { get; set; }
    }
}
