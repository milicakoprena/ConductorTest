using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrimVideos
{
    public class OutputPath
    {
        [JsonProperty("outputFilename")]
        public string OutputFilename { get; set; }
    }
}
