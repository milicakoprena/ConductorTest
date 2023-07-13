using Common;
using DownloadVideos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrimVideos
{
    public class TrimVideosWorker : Worker
    {
        public TrimVideosWorker(string task_) : base(task_)
        {
        }

        public static async Task<string> GetYtdlpFilename(string ytdlp, string fileFormat, string youtubeLink)
        {
            var process = new Process();
            process.StartInfo.FileName = ytdlp;
            process.StartInfo.RedirectStandardOutput = true;
            var args = new[] { "--print", "filename", "-o", fileFormat, youtubeLink };
            foreach (var arg in args)
            {
                process.StartInfo.ArgumentList.Add(arg);
            }

            process.Start();
            var outputFilename = await process.StandardOutput.ReadToEndAsync();
            outputFilename = outputFilename.Replace("\n", string.Empty);
            await process.WaitForExitAsync();
            return outputFilename;
        }


        private async Task TrimVideo(string ffmpeg, string videoInputPath,
    int from, int length, string outputPath)
        {
            var downloadProcess = Process.Start(ffmpeg, new[] { "-i", videoInputPath, "-ss", from.ToString(), "-t", length.ToString(), "-c:v", "copy", "-c:a", "copy", outputPath });
            await downloadProcess.WaitForExitAsync();
        }

        public override async Task<JObject> Work(JObject inputData)
        {
            var videoPath = inputData.ToObject<VideoPath>();
            var ffmpeg = "C:\\Users\\Codaxy\\Desktop\\conductor\\ffmpeg.exe";
            string filename = $"{Guid.NewGuid()}.mp4";
            string outputPath = Path.Combine("\\\\192.168.0.20\\codaxy\\Praksa\\trimDir", filename);
            await TrimVideo(ffmpeg, videoPath.Path, videoPath.From, videoPath.Length, outputPath);

            var response = new OutputPath { OutputFilename = outputPath };
            return JObject.FromObject(response);
        }
    }
}
