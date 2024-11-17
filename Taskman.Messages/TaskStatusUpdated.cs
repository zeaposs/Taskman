using Taskman.Common;

namespace Taskman.Messages
{
    public record TaskStatusUpdated
    {
        public int Id { get; set; }

        public TaskEntityStatus Status { get; set; }
    }
}
