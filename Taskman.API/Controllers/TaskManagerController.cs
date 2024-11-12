using Microsoft.AspNetCore.Mvc;

namespace Taskman.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskManagerController : ControllerBase
    {
        private readonly ILogger<TaskManagerController> _logger;

        public TaskManagerController(ILogger<TaskManagerController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetInfo")]
        public async Task<string> Get()
        {
            return await Task.FromResult("Ok");
        }
    }
}
