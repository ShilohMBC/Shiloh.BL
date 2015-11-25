using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL.ShilohTableAdapters;

namespace Shiloh.BL
{
    #region MediaProvider Class

    public class MediaProvider
    {
        #region Constructors

        public MediaProvider()
        {
        }

        public MediaProvider(int ProviderId)
        {
            _Id = ProviderId;
            Get();
        }

        public MediaProvider(Shiloh.mediaProviderRow Row)
        {
            LoadObjectFromRow(Row);
        }

        #endregion

        #region Properties

        internal int _Id;
        public int Id
        {
            get { return _Id; }
        }

        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        #region DataAccess Methods

        private mediaProviderTableAdapter _Adapter = null;
        protected mediaProviderTableAdapter Adapter
        {
            get
            {
                if (_Adapter == null)
                    _Adapter = new mediaProviderTableAdapter("shilohConnectionString");

                return _Adapter;
            }
        }

        internal void LoadObjectFromRow(Shiloh.mediaProviderRow Row)
        {
            if (Row != null)
            {
                _Id = Row.Id;
                Name = Row.Name;
                Description = Row.Description;
            }
        }

        protected internal Shiloh.mediaProviderDataTable GetAll()
        {
            return Adapter.GetMediaProvider();
        }

        protected void Get()
        {
            if (Id > 0)
            {
                Shiloh.mediaProviderDataTable provider = Adapter.GetMediaProviderById(Id);

                if (provider != null && provider.Count > 0)
                {
                    LoadObjectFromRow((Shiloh.mediaProviderRow)provider.Rows[0]);
                }                
            }
        }

        #endregion
    }

    #endregion

    #region MediaProviders

    public class MediaProviders : List<MediaProvider>
    {
        static public MediaProviders GetAll()
        {
            MediaProviders providers = new MediaProviders();

            MediaProvider _providers = new MediaProvider();

            foreach (Shiloh.mediaProviderRow row in _providers.GetAll())
                providers.Add(new MediaProvider(row));

            return providers;
        }
    }

    #endregion
}
