
using System.ComponentModel.DataAnnotations;
using Taskman.Common;

namespace Taskman.Data.Models
{
    /// <remarks>
    /// Using name <see cref="TaskEntity"/> so that it won't be confused with <see cref="=Task"/>. Could use aliases, but that's just easier.
    /// </remarks>
    public class TaskEntity
    {
        [Key]
        public int ID { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public TaskEntityStatus Status { get; set; } = TaskEntityStatus.NotStarted;

        public string? AssignedTo { get; set; }
    }
}
