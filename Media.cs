using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL.ShilohTableAdapters;

namespace Shiloh.BL
{
    #region Media 

    public partial class Media
    {
        public Media()
        {
            InitObject();
        }

        public Media(int MediaId)
        {
            Id = MediaId;
            Get();
        }

        public Media(Shiloh.mediaRow Row)
        {
            LoadObjectFromRow(Row);
        }

        #region Properties

        public int Id { get; internal set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime MediaDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsStreaming { get; set; }
        public int ProviderId { get; private set; }
        MediaProvider _Provider;
        public MediaProvider Provider {
            get
            {
                if ((ProviderId > 0) && (_Provider == null))
                    _Provider = new MediaProvider(ProviderId);

                return _Provider;
            }
            set
            {
                _Provider = value;
                if (value != null)
                    ProviderId = _Provider.Id;
            }
        }
        public int CategoryId { get; private set; }
        MediaCategory _Category;
        public MediaCategory Category {
            get
            {
                if ((CategoryId > 0) && (_Category == null))
                    _Category = new MediaCategory(CategoryId);

                return _Category;
            }
            set
            {
                _Category = value;
                if (value != null)
                    CategoryId = _Category.Id;
            }
        }
        public string MIMEType { get; set; }
        public int Size { get; set; }
        public string URL  { get; set; }
        public string EmbedObjectBlob { get; set; }
        public DateTime DateCreated { get; private set; }

        #endregion

        #region Methods

        private void InitObject()
        {
            Id = 0;
            Name = string.Empty;
            Description = string.Empty;
            MediaDate = DateTime.MinValue;
            MIMEType = string.Empty;
            URL = string.Empty;
            EmbedObjectBlob = string.Empty;
        }

        internal void LoadObjectFromRow(Shiloh.mediaRow Row)
        {
            if (Row != null)
            {
                Id = Row.Id;
                Name = Row.Name;
                Description = Row.Description;
                MediaDate = Row.MediaDate;
                IsActive = Row.IsActive;
                IsStreaming = Row.IsStreaming;
                ProviderId = Row.ProviderId;
                Provider = new MediaProvider(Row.ProviderId);
                CategoryId = Row.CategoryId;
                Category = new MediaCategory(Row.CategoryId);
                MIMEType = Row.MIMEType;
                Size = Row.Size;
                URL = Row.URLPath;
                EmbedObjectBlob = Row.EmbedObjectBlob;
                DateCreated = Row.DateCreated;
            }
        }

        protected void Get()
        {
            if (Id > 0)
            {
                Shiloh.mediaDataTable media = GetMediaByID(Id);

                if (media != null && media.Count > 0)
                {
                    LoadObjectFromRow((Shiloh.mediaRow)media.Rows[0]);
                }
            }
        }

        public bool Save()
        {
            bool saved = false;

            if (Id > 0)
                saved = UpdateMedia();
            else
                saved = AddMedia();

            return saved;
        }

        public bool Delete()
        {
            bool deleted = true;

            if (Id > 0)
                deleted = DeleteMedia(Id);

            return deleted;
        }

        #endregion

        #region Data Adapter Functions

        private mediaTableAdapter _Adapter = null;
        protected mediaTableAdapter Adapter
        {
            get
            {
                if (_Adapter == null)
                    _Adapter = new mediaTableAdapter("shilohConnectionString");

                return _Adapter;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Shiloh.mediaDataTable GetAllMedia()
        {
            return Adapter.GetMedia();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public Shiloh.mediaDataTable GetMediaByID(int ID)
        {
            return Adapter.GetMediaById(ID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        internal Shiloh.mediaDataTable GetByDateRange(DateTime StartDate, DateTime EndDate)
        {
            // get activities by month
            Shiloh.mediaDataTable dtMedia = Adapter.GetMediaByDateRange(StartDate, EndDate);

            return dtMedia;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        internal bool AddMedia()
        {
            int rowsAffected = Adapter.InsertMedia(CategoryId, Name, Description, MediaDate, IsActive, ProviderId, IsStreaming, MIMEType, Size, URL, EmbedObjectBlob, DateTime.Now);

            // Return true if precisely one row was inserted, otherwise false
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        internal bool UpdateMedia()
        {
            Shiloh.mediaDataTable dtMedia = Adapter.GetMediaById(Id);
            if (dtMedia.Count == 0)
                return false;

            Shiloh.mediaRow media = dtMedia[0];

            media.CategoryId = CategoryId;
            media.DateCreated = DateCreated;
            media.Description = Description;
            media.EmbedObjectBlob = EmbedObjectBlob;
            media.IsActive = IsActive;
            media.IsStreaming = IsStreaming;
            media.MediaDate = MediaDate;
            media.MIMEType = MIMEType;
            media.Name = Name;
            media.ProviderId = ProviderId;
            media.Size = Size;
            media.URLPath = URL;

            int rowsAffected = Adapter.Update(media);

            // Return true if precisely one row was inserted, otherwise false
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        internal bool DeleteMedia(int ID)
        {
            int rowsAffected = Adapter.DeleteMediaById(ID);

            // Return true if precisely one row was deleted, otherwise false
            return rowsAffected == 1;
        }

        #endregion

    }

    #endregion

    #region Medias

    public class Medias : List<Media>
    {
        static public Medias GetAll()
        {
            Medias medias = new Medias();
            Media _medias = new Media();

            foreach (Shiloh.mediaRow row in _medias.GetAllMedia())
                medias.Add(new Media(row));

            return medias;
        }

        static public Medias GetByDateRange(DateTime StartDate, DateTime EndRange)
        {
            Medias medias = new Medias();
            Media _medias = new Media();

            foreach (Shiloh.mediaRow row in _medias.GetByDateRange(StartDate, EndRange))
                medias.Add(new Media(row));

            return medias;
        }
    }

    #endregion
}
