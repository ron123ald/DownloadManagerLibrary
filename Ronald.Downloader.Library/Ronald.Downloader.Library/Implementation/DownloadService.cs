using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ronald.Downloader.Library.Collections;
using Ronald.Downloader.Library.delegates;
using Ronald.Downloader.Library.Enumerate;
using Ronald.Downloader.Library.EventArguments;
using Ronald.Downloader.Library.Interface;

namespace Ronald.Downloader.Library.Implementation
{
    public class DownloadService : IDownloadService
    {
        private string _location = string.Empty;
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


            string Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string target = Path + "\\OnePiece Downloads\\";
            if (!Directory.Exists(target))
                Directory.CreateDirectory(target);
            _location = target;
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

            if (Save(((DownloadSuccessEventArgs)sender).Data, ((DownloadSuccessEventArgs)sender).FileName, ((DownloadSuccessEventArgs)sender).FileType))
                if (this.DownloadSuccess != null && (DownloadSuccessEventArgs)sender != null)
                    this.DownloadSuccess((DownloadSuccessEventArgs)sender, e); 
        }

        void Channel_InternalProgressHandler(object sender, EventArgs e)
        {
            if (this.DownloadProgress != null && (DownloadProgressEventArgs)sender != null)
                this.DownloadProgress((DownloadProgressEventArgs)sender, e);
        }

        void Channel_InternalErrorHandler(object sender, EventArgs e)
        {
            if (this.DownloadError != null && (DownloadErrorEventArgs)sender != null)
                this.DownloadError((DownloadErrorEventArgs)sender, e);
        }


        public void Save()
        {
            throw new NotImplementedException();
        }

        private bool Save(byte[] downloadedData, string filename, FileType fileType)
        {
            bool flag = false;

            using (FileStream filetoSave = new FileStream(String.Format("{0}{1}.{2}", _location, filename, fileType.ToString()), FileMode.Create))
            {
                filetoSave.Write(downloadedData, 0, downloadedData.Length);
                flag = true;
            }
            return flag;
        }
    }
}
