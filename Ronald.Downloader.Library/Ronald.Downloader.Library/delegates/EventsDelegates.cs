using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ronald.Downloader.Library.delegates
{
    public delegate void DownloadSuccessEventHandler(object sender, EventArgs e);
    public delegate void DownloadErrorEventHandler(object sender, EventArgs e);
    public delegate void DownloadProgressEventHandler(object sender, EventArgs e);

    public delegate void CollectionAddJobEventHandler(object sender, EventArgs e);
    public delegate void CollectionRemoveJobEventHandler(object sender, EventArgs e);

    delegate void DownloadProgressCallBack(Int64 ReadBytes, Int64 TotalBytes);
}
