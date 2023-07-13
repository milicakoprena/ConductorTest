using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common
{
    public abstract class Worker
    {
        protected string baseUri { get; set; }
        protected string task { get; set; }
        HttpClient httpClient { get; set; }

        public Worker(string task_)
        {
            this.task = task_;
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://192.168.101.17:8082/api/")
            };
        }

        public async Task Poll()
        {
            while (true)
            {
                var response = await httpClient.GetAsync($"tasks/poll/{task}");
                if(response.StatusCode == System.Net.HttpStatusCode.NoContent) { 
                   // Thread.Sleep(1000);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    PollResult pollResult = JsonConvert.DeserializeObject<PollResult>(content);

                    var output = await Work(pollResult.InputData);

                    var updateTaskStatus = new UpdateTaskStatus();
                    updateTaskStatus.WorkflowInstanceId = pollResult.WorkflowInstanceId;
                    updateTaskStatus.TaskId = pollResult.TaskId;
                    updateTaskStatus.Status = "COMPLETED";
                    updateTaskStatus.OutputData = output;

                    await UpdateTaskStatus(updateTaskStatus);
                }
                await Task.Delay(1000);
            }

        }

        public async Task UpdateTaskStatus(UpdateTaskStatus updateTaskStatus)
        {
            string updateBody = JsonConvert.SerializeObject(updateTaskStatus);
            var content = new StringContent(updateBody, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            var response = await httpClient.PostAsync("tasks", content);
        }

        public abstract Task<JObject> Work(JObject inputData);

    }
}
