using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareVideos
{
    public class DownloadAndProcessing
    {
        [JsonProperty("youtubeLink")]
        public string YoutubeLink { get; set; }

        [JsonProperty("from")]
        public int From { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }
    }
}