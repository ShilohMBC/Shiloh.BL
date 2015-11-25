using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL;
using Shiloh.BL.ShilohTableAdapters;

namespace Shiloh.BL
{
    public class Activity
    {
        public Activity()
        {
            InitObject();
        }

        public Activity(int ActivityId)
        {
            InitObject();
            _Id = ActivityId;
            Get();
        }

        public Activity(Shiloh.activityRow ActivityRow)
        {
            InitObject();
            _Id = ActivityRow.ID;
            CopyRowToObject(ActivityRow);
        }

        #region Properties

        int _Id;
        public int Id
        {
            get { return _Id; }
        }

        public string Name { get; set; }
        public string Tag { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Description { get; set; }
        public int Frequency { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIPcode { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ContactURL { get; set; }
        public string ImagePath { get; set; }
        public int HostId { get; set; }

        #endregion

        #region Methods

        public void Get()
        {
            if (Id > 0)
            {
                Shiloh.activityDataTable dtActivity = GetActivityByID(Id);

                if (dtActivity != null && dtActivity.Rows.Count > 0)
                {
                    CopyRowToObject((Shiloh.activityRow)dtActivity.Rows[0]);
                }
                else
                    InitObject();
            }
        }

        public bool Save()
        {
            bool saved = false;

            if (Id > 0)
                saved = UpdateActivity();
            else
                saved = AddActivity();

            return saved;
        }

        public bool Delete()
        {
            bool deleted = true;

            if (Id > 0)
                deleted = DeleteActivity(Id);

            return deleted;
        }

        private void InitObject()
        {
            _Id = 0;
            EndDateTime = DateTime.MinValue;
            StartDateTime = DateTime.MinValue;
            Description = string.Empty;
            Frequency = 0;
            HostId = 0;
            Name = string.Empty;
            Tag = string.Empty;
            Location = string.Empty;
            State = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            State = string.Empty;
            ZIPcode = string.Empty;
            ContactName = string.Empty;
            ContactPhone = string.Empty;
            ContactEmail = string.Empty;
            ContactURL = string.Empty;
            ImagePath = string.Empty;
        }

        internal void CopyRowToObject(Shiloh.activityRow Row)
        {
            if (Row != null)
            {
                _Id = Row.ID;
                EndDateTime = Row.activityEnd;
                StartDateTime = Row.activityStart;
                Description = Row.description;
                Frequency = Row.frequency;
                HostId = Row.hostCommunityID;
                Name = Row.name;
                Tag = Row.tag;
                Location = Row.location;
                State = Row.state;
                Address = Row.address;
                City = Row.city;
                State = Row.state;
                ZIPcode = Row.zipCode;
                ContactName = Row.contactName;
                ContactPhone = Row.contactPhone;
                ContactEmail = Row.contactEmailAddress;
                ContactURL = Row.contactURL;
                ImagePath = Row.imagePath;
            }
        }

        #endregion

        #region Data Adapter Functions

        private activityTableAdapter _Adapter = null;
        protected activityTableAdapter Adapter
        {
            get
            {
                if (_Adapter == null)
                    _Adapter = new activityTableAdapter("shilohConnectionString");

                return _Adapter;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Shiloh.activityDataTable GetAllActivities()
        {
            return Adapter.GetActivity();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public Shiloh.activityDataTable GetActivityByID(int ID)
        {
            return Adapter.GetActivityById(ID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public Shiloh.activityDataTable GetByMonth(int Month, int Year)
        {
            DateTime startDate = new DateTime(Year, Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddSeconds(-1.0);

            List<Activity> activities = new List<Activity>();

            // get activities by month
            Shiloh.activityDataTable dtActivity = Adapter.GetActivityByDateRange(startDate, endDate);

            return dtActivity;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        internal bool AddActivity()
        {
            int rowsAffected = Adapter.InsertActivity(
               Name, Tag, StartDateTime, EndDateTime, Frequency, Description, Location,
               Address, City, State, ZIPcode, ContactName, ContactEmail, ContactPhone,
               HostId, ContactURL, ImagePath);

            // Return true if precisely one row was inserted, otherwise false
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        internal bool UpdateActivity()
        {
            Shiloh.activityDataTable dtActivity = Adapter.GetActivityById(Id);
            if (dtActivity.Count == 0)
                return false;

            Shiloh.activityRow activity = dtActivity[0];
            
            activity.activityEnd = EndDateTime;
            activity.activityStart = StartDateTime;
            activity.description = Description;
            activity.frequency = Frequency;
            activity.hostCommunityID = HostId;
            activity.name = Name;
            activity.tag = Tag;
            activity.location = Location;
            activity.state = State;
            activity.address = Address;
            activity.city = City;
            activity.state = State;
            activity.zipCode = ZIPcode;
            activity.contactName = ContactName;
            activity.contactPhone = ContactPhone;
            activity.contactEmailAddress = ContactEmail;
            activity.contactURL = ContactURL;
            activity.imagePath = ImagePath;

            //dtActivity.AddactivityRow(activity);
            int rowsAffected = Adapter.Update(activity);

            // Return true if precisely one row was inserted, otherwise false
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        internal bool DeleteActivity(int ID)
        {
            int rowsAffected = Adapter.DeleteActivityById(ID);

            // Return true if precisely one row was deleted, otherwise false
            return rowsAffected == 1;
        }

        #endregion
    }
}
