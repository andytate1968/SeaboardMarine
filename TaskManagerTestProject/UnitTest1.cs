using Models;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using System;

namespace TaskManagerTestProject
{

    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public async Task TestMethod1Async()
        {
            var baseurl = "https://localhost:7149";
            var t = new TaskDto();
            t.Title = "unittest1";
            t.Description = "Unit Test 1";
            t.DueDate = DateTime.Now.AddDays(5);
            t.IsCompleted = false;
            var jsonContent = JsonConvert.SerializeObject(t);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var client = new HttpClient { BaseAddress = new Uri(baseurl) };
            var response = await client.PutAsync("/TaskManager/Create", content);
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                var task = JsonConvert.DeserializeObject<TaskDto>(resp);
                
                Assert.IsNotNull(task);
            }
            else
            {
                Assert.Fail("Unable to add task");
            }
        }

        [TestMethod]
        public async Task TestMethod2Async()
        {
            var id = await GetIdOfUnitTest();

            Assert.IsNotNull(id);

            var baseurl = "https://localhost:7149";
            var client = new HttpClient { BaseAddress = new Uri(baseurl) };

            var response = await client.GetAsync("/TaskManager/Get?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                var task = JsonConvert.DeserializeObject<TaskDto>(resp);
                if (task != null)
                {                
                    Assert.AreEqual(id, task.Id);                 
                }
                Assert.IsNotNull(task);
            }
            else
            {
                Assert.Fail("Unable to get task");
            }
        }

        [TestMethod]
        public async Task TestMethod3Async()
        {
            var id = await GetIdOfUnitTest();

            Assert.IsNotNull(id);
            var baseurl = "https://localhost:7149";
            var client = new HttpClient { BaseAddress = new Uri(baseurl) };
            var response = await client.GetAsync("/TaskManager/Get?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                var task = JsonConvert.DeserializeObject<TaskDto>(resp);
                task.Title = "Updateunittest";
                task.Description = "Updated Unit Test";
                task.IsCompleted = true;
                var jsonContent = JsonConvert.SerializeObject(task);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var responseu = await client.PutAsync("/TaskManager/Create", content);
                if (responseu.IsSuccessStatusCode)
                {
                    var respu = await responseu.Content.ReadAsStringAsync();
                    var tasku = JsonConvert.DeserializeObject<TaskDto>(resp);
                    Assert.AreEqual(tasku.Title, task.Title);
                }
            }
            else
            {
                Assert.Fail("Unable to update task");
            }
        }

        [TestMethod]
        public async Task TestMethod4Async()
        {            
            var baseurl = "https://localhost:7149";
            var client = new HttpClient { BaseAddress = new Uri(baseurl) };
            var response = await client.GetAsync("/TaskManager/GetAll");
            if (response.IsSuccessStatusCode)
            {                
                var content = await response.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<IEnumerable<TaskDto>>(content);
                if (tasks != null)
                {
                    Assert.IsTrue(tasks.Any());
                }
                else
                {
                    Assert.Fail("Unable to Get All Tasks");
                }
            }
            else
            {
                Assert.Fail("Unable to update task");
            }
        }
        [TestMethod]
        public async Task TestMethod5Async()
        {
            Thread.Sleep(5000);
            var id = await GetIdOfUnitTest();

            Assert.IsNotNull(id);
            var baseurl = "https://localhost:7149";
            var client = new HttpClient { BaseAddress = new Uri(baseurl) };
            var response = await client.DeleteAsync("/TaskManager/Delete?id=" + id);
            if (response.IsSuccessStatusCode)
            {

                var resp = await response.Content.ReadAsStringAsync();
                var count = JsonConvert.DeserializeObject<int>(resp);
                Assert.AreEqual(1, count);
            }
            else
            {
                Assert.Fail("Unable to delete task");
            }
        }

        private async Task<int> GetIdOfUnitTest()
        {
            var baseurl = "https://localhost:7149";
            var client = new HttpClient { BaseAddress = new Uri(baseurl) };
            var response = await client.GetAsync("/TaskManager/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<IEnumerable<TaskDto>>(content);
                
                var t = tasks.Where(u=> u.Title.Contains("unittest")).Last();
                return t.Id;
            }
            return 0;
        }
    }
}