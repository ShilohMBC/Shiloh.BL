using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL;
using Shiloh.BL.ShilohTableAdapters;

namespace Shiloh.BL
{
    public class MinistryEntry
    {
        public MinistryEntry()
            : this(0)
        {
        }

        public MinistryEntry(int MinistryId)
        {
            InitObject();
            _ministryId = MinistryId;
            Get();
        }

        internal MinistryEntry(Shiloh.ministryEntryRow Row)
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

        int _ministryId;
        public int ministryId { get { return _ministryId;  } }
        public string description { get; set; }
        public bool isLocked { get; set; }
        public string contactName { get; set; }
        public string contactPhone1 { get; set; }
        public string contactPhone2 { get; set; }
        public string contactEmail { get; set; }
        public string summary { get; set; }
        public string supportInfo { get; set; }
        public string miscInfo { get; set; }
        public string programInfo { get; set; }
        public DateTime dateCreated { get; set; }

        #endregion

        #region Methods

        protected void InitObject()
        {
            supportInfo = string.Empty;
            miscInfo = string.Empty;
            programInfo = string.Empty;
            description = string.Empty;
            contactName = string.Empty;
            contactPhone1 = string.Empty;
            contactEmail = string.Empty;
            summary = string.Empty;
            contactEmail = string.Empty;
        }

        public void Get()
        {
            if (ministryId > 0)
            {
                Shiloh.ministryEntryDataTable dtTable = GetMinistryEntryByMinistryId(ministryId);

                if (dtTable != null && dtTable.Rows.Count > 0)
                {
                    CopyRowToObject((Shiloh.ministryEntryRow)dtTable.Rows[0]);
                }
            }
        }

        internal void CopyRowToObject(Shiloh.ministryEntryRow Row)
        {
            if (Row != null)
            {
                _Id = Row.rowId;
                _ministryId = Row.ministryId;
                miscInfo = Row.miscInfo;
                description = Row.description;
                dateCreated = Row.datecreated;
                isLocked = Row.isLocked;
                contactName = Row.contactName;
                contactEmail = Row.contactEmail;
                contactPhone1 = Row.contactPhone1;
                contactPhone2 = Row.contactPhone2;
                summary = Row.summary;
                programInfo = Row.programInfo;
                supportInfo = Row.supportInfo;
            }
        }

        public bool Save()
        {
            bool saved = false;

            try
            {
                if (Id > 0)
                {
                   saved = UpdateEntry();
                }
                else
                {
                    saved = AddEntry();
                }
            }
            catch (Exception ex)
            {
                saved = false;
            }

            return saved;
        }

        public bool Delete()
        {
            bool deleted = true;

            if (Id > 0)
            {
                deleted = DeleteEntry(Id);
            }

            return deleted;
        }

        #endregion

        #region TableAdapterMethods

        private ministryEntryTableAdapter _Adapter = null;
        protected ministryEntryTableAdapter Adapter
        {
            get
            {
                if (_Adapter == null)
                    _Adapter = new ministryEntryTableAdapter("shilohConnectionString");

                return _Adapter;
            }
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Shiloh.ministryEntryDataTable GetAllEntries()
        {
            return Adapter.GetAllEntries();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        protected Shiloh.ministryEntryDataTable GetMinistryEntryByMinistryId(int MinistryId)
        {
            return Adapter.GetEntryByMinistryId(MinistryId);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        internal bool AddEntry()
        {
            int rowsAffected = Adapter.Insert(ministryId, isLocked, summary, contactName, contactPhone1, contactPhone2, contactEmail, description, supportInfo, programInfo, miscInfo, DateTime.Now);

            // Return true if precisely one row was inserted, otherwise false
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        internal bool UpdateEntry()
        {
            Shiloh.ministryEntryDataTable dtTable = Adapter.GetEntryByMinistryId(ministryId);
            if (dtTable.Count == 0)
                return false;

            Shiloh.ministryEntryRow row = dtTable[0];

            row.contactEmail = contactEmail;
            row.contactName = contactName;
            row.contactPhone1 = contactPhone1;
            row.contactPhone2 = contactPhone2;
            row.datecreated = dateCreated;
            row.description = description;
            row.isLocked = isLocked;
            row.miscInfo = miscInfo;
            row.programInfo = programInfo;
            row.summary = summary;
            row.supportInfo = supportInfo;            

            int rowsAffected = Adapter.Update(row);

            // Return true if precisely one row was inserted, otherwise false
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        internal bool DeleteEntry(int RowId)
        {
            int rowsAffected = Adapter.Delete(RowId);

            // Return true if precisely one row was deleted, otherwise false
            return rowsAffected == 1;
        }

        #endregion


    }
}
