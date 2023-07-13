using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrimVideos
{
    public class VideoPath
    {
        [JsonProperty("videoPath")]
        public string Path { get; set; }
        [JsonProperty("length")]
        public int Length { get; set; }
        [JsonProperty("from")]
        public int From { get; set; }
    }
}
