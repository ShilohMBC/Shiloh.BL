using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL.ShilohTableAdapters;

namespace Shiloh.BL
{
    public class Address
    {
        #region Constructors

        public Address()
        {
            InitObject();
        }

        public Address(int AddressId)
        {
            InitObject();
            _Id = AddressId;
            Get();
        }

        #endregion

        #region Properties

        int _Id;
        public int Id
        {
            get { return _Id; }
        }

        public string ContactName
        {
            get;
            set;
        }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        DateTime _DateCreated;
        public DateTime DateCreated
        {
            get { return _DateCreated; }
        }

        #endregion

        #region Methods

        private void InitObject()
        {
            _Id = 0;
            _DateCreated = DateTime.MinValue;
            ContactName = string.Empty;
            StreetAddress = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Zip = string.Empty;
            Country = string.Empty;
            Phone = string.Empty;
            Fax = string.Empty;
        }

        internal void CopyRowDataToObject(Shiloh.addressRow Row)
        {
            if (Row != null)
            {
                _DateCreated = Row.dateCreated;
                _Id = Row.ID;
                this.City = Row.city;
                this.Country = Row.country;
                this.Fax = Row.fax;
                this.Phone = Row.phone;
                this.State = Row.state;
                this.ContactName = Row.contactName;
                this.StreetAddress = Row.streetAddress;
                this.Zip = Row.zipCode;
            }
        }

        internal void Get()
        {
            if (Id > 0)
            {
                Shiloh.addressDataTable dtAddresses = GetAddressById(Id);
                if (dtAddresses != null && dtAddresses.Rows.Count > 0)
                {
                    CopyRowDataToObject((Shiloh.addressRow)dtAddresses.Rows[0]);
                }
            }
        }

        public bool Save()
        {
            bool saved = false;

            if (Id > 0)
                saved = UpdateAddress();
            else
                saved = AddAddress();

            return saved;
        }

        public bool Delete()
        {
            return DeleteAddress(Id);
        }

        public int GetCount()
        {
            return GetCount(_Id);
        }

        protected int GetCount(int ID)
        {
            int? count = _Adapter.GetAddressCountById(ID);
            return (count != null) ? (int)count : 0;
        }

        #endregion

        #region DataAdapter Methods

        private addressTableAdapter _Adapter = null;
        protected addressTableAdapter Adapter
        {
            get
            {
                if (_Adapter == null)
                    _Adapter = new addressTableAdapter("shilohConnectionString");

                return _Adapter;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Shiloh.addressDataTable GetAll()
        {
            return Adapter.GetAddresses();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        internal Shiloh.addressDataTable GetAddressById(int ID)
        {
            return Adapter.GetAddressById(ID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        internal bool AddAddress()
        {
            bool saved = false;

            object retId = Adapter.InsertAddress(ContactName, StreetAddress, City, State, Zip, Country, Phone, Fax, DateTime.Now);

            if (retId != null)
                _Id = (int)retId;

            saved = (_Id > 0);

            return saved;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        internal bool UpdateAddress()
        {
            Shiloh.addressDataTable dtAddresses = Adapter.GetAddressById(Id);
            if (dtAddresses.Count == 0)
                return false;

            Shiloh.addressRow address = dtAddresses[0];

            address.dateCreated = DateCreated;
            address.city = this.City;
            address.contactName = this.ContactName;
            address.country = this.Country;
            address.fax = this.Fax;
            address.phone = this.Phone;
            address.state = this.State;
            address.streetAddress = this.StreetAddress;
            address.zipCode = this.Zip;

            int rowsAffected = Adapter.Update(address);

            // Return true if precisely one row was inserted, otherwise false
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        internal bool DeleteAddress(int ID)
        {
            bool deleted = true;

            if (ID > 0)
            {
                int rowsAffected = Adapter.DeleteAddress(ID);

                // Return true if precisely one row was deleted, otherwise false
                deleted = (rowsAffected == 1);
            }
            
            return deleted;
        }

        #endregion
    }
}