using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Ronald.Downloader.Library.delegates;
using Ronald.Downloader.Library.Enumerate;
using Ronald.Downloader.Library.EventArguments;
using Ronald.Downloader.Library.Factory;
using Ronald.Downloader.Library.Interface;

namespace Ronald.Downloader.Library.Implementation
{
    public class ModelService : IModel, IDisposable
    {
        private event DownloadProgressCallBack _progressCallBack;
        private DownloadProgressEventArgs _progress = default(DownloadProgressEventArgs);
        private DownloadSuccessEventArgs _success = default(DownloadSuccessEventArgs);
        private DownloadErrorEventArgs _error = default(DownloadErrorEventArgs);
        private BackgroundWorker _worker = default(BackgroundWorker);
        private EventArgs e = default(EventArgs);

        public event DownloadSuccessEventHandler InternalSuccessHandler;
        public event DownloadErrorEventHandler InternalErrorHandler;
        public event DownloadProgressEventHandler InternalProgressHandler;
        public DownloadFactory _factory = default(DownloadFactory);
        public ModelService() { }
        public ModelService(DownloadFactory factory)
        {
            this._factory = factory;
            this._worker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            this._worker.DoWork += DoBackgroundProccess;
            this._worker.RunWorkerCompleted += DoBackgroundWorkerComplete;
            this._progressCallBack += new DownloadProgressCallBack(DoBackgroundWorkerProgress);

            this._progress = new DownloadProgressEventArgs();
            this._progress.HashCode = HashCode;

            this._success = new DownloadSuccessEventArgs();
            this._success.HashCode = HashCode;

            this._error = new DownloadErrorEventArgs();
            this._error.HashCode = HashCode;

            this.e = new EventArgs();
        }

        private void DoBackgroundWorkerProgress(Int64 ReadBytes, Int64 TotalBytes)
        {
            if (this.InternalProgressHandler != null)
            {
                this._progress.Percentage =  Convert.ToInt32((ReadBytes * 100) /TotalBytes);
                this.InternalProgressHandler(this._progress, e);
                Console.WriteLine("Download Percentage : {0}", Convert.ToInt32((ReadBytes * 100) /TotalBytes));
            }
        }

        private void DoBackgroundWorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is Exception && this.InternalErrorHandler != null)
            {
                /// Publish Error message to subscriber
                this._error.Exception = (Exception)e.Result;
                this.InternalErrorHandler(this._error, e);
            }
            else if (e.Result is byte[] && this.InternalSuccessHandler != null)
            {
                /// Publish Success message to subcriber
                this._success.Data = (byte[])e.Result;
                this._success.FileName = "";
                this._success.FileType = FileType.mp4;
                this.InternalSuccessHandler(this._success, e);
            }
        }

        private void DoBackgroundProccess(object sender, DoWorkEventArgs e)
        {
            byte[] buffer = new byte[1024];
            try
            {
                HttpWebRequest request = WebRequest.Create(this._factory.UrlLink) as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (MemoryStream memory = new MemoryStream())
                        {
                            int readData = 0;
                            // loop until the data has been downloaded
                            while ((readData = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                memory.Write(buffer, 0, readData);
                                this._progressCallBack(memory.Length, response.ContentLength);
                            }
                            e.Result = memory.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }

        public void Cancel()
        {
            this._worker.CancelAsync();
        }

        public void Dispose()
        {
            this._worker.Dispose();
        }

        public void Dispose(bool disposing)
        {
            this.Dispose();
        }

        public void StartDownload()
        {
            this._worker.RunWorkerAsync();
        }

        public int HashCode
        {
            get { return this.GetHashCode(); }
        }
    }
}
