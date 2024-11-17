using Microsoft.EntityFrameworkCore;
using Taskman.Data.Models;

namespace Taskman.Data
{
    public abstract class TaskDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>();
        }
    }
}
