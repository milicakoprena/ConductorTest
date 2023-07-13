using Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.TaskConfig;

namespace PrepareVideos
{
    internal class PrepareVideosWorker : Worker
    {
        public PrepareVideosWorker(string task_) : base(task_)
        {

        }

        public override Task<JObject> Work(JObject inputData)
        {
            List<TaskConfig> dynamicTasks = new List<TaskConfig>();
            Dictionary<string, DownloadAndProcessing> dynamicTasksI = new Dictionary<string, DownloadAndProcessing>();
            var prepareVideosInput = inputData.ToObject<PrepareVideosInput>();

            for(int i = 0; i < prepareVideosInput.VideosToProcess.Length; i++)
            {
                TaskConfig taskConfig = new TaskConfig();
                taskConfig.Name = "download_and_process" + i;
                taskConfig.TaskReferenceName = "download_and_process" + i;
                taskConfig.Type = "SUB_WORKFLOW";
                SubWorkflowParamModel subWorkflowParamModel = new SubWorkflowParamModel();
                subWorkflowParamModel.Name = "download_and_process";
                subWorkflowParamModel.Version = 1;
                taskConfig.SubWorkflowParam = subWorkflowParamModel;

                dynamicTasks.Add(taskConfig);

                DownloadAndProcessing downloadAndProcessing = new DownloadAndProcessing();
                downloadAndProcessing.YoutubeLink = prepareVideosInput.VideosToProcess[i].YoutubeLink;
                downloadAndProcessing.From = prepareVideosInput.VideosToProcess[i].From;
                downloadAndProcessing.Length = prepareVideosInput.VideosToProcess[i].Length;

                dynamicTasksI.Add(taskConfig.TaskReferenceName, downloadAndProcessing);
            }

            PrepareVideosOutput prepareVideosOutput = new PrepareVideosOutput();

            prepareVideosOutput.DynamicTasks = dynamicTasks;
            prepareVideosOutput.DynamicTasksI = dynamicTasksI;

            Console.WriteLine(dynamicTasksI);

            return Task.FromResult(JObject.FromObject(prepareVideosOutput));
            
        }
    }
}
