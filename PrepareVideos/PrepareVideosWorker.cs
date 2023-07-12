using Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareVideos
{
    internal class PrepareVideosWorker : Worker
    {
        public PrepareVideosWorker(string task_) : base(task_)
        {
        }

        public override async Task<JObject> Work(JObject inputData)
        {
            return new JObject();
            
        }
    }
}
