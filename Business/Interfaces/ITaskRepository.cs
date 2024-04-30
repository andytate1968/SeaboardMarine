using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ITaskRepository
    {
        public Task<TaskDto?> Get(int id);
        public Task<IEnumerable<TaskDto>> GetAll();
        public Task<TaskDto?> Create(TaskDto objDTO);
        public Task<TaskDto?> Update(TaskDto objDTO);
        public Task<int?> Delete(int id);
    }
}
