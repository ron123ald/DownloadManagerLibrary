using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ronald.Downloader.Library.Enumerate;

namespace Ronald.Downloader.Library.EventArguments
{
    public class DownloadSuccessEventArgs : DownloadCommonAttributes
    {
        public byte[] Data { get; set; }
        public string FileName { get; set; }
        public FileType FileType { get; set; }

        public DownloadSuccessEventArgs() { }

        public DownloadSuccessEventArgs(byte[] Data, string FileName, FileType FileType) 
        {
            this.Data = Data;
            this.FileType = FileType;
            this.FileName = FileName;
        }
    }
}
