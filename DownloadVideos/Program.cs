using DownloadVideos;
using System.Net.Http.Headers;

try
{
    DownloadVideosWorker downloadVideosWorker = new DownloadVideosWorker("downloader");
    await downloadVideosWorker.Poll();
}
catch (Exception e)
{

}