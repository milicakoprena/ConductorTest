using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareVideos
{
    public class PrepareVideosInput
    {
        [JsonProperty("videosToProcess")]
        public DownloadAndProcessing[] VideosToProcess { get; set; }
    }
}