using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ronald.Downloader.Library.Enumerate;

namespace Ronald.Downloader.Library.Attributes
{
    public abstract class DownloadAttributes
    {
        public abstract string UrlLink { get; set; }
        public abstract FileType FileType { get; set; }
        public abstract string FileLocation { get; set; }
    }
}
