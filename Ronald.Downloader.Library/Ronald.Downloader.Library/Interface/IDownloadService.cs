using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ronald.Downloader.Library.delegates;

namespace Ronald.Downloader.Library.Interface
{
    public interface IDownloadService
    {
        event DownloadSuccessEventHandler DownloadSuccess;
        event DownloadErrorEventHandler DownloadError;
        event DownloadProgressEventHandler DownloadProgress;

        event CollectionAddJobEventHandler NewChannel;
        event CollectionRemoveJobEventHandler RemoveChannel;
        void DownloadChannel(IModel Channel);
        void Save();
    }
}
