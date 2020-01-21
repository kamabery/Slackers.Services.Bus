using System.Threading.Tasks;
using Contracts;
using Microsoft.Extensions.Logging;
using ProjectManager.Models;
using Slackers.Services.Bus;
using Slackers.Services.Repository;

namespace ProjectManager.Handlers
{
    public class TaskAddedHandler : IEventHandler<TaskAdded>
    {
        private readonly ILogger<TaskAddedHandler> _logger;
        private readonly IRepository _repository;

        public TaskAddedHandler(ILogger<TaskAddedHandler> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task HandleEvent(TaskAdded eventMessage)
        {
            var project = await _repository.Get<Project>(p => p.Id == eventMessage.ProjectId, true);
            if (project != null)
            {
                project.TaskCount++;
            }

            await _repository.Put(project);
        }
    }
}