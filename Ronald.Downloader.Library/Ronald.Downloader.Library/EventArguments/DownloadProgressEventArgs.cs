using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ronald.Downloader.Library.EventArguments
{
    public class DownloadProgressEventArgs : DownloadCommonAttributes
    {
        public int Percentage { get; set; }
        public DownloadProgressEventArgs() { }
        public DownloadProgressEventArgs(int Percentage, int HashCode)
        {
            this.Percentage = Percentage;
            this.HashCode = HashCode;
        }
    }
}
