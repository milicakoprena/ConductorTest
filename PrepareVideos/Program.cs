using PrepareVideos;
using System.Net.Http.Headers;

try
{
    PrepareVideosWorker prepareVideosWorker = new PrepareVideosWorker("prepare_videos");
    await prepareVideosWorker.Poll();
}catch(Exception e)
{
   
}