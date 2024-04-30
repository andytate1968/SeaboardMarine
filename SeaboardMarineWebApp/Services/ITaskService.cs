using Models;

namespace TaskManagerAPI.Services
{
    public interface ITaskService
    {
        public Task<IEnumerable<TaskDto>> GetAll();
        public Task<TaskDto> Get(int id);

        public Task<TaskDto> Create(TaskDto dto);
        public Task<TaskDto> Update(TaskDto dto);

        public Task<int> Delete(int id);
    }
}
