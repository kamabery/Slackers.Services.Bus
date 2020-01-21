using System.Threading.Tasks;
using Contracts;
using Slackers.Services.Bus;
using Slackers.Services.Repository;
using UserManager.Models;

namespace UserManager.Handlers
{
    public class TaskAddedHandler : IEventHandler<TaskAdded>
    {
        private readonly IRepository _repository;

        public TaskAddedHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleEvent(TaskAdded eventMessage)
        {
            var user = await _repository.Get<User>(eventMessage.UserId);
            if (user != null)
            {
                user.TaskCount++;
                await _repository.Put(user);
            }
        }
    }
}