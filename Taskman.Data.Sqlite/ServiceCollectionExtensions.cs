using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskman.Data.Sqlite
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataServicesSqlite(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<TaskDbContextSqlite2>(options
            //    => options.UseSqlite(
            //        configuration.GetConnectionString("TaskDatabase")
            //    ));
            services.AddDataServicesInternal<TaskDbContextSqlite>(options
                => options.UseSqlite(
                    configuration.GetConnectionString("TaskDatabase")
                )
            );
        }
    }
}
