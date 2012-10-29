using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ronald.Downloader.Library.delegates;
using Ronald.Downloader.Library.EventArguments;
using Ronald.Downloader.Library.Interface;

namespace Ronald.Downloader.Library.Collections
{
    public class DownloadCollections
    {
        private EventArgs e = new EventArgs();
        public event CollectionAddJobEventHandler NewChannel;
        public event CollectionRemoveJobEventHandler RemoveChannel;
        private Dictionary<int, IModel> _models = default(Dictionary<int, IModel>);
        private static DownloadCollections _instance = default(DownloadCollections);
        private DownloadCollections() {
            this._models = new Dictionary<int, IModel>();
        }
        public static DownloadCollections GetInstanceContext()
        {
            return _instance ?? (_instance = (new DownloadCollections()));
        }

        public void AddModel(IModel channel)
        {
            if (!this._models.ContainsValue(channel))
            {
                if (this.NewChannel != null)
                    this.NewChannel(new CollectionAddJobEventArgs(), e);
                this._models.Add(channel.HashCode, channel);
            }
        }

        public void RemoveModel(IModel channel)
        {
            if (this._models.ContainsValue(channel))
            {
                if (this.RemoveChannel != null)
                    this.RemoveChannel(new CollectionRemoveJobEventArgs(), e);
                this._models.Remove(channel.GetHashCode());
            }
        }

        public IModel GetModel(IModel channel)
        {
            if (this._models.ContainsValue(channel))
                return this._models[channel.GetHashCode()];
            return default(IModel);
        }
    }
}
