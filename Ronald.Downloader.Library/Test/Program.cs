using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ronald.Downloader.Library.Enumerate;
using Ronald.Downloader.Library.EventArguments;
using Ronald.Downloader.Library.Factory;
using Ronald.Downloader.Library.Implementation;
using Ronald.Downloader.Library.Interface;

namespace Test
{
    class Program
    {
        static IDownloadService service = default(IDownloadService);
        static void Main(string[] args)
        {
            service = DownloadService.GetInstanceContext();
            service.DownloadError += service_DownloadError;
            service.DownloadProgress += service_DownloadProgress;
            service.DownloadSuccess += service_DownloadSuccess;
            service.NewChannel += service_NewChannel;

            //string toDownload = "http://www.directvid.com/v/23589e18aeae9f1c74d837b5b76c39b0.mp4";
            Console.Write("Enter the link here to download : ");
            string toDownload = Console.ReadLine();
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
