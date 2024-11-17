using Taskman.Common;

namespace Taskman.Messages
{
    public record AddTask
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public TaskEntityStatus Status { get; set; } = TaskEntityStatus.NotStarted;

        public string? AssignedTo { get; set; }
    }
}
