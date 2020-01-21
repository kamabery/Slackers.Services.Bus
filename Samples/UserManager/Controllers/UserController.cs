using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Slackers.Services.Repository;
using UserManager.Models;

namespace UserManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            _logger.LogInformation("Getting User");
            return await _repository.Get<User>();
        }

        [HttpPost]
        public async Task<ActionResult> Post(User user)
        {
            user.Id = Guid.NewGuid();
            _logger.LogInformation("Added User");
            await _repository.Post(user);
            return Ok(user.Id);
        }

    }
}
