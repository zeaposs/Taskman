using Microsoft.EntityFrameworkCore;
using Taskman.Data.Models;


namespace Taskman.Data.Sqlite
{
    public class TaskDbContextSqlite(DbContextOptions<TaskDbContextSqlite> options) : TaskDbContext(options)
    {
    }
}
