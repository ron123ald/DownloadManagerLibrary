using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ronald.Downloader.Library.Attributes;
using Ronald.Downloader.Library.Enumerate;
using Ronald.Downloader.Library.Implementation;
using Ronald.Downloader.Library.Interface;

namespace Ronald.Downloader.Library.Factory
{
    public class DownloadFactory : DownloadAttributes
    {

        public DownloadFactory()
        {
        }

        public DownloadFactory(string UrlLink, string FileLocation, FileType Type)
        {
            this.FileLocation = FileLocation;
            this.UrlLink = UrlLink;
            this.FileType = Type;
        }

        public override string UrlLink
        {
            get;
            set;
        }

        public override FileType FileType
        {
            get;
            set;
        }

        public override string FileLocation
        {
            get;
            set;
        }

        public IModel CreateChannel()
        {
            return (IModel)new ModelService(this);
        }
    }
}
