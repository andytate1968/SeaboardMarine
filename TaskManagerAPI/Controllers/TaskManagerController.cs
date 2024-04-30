using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TaskManagerController : ControllerBase
    {

        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskManagerController> _logger;

        public TaskManagerController(ITaskRepository taskRepository,ILogger<TaskManagerController> logger)
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }

        [HttpGet(Name = "GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _taskRepository.GetAll();
            if (result == null)
            {
                return BadRequest("Tasks were unable to be retrieved");
            }
            return Ok(result);           
        }

        [HttpGet(Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _taskRepository.Get(id);
            if (result == null)
            {
                return BadRequest("Task was unable to be retrieved");
            }
            return Ok(result);
        }

        [HttpPut]
        [ActionName("Create")]
        public async Task<IActionResult> Create([FromBody] TaskDto taskDto)
        {
            
            var result = await _taskRepository.Create(taskDto);
            if (result == null)
            {
                return BadRequest("Task was unable to be created");
            }
            return Ok(result);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> Update([FromBody] TaskDto taskDto)
        {

            var result = await _taskRepository.Update(taskDto);
            if (result == null)
            {
                return BadRequest("Task was unable to be updated");
            }
            return Ok(result);
        }

        [HttpDelete]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _taskRepository.Delete(id);
            if (result == null)
            {
                return BadRequest("Task was unable to be deleted");
            }
            return Ok(result);
        }
    }
}
