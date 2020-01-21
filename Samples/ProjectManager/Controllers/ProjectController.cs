using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManager.Models;
using Slackers.Services.Repository;


namespace ProjectManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {

        private readonly ILogger<ProjectController> _logger;
        private readonly IRepository _repository;

        public ProjectController(ILogger<ProjectController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Project>> Get()
        {
            return await _repository.Get<Project>();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Project project)
        {
            _logger.LogInformation("Adding Project");
            project.Id = Guid.NewGuid();
            await _repository.Post(project);
            return Ok(project.Id);
        }
    }
}
