using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ronald.Downloader.Library.Enumerate;
using Ronald.Downloader.Library.Factory;
using Ronald.Downloader.Library.Implementation;
using Ronald.Downloader.Library.Interface;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IDownloadService service = DownloadService.GetInstanceContext();
            service.DownloadError += service_DownloadError;
            service.DownloadProgress += service_DownloadProgress;
            service.DownloadSuccess += service_DownloadSuccess;
            service.NewChannel += service_NewChannel;

            ///string toDownload = "http://localhost/localservice/CaC.mp3";
            string toDownload = "http://localhost:81/480part1.mp4";
            //string toDownload = "http://www1.watchop.com/watch/one-piece-episode-570-english-subbed/";
            DownloadFactory factory = new DownloadFactory(toDownload, "", FileType.flv);
            IModel model = factory.CreateChannel();
            service.DownloadChannel(model);
            Console.ReadLine();
        }

        static void service_NewChannel(object sender, EventArgs e)
        {
            Console.WriteLine("");
        }

        static void service_DownloadSuccess(object sender, EventArgs e)
        {
        }

        static void service_DownloadProgress(object sender, EventArgs e)
        {
        }

        static void service_DownloadError(object sender, EventArgs e)
        {
        }
    }
}
