using TrimVideos;
using System.Net.Http.Headers;

try
{
    TrimVideosWorker trimVideosWorker = new TrimVideosWorker("trimmer");
    await trimVideosWorker.Poll();
}
catch (Exception e)
{

}