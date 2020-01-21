using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Slackers.Services.Repository;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkTaskController : ControllerBase
    {
        private readonly ILogger<WorkTaskController> _logger;
        private readonly IRepository _repository;
        private readonly IBus _bus;

        public WorkTaskController(ILogger<WorkTaskController> logger, IRepository repository, IBus bus)
        {
            _logger = logger;
            _repository = repository;
            _bus = bus;
        }

        [HttpGet]
        public async Task<IEnumerable<WorkTask>> Get()
        {
            return await _repository.Get<WorkTask>();
        }

        [HttpPost]
        public async Task<ActionResult> Post(WorkTask workTask)
        {
            _logger.LogInformation("Add Task");
            await _repository.Post(workTask);
            await _bus.Publish(new TaskAdded {ProjectId = workTask.ProjectId, UserId = workTask.UserId, TaskId = workTask.Id});
            return Ok(workTask.Id);
        }
    }
}
