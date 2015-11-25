using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL;
using Shiloh.BL.ShilohTableAdapters;
 
namespace Shiloh.BL
{
    public class Ministry
    {
        public Ministry() : this(0)
        {
        }

        public Ministry(int MinistryId)
        {
            InitObject();
            _Id = MinistryId;
            Get();
        }

        internal Ministry(Shiloh.ministryRow Row)
        {
            InitObject();
            CopyRowToObject(Row);
        }

        #region Properties
        int _Id;
        public int Id
        {
            get
            {
                return _Id;
            }
        }

        public string name { get; set; }
        public string description { get; set; }
        public bool isMajor { get; set; }
        public string contactName { get; set; }
        public string contactPhone1 { get; set; }
        public string contactPhone2 { get; set; }
        public string contactEmail { get; set; }
        public string summary { get; set; }
        public int? parentId { get; set; }
        public DateTime dateCreated { get; set; }

        #endregion

        #region Methods

        protected void InitObject()
        {
            name = string.Empty;
            description = string.Empty;
            contactName = string.Empty;
            contactPhone1 = string.Empty;
            contactEmail = string.Empty;
            summary = string.Empty;
            contactEmail = string.Empty;
        }

        protected void Get()
        {
            if (Id > 0)
            {
                Shiloh.ministryDataTable dtTable = GetMinistryByID(Id);

                if (dtTable != null && dtTable.Rows.Count > 0)
                {
                    CopyRowToObject((Shiloh.ministryRow)dtTable.Rows[0]);
                }
                else
                    InitObject();
            }
        }

        internal void CopyRowToObject(Shiloh.ministryRow Row)
        {
            if (Row != null)
            {
                _Id = Row.ministryId;
                name = Row.name;
                description = Row.description;
                dateCreated = Row.dateCreated;
                isMajor = Row.isMajor;
                contactName = Row.contactName;
                contactEmail = Row.contactEmail;
                contactPhone1 = Row.contactPhone1;
                contactPhone2 = Row.contactPhone2;
                summary = Row.summary;
                parentId = Row.parentMinistryId;                
            }
        }

        #endregion

        #region TableAdapterMethods

        private ministryTableAdapter _Adapter = null;
        protected ministryTableAdapter Adapter
        {
            get
            {
                if (_Adapter == null)
                    _Adapter = new ministryTableAdapter("shilohConnectionString");

                return _Adapter;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Shiloh.ministryDataTable GetAllMinistries()
        {
            return Adapter.GetAllMinistries();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        protected Shiloh.ministryDataTable GetMinistryByID(int ID)
        {
            return Adapter.GetMinistryById(ID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        protected Shiloh.ministryDataTable GetMinistriesByParentId(int ParentMinistryId)
        {
            Shiloh.ministryDataTable dtTable = Adapter.GetMinistriesByParentId(ParentMinistryId);

            return dtTable;
        }       

        #endregion


    }
}
