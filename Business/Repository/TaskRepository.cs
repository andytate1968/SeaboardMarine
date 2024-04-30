using AutoMapper;
using Business.Interfaces;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

        public TaskRepository(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<TaskDto?> Create(TaskDto objTask)
        {
            try
            {
                var obj = _mapper.Map<TaskDto, DataAccess.Data.Task>(objTask);
                var addedObj = _db.Tasks.Add(obj);
                await _db.SaveChangesAsync();

                return _mapper.Map<DataAccess.Data.Task, TaskDto>(addedObj.Entity);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TaskDto?> Update(TaskDto objTask)
        {
            try
            {
                var objFromDb = await _db.Tasks.FirstOrDefaultAsync(u => u.Id == objTask.Id);
                if (objFromDb != null)
                {
                    objFromDb .DueDate= objTask.DueDate;
                    objFromDb.Description = objTask.Description;
                    objFromDb.Title = objTask.Title;
                    objFromDb.IsCompleted = objTask.IsCompleted;
                    
                    _db.Tasks.Update(objFromDb);
                    await _db.SaveChangesAsync();
                    if (objTask.TaskTags != null)
                    {
                        await UpdateTags(objTask.TaskTags, objTask.Id);
                    }
                    
                    
                    
                    return await Get(objTask.Id);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async System.Threading.Tasks.Task UpdateTags(List<TaskTagDto> taskTags, int taskId)
        {
            foreach (var tag in taskTags)
            {
                if (tag.IsNew && tag.IsDelete == false)
                {
                    var obj = _mapper.Map<TaskTagDto, DataAccess.Data.TaskTag>(tag);
                    obj.TaskId = taskId;
                    _db.TaskTags.Add(obj);
                    await _db.SaveChangesAsync();
                }
                else if (tag.IsDelete)
                {
                    var tagd = _db.TaskTags.First(t => t.Id == tag.Id);
                    
                    _db.TaskTags.Remove(tagd);
                    await _db.SaveChangesAsync();
                }
            }
        }

        public async Task<int?> Delete(int id)
        {
            try
            {
                var obj = await _db.Tasks.Include(task => task.TaskTags).FirstOrDefaultAsync(u => u.Id == id);
                if (obj != null)
                {
                    _db.Tasks.Remove(obj);
                    return await _db.SaveChangesAsync();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TaskDto?> Get(int id)
        {
            try
            {
                var obj = await _db.Tasks.Include(task => task.TaskTags).FirstOrDefaultAsync(u => u.Id == id);
                if (obj != null)
                {
                    return _mapper.Map<DataAccess.Data.Task, TaskDto>(obj);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<IEnumerable<TaskDto>?> GetAll()
        {
            try
            {
                var tasks = _db.Tasks.Include(task => task.TaskTags);
                return _mapper.Map<IEnumerable<DataAccess.Data.Task>, IEnumerable<TaskDto>>(tasks);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
