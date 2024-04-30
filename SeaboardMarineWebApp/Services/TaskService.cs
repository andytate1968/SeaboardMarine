using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManagerAPI.Services
{
    public class TaskService: ITaskService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServerUrl;


        public TaskService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<int> Delete(int id)
        {
           
            var response = await _httpClient.DeleteAsync("/TaskManager/Delete?id=" + id);
            if (response.IsSuccessStatusCode)
            {

                var resp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<int>(resp);
            }

            return -1;
        }

        public async Task<TaskDto> Get(int id)
        {
            var response = await _httpClient.GetAsync("/TaskManager/Get?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var task = JsonConvert.DeserializeObject<TaskDto>(content);

                return task;
            }

            return new TaskDto();
        }

        public async Task<IEnumerable<TaskDto>> GetAll()
        {
            
            var response = await _httpClient.GetAsync("/TaskManager/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<IEnumerable<TaskDto>>(content);

                return tasks;
            }

            return new List<TaskDto>();
        }

        public async Task<TaskDto> Update(TaskDto data)
        {
            var jsonContent = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/TaskManager/Update", content);
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<TaskDto>(resp);

                return tasks;
            }
            return new TaskDto();
        }

        public async Task<TaskDto> Create(TaskDto data)
        {
            var jsonContent = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/TaskManager/Update", content);
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<TaskDto>(resp);

                return tasks;
            }
            return new TaskDto();
        }
    }
}
