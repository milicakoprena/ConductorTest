using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadVideos
{
    public class DownloadResponse
    {
        [JsonProperty("downloadPath")]
        public string DownloadPath { get; set; }
    }
}
