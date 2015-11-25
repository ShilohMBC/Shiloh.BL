using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL;
using Shiloh.BL.ShilohTableAdapters;

namespace Shiloh.BL
{
    public class PrayerReq
    {
        public PrayerReq()
        {
            InitObject();
        }

        public PrayerReq(int RequestId)
        {
            InitObject();
            _Id = RequestId;
            Get();
        }

        internal PrayerReq(Shiloh.prayerRequestRow Row)
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

        List<PrayerResponse> _Responses;
        public List<PrayerResponse> Responses
        {
            get 
            {
                if (_Responses == null)
                    GetPrayerResponses();

                return _Responses; 
            }
        }

        public bool HasResponses
        {
            get {                
                return (Responses.Count > 0);
            }
        }

        public string BestCallTime { get; set; }
        public string City { get; set; }
        public DateTime DateReceived { get; set; }
        public bool WasProcessed { get; set; }
        public string ProcessedBy { get; set; }
        public bool DoHospitalVisit { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string HospitalName { get; set; }
        public string HospitalRoomNo { get; set; }
        public bool IsConfidential { get; set; }
        public bool IsInDanger { get; set; }
        public bool IsInHospital { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public bool PleaseCall { get; set; }
        public string PrayerNeeds { get; set; }
        public string Referrals { get; set; }
        public string SpecialInstructions { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }

        private prayerRequestTableAdapter _Adapter = null;
        protected prayerRequestTableAdapter Adapter
        {
            get
            {
                if (_Adapter == null)
                    _Adapter = new prayerRequestTableAdapter("shilohConnectionString");

                return _Adapter;
            }
        }

        #endregion

        #region Methods

        private void InitObject()
        {
            BestCallTime = string.Empty;
            City = string.Empty;
            Email = string.Empty;
            FirstName = string.Empty;
            HospitalName = string.Empty;
            HospitalRoomNo = string.Empty;
            LastName = string.Empty;
            MiddleName = string.Empty;
            Phone = string.Empty;
            PrayerNeeds = string.Empty;
            Referrals = string.Empty;
            SpecialInstructions = string.Empty;
            State = string.Empty;
            Address = string.Empty;
            ZipCode = string.Empty;
            WasProcessed = false;
            ProcessedBy = string.Empty;
        }

        internal void LoadObjectFromRow(Shiloh.prayerRequestRow RequestRow)
        {
            if (RequestRow != null)
            {
                BestCallTime = RequestRow.bestCallTime;
                City = RequestRow.city;
                DateReceived = RequestRow.dateReceived;
                DoHospitalVisit = RequestRow.doHospitalVisit;
                Email = RequestRow.emailAddress;
                FirstName = RequestRow.firstName;
                HospitalName = RequestRow.hospitalName;
                HospitalRoomNo = RequestRow.hospitalRoomNo;
                _Id = RequestRow.ID;
                IsConfidential = RequestRow.isConfidential;
                IsInDanger = RequestRow.isInDanger;
                IsInHospital = RequestRow.isInHospital;
                LastName = RequestRow.lastName;
                MiddleName = RequestRow.middleName;
                Phone = RequestRow.phone;
                PleaseCall = RequestRow.pleaseCall;
                PrayerNeeds = RequestRow.prayerNeeds;
                Referrals = RequestRow.resourceReferrals;
                SpecialInstructions = RequestRow.specialInstructions;
                State = RequestRow.state;
                Address = RequestRow.streetAddress;
                ZipCode = RequestRow.zipCode;
                WasProcessed = RequestRow.wasProcessed;
                ProcessedBy = RequestRow.processedBy;
            }
        }

        public void Get()
        {
            if (Id > 0)
            {
                // get request information
                Shiloh.prayerRequestDataTable request = GetPrayerRequestById(Id);
                if (request != null && request.Rows.Count > 0)
                {
                    LoadObjectFromRow((Shiloh.prayerRequestRow)request.Rows[0]);
                }                
            }
        }

        public bool Save()
        {
            bool saved = false;

            if (Id > 0)
                saved = UpdatePrayerRequest();
            else
                saved = AddPrayerRequest();

            return saved;
        }

        public bool Delete()
        {
            return DeletePrayerRequest();
        }

        protected void GetPrayerResponses()
        {
            // get any responses
            if (_Responses == null)
                _Responses = new List<PrayerResponse>();
            else
                _Responses.Clear();

            PrayerResponse response = new PrayerResponse();
            Shiloh.prayerResponseDataTable dtResponses = response.GetByResponsesByRequestId(Id);

            if (dtResponses != null && dtResponses.Rows.Count > 0)
            {
                foreach (Shiloh.prayerResponseRow row in dtResponses.Rows)
                {
                    PrayerResponse resp = new PrayerResponse(row);
                    _Responses.Add(resp);
                }
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        internal Shiloh.prayerRequestDataTable GetPrayerRequestById(int ID)
        {
            return Adapter.GetPrayerRequestById(ID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public Shiloh.prayerRequestDataTable GetByDateRange(DateTime StartDate, DateTime EndDate)
        {
            // get activities by month
            Shiloh.prayerRequestDataTable dtRequests = Adapter.GetPrayerRequestByDateRange(StartDate.Date, EndDate.Date);

            return dtRequests;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        internal bool AddPrayerRequest()
        {
            bool saved = false;


            Shiloh.prayerRequestDataTable dtRequests = new Shiloh.prayerRequestDataTable();
            Shiloh.prayerRequestRow request = dtRequests.NewprayerRequestRow();


            request.bestCallTime = BestCallTime;
            request.city = City;
            request.dateReceived = DateReceived;
            request.doHospitalVisit = DoHospitalVisit;
            request.emailAddress = Email;
            request.firstName = FirstName;
            request.hospitalName = HospitalName;
            request.hospitalRoomNo = HospitalRoomNo;
            request.isConfidential = IsConfidential;
            request.isInDanger = IsInDanger;
            request.isInHospital = IsInHospital;
            request.lastName = LastName;
            request.middleName = MiddleName;
            request.phone = Phone;
            request.pleaseCall = (!string.IsNullOrEmpty(BestCallTime));
            request.prayerNeeds = PrayerNeeds;
            request.resourceReferrals = Referrals;
            request.specialInstructions = SpecialInstructions;
            request.state = State;
            request.streetAddress = Address;
            request.zipCode = ZipCode;
            request.processedBy = ProcessedBy;
            request.wasProcessed = WasProcessed;

            dtRequests.AddprayerRequestRow(request);
            saved = (Adapter.Update(dtRequests) > 0);

            return saved;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        internal bool UpdatePrayerRequest()
        {
            bool updated = false;

            int count = Adapter.UpdatePrayerRequest(
                DateReceived, IsConfidential, WasProcessed, LastName, FirstName, MiddleName,
                Email, Address, City, State, ZipCode, Phone, PleaseCall, BestCallTime,
                IsInHospital, DoHospitalVisit, HospitalName, HospitalRoomNo, IsInDanger,
                PrayerNeeds, Referrals, SpecialInstructions, ProcessedBy, Id, Id);

            updated = (count > 0);

            return updated;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        internal bool DeletePrayerRequest()
        {
            bool deleted = true;

            deleted = (Adapter.DeletePrayerRequestById(Id) > 0);

            return deleted;
        }

        #endregion
    }

}