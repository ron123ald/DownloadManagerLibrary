using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ronald.Downloader.Library.EventArguments
{
    public class DownloadErrorEventArgs : DownloadCommonAttributes
    {
        public Exception Exception { get; set; }
        public DownloadErrorEventArgs() { }
        public DownloadErrorEventArgs(Exception Exception, int HasCode) 
        {
            this.Exception = Exception;
            this.HashCode = HashCode;
        }
    }
}
