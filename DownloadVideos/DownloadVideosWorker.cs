using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrepareVideos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadVideos
{
    public class DownloadVideosWorker : Worker
    {
        public DownloadVideosWorker(string task_) : base(task_)
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
            Console.WriteLine(outputFilename);
            outputFilename = outputFilename.Replace("\n", string.Empty);
            await process.WaitForExitAsync();
            return outputFilename;
        }

        public static async Task DownloadVideo(string ytdlp, string fileFormat, string downloadPath,
            string youtubeLink)
        {
            var downloadProcess = Process.Start(ytdlp, new[] { "-o", fileFormat, "-P", downloadPath, youtubeLink });
            await downloadProcess.WaitForExitAsync();
        }

        public async override Task<JObject> Work(JObject inputData)
        {
            var videoLink = inputData.ToObject<VideoLink>();
            Console.WriteLine(videoLink);
            string ytdplExe = "C:\\Users\\Codaxy\\Desktop\\conductor\\yt-dlp.exe";
            string fileFormat = $"{Guid.NewGuid()}.%(ext)s";

            var ytdlpFilename = await GetYtdlpFilename(ytdplExe, fileFormat, videoLink.YoutubeLink);

            string downloadDir = "\\\\192.168.0.20\\codaxy\\Praksa\\downloadDir";
            string downloadPath = Path.Combine(downloadDir,ytdlpFilename);

            await DownloadVideo(ytdplExe, fileFormat, downloadDir, videoLink.YoutubeLink);

            var response = new DownloadResponse { DownloadPath = downloadPath };
            return JObject.FromObject(response);
        }

    }
}
