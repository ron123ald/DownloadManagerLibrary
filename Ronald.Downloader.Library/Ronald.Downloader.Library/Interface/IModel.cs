using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ronald.Downloader.Library.delegates;

namespace Ronald.Downloader.Library.Interface
{
    public interface IModel
    {
        event DownloadSuccessEventHandler InternalSuccessHandler;
        event DownloadErrorEventHandler InternalErrorHandler;
        event DownloadProgressEventHandler InternalProgressHandler;

        int HashCode { get; }

        void Cancel();
        void Dispose(bool disposing);
        void StartDownload();
    }
}
