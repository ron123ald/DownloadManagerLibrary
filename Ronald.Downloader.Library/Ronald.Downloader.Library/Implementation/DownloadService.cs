using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ronald.Downloader.Library.Collections;
using Ronald.Downloader.Library.delegates;
using Ronald.Downloader.Library.EventArguments;
using Ronald.Downloader.Library.Interface;

namespace Ronald.Downloader.Library.Implementation
{
    public class DownloadService : IDownloadService
    {
        public event DownloadSuccessEventHandler DownloadSuccess;
        public event DownloadErrorEventHandler DownloadError;
        public event DownloadProgressEventHandler DownloadProgress;

        public event CollectionAddJobEventHandler NewChannel;
        public event CollectionRemoveJobEventHandler RemoveChannel;

        private DownloadCollections collections = default(DownloadCollections);
        private static DownloadService _instance = default(DownloadService);
        private DownloadService() 
        {
            this.collections = DownloadCollections.GetInstanceContext();
            this.collections.NewChannel += collections_NewChannel;
            this.collections.RemoveChannel += collections_RemoveChannel;
        }

        private void collections_RemoveChannel(object sender, EventArgs e)
        {
            if (this.RemoveChannel != null && (CollectionRemoveJobEventArgs)sender != null)
                this.RemoveChannel((CollectionRemoveJobEventArgs)sender, e);
        }

        private void collections_NewChannel(object sender, EventArgs e)
        {
            if (this.NewChannel != null && (CollectionAddJobEventArgs)sender != null)
                this.NewChannel((CollectionAddJobEventArgs)sender, e);
        }

        public static DownloadService GetInstanceContext()
        {
            return _instance ?? (_instance = (new DownloadService()));
        }

        public void DownloadChannel(IModel Channel)
        {
            this.collections.AddModel(Channel);
            
            Channel.InternalErrorHandler += Channel_InternalErrorHandler;
            Channel.InternalProgressHandler += Channel_InternalProgressHandler;
            Channel.InternalSuccessHandler += Channel_InternalSuccessHandler;

            System.Action download = (()=> Channel.StartDownload());
            download.Invoke();
        }

        void Channel_InternalSuccessHandler(object sender, EventArgs e)
        {
            if (this.DownloadProgress != null && (DownloadSuccessEventArgs)sender != null)
                this.DownloadProgress((DownloadSuccessEventArgs)sender, e);
        }

        void Channel_InternalProgressHandler(object sender, EventArgs e)
        {
            if (this.DownloadProgress != null && (DownloadProgressEventArgs)sender != null)
                this.DownloadProgress((DownloadProgressEventArgs)sender, e);
        }

        void Channel_InternalErrorHandler(object sender, EventArgs e)
        {
            if (this.DownloadProgress != null && (DownloadErrorEventArgs)sender != null)
                this.DownloadError((DownloadErrorEventArgs)sender, e);
        }
    }
}
