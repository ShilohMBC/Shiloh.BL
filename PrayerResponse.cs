using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL;
using Shiloh.BL.ShilohTableAdapters;

namespace Shiloh.BL
{
    public class PrayerResponse
    {
         public PrayerResponse()
        {
            InitObject();
        }

         public PrayerResponse(int ResponseId)
        {
            InitObject();
            _Id = ResponseId;
            Get();
        }

         internal PrayerResponse(Shiloh.prayerResponseRow Row)
         {
             InitObject();
             LoadObjectFromRow(Row);
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

         public int RequestId { get; set; }
         public string ProcessedBy { get; set; }
         public string ResponseText { get; set; }
         public DateTime DateCreated { get; set; }

         private prayerResponseTableAdapter _Adapter = null;
         protected prayerResponseTableAdapter Adapter
         {
             get
             {
                 if (_Adapter == null)
                     _Adapter = new prayerResponseTableAdapter("shilohConnectionString");

                 return _Adapter;
             }
         }

        #endregion

        #region Methods

         private void InitObject()
         {
             _Id = 0;
             RequestId = 0;
             ResponseText = string.Empty;
             DateCreated = DateTime.MinValue;
             ProcessedBy = string.Empty;
         }

         internal void LoadObjectFromRow(Shiloh.prayerResponseRow Row)
         {
             if (Row != null)
             {
                 _Id = Row.ID;
                 RequestId = Row.requestId;
                 ResponseText = Row.response;
                 DateCreated = Row.dateCreated;
                 ProcessedBy = Row.processedBy;
             }
         }

         public void Get()
         {
             if (Id > 0)
             {
                 Shiloh.prayerResponseDataTable response = GetPrayerResponseById(Id);
                 if (response != null && response.Rows.Count > 0)
                 {
                     LoadObjectFromRow((Shiloh.prayerResponseRow)response.Rows[0]);
                 }
             }
         }

         public bool Save()
         {
             bool saved = false;

             if (Id > 0)
                 saved = UpdatePrayerResponse();
             else
                 saved = AddPrayerResponse();

             return saved;
         }

         public bool Delete()
         {
             return DeletePrayerResponse();
         }

        #endregion

        #region DataLayer Methods
         [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
         internal Shiloh.prayerResponseDataTable GetPrayerResponseById(int ID)
         {
             return Adapter.GetPrayerRequestById(ID);
         }

         [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
         public Shiloh.prayerResponseDataTable GetByResponsesByRequestId(int PrayerRequestId)
         {
             // get activities by month
             Shiloh.prayerResponseDataTable dtResponses = Adapter.GetByResponsesByRequestId(PrayerRequestId);

             return dtResponses;
         }

         [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
         internal bool AddPrayerResponse()
         {
             bool saved = false;

             Shiloh.prayerResponseDataTable dtResponses = new Shiloh.prayerResponseDataTable();
             Shiloh.prayerResponseRow response = dtResponses.NewprayerResponseRow();

             response.ID = _Id;
             response.requestId = RequestId;
             response.response = ResponseText;
             response.dateCreated = DateTime.Now;
             response.processedBy = ProcessedBy;

             dtResponses.AddprayerResponseRow(response);
             saved = (Adapter.Update(dtResponses) > 0);

             return saved;
         }

         [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
         internal bool UpdatePrayerResponse()
         {
             bool updated = false;

             int count = Adapter.UpdatePrayerRequest(RequestId, ProcessedBy, ResponseText, DateTime.Now, _Id);

             updated = (count > 0);

             return updated;
         }

         [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
         internal bool DeletePrayerResponse()
         {
             bool deleted = true;

             deleted = (Adapter.DeletePrayerRequest(_Id) > 0);

             return deleted;
         }

        #endregion
    }
}
