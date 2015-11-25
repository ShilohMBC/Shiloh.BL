using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL.ShilohTableAdapters;

namespace Shiloh.BL
{
    public class Member
    {
        #region Constructors

        public Member()
        {
            InitObject();
        }

        public Member(int MemberId)
        {            
            InitObject();
            _Id = MemberId;
            Get();
        }

        #endregion

        #region Properties

        int _Id;
        public int Id
        {
            get { return _Id; }
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
        public int Status { get; set; }
        public DateTime AnniversaryDate { get; set; }

        int _WebAccountId;
        WebAccount _WebAccount;
        public WebAccount WebAccount
        {
            get { return _WebAccount; }
            set { _WebAccount = value; }
        }

        char _Sex;
        public enum eSex { Female = 1, Male = 2 }
        public eSex Sex
        {
            get { return (_Sex == 'M') ? eSex.Male : eSex.Female; }
            set { _Sex = (value == eSex.Male) ? 'M' : 'F'; }
        }

        public DateTime BirthDate { get; set; }
        public string Occupation { get; set; }

        int _AddressId;
        Address _Address = null;
        public Address Address
        {
            get
            {
                if (_Address == null && _AddressId > 0)
                {
                    _Address = new Address(_AddressId);
                }

                return _Address;
            }

            set
            {
                _Address = value;
            }
        }

        int _EmergencyAddressId;
        Address _EmergencyAddress = null;
        public Address EmergencyAddress
        {
            get
            {
                if (_EmergencyAddress == null && _EmergencyAddressId > 0)
                {
                    _EmergencyAddress = new Address(_EmergencyAddressId);
                }

                return _EmergencyAddress;
            }

            set
            {
                _EmergencyAddress = value;
            }
        }

        public string PhoneWork { get; set; }
        public string PhoneMobile { get; set; }
        public string Email { get; set; }
        public string EmailWork { get; set; }
        public string Notes { get; set; }

        DateTime _DateCreated;
        public DateTime DateCreated
        {
            get { return _DateCreated; }
        }

        #endregion

        #region Methods

        protected void InitObject()
        {
            _Id = 0;
            _Sex = ' ';
            _WebAccount = null;
            _WebAccountId = 0;
            LastName = string.Empty;
            FirstName = string.Empty;
            IsActive = false;
            Status = 0;
            AnniversaryDate = DateTime.MinValue;
            BirthDate = DateTime.MinValue;
            _DateCreated = DateTime.MinValue;
            Occupation = string.Empty;
            _Address = null;
            _AddressId = -1;
            _EmergencyAddress = null;
            _EmergencyAddressId = -1;
            PhoneMobile = string.Empty;
            PhoneWork = string.Empty;
            Email = string.Empty;
            EmailWork = string.Empty;
            Notes = string.Empty;
        }

        internal void CopyRowDataToObject(Shiloh.memberRow Row)
        {
            if (Row != null)
            {
                _Id = Row.ID;
                _Sex = Row.sex[0];
                _WebAccount = null;
                _WebAccountId = (!Row.IswebAccountIdNull()) ? Row.webAccountId : -1;
                LastName = Row.lastName;
                FirstName = Row.firstName;
                IsActive = Row.isActive;
                Status = Row.statusId;
                AnniversaryDate = Row.anniverisaryDate;
                BirthDate = Row.birthDate;
                _DateCreated = Row.dateCreated;
                Occupation = Row.occupation;
                _Address = null;
                _AddressId = (!Row.IsaddressIdNull()) ? Row.addressId : -1;
                _EmergencyAddress = null;
                _EmergencyAddressId = (!Row.IsemergencyAddressIdNull()) ? Row.emergencyAddressId : -1;
                PhoneMobile = Row.phoneMobile;
                PhoneWork = Row.phoneWork;
                Email = Row.email;
                EmailWork = Row.emailWork;
                Notes = Row.notes;
            }
        }

        internal void Get()
        {
            if (Id > 0)
            {
                Shiloh.memberDataTable dtMembers = GetMemberById(Id);
                if (dtMembers != null && dtMembers.Rows.Count > 0)
                {
                    CopyRowDataToObject((Shiloh.memberRow)dtMembers.Rows[0]);
                }
            }
        }

        public bool Save()
        {
            bool saved = false;

            if (Id > 0)
                saved = UpdateMember();
            else
                saved = AddMember();

            return saved;
        }

        public bool Delete()
        {
            return DeleteMember(Id);
        }

        #endregion

        #region DataAdapter Methods

        private memberTableAdapter _Adapter = null;
        protected memberTableAdapter Adapter
        {
            get
            {
                if (_Adapter == null)
                    _Adapter = new memberTableAdapter("shilohConnectionString");

                return _Adapter;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Shiloh.memberDataTable GetAll()
        {
            return Adapter.GetMembers();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        internal Shiloh.memberDataTable GetMemberById(int ID)
        {
            return Adapter.GetMemberById(ID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        internal bool AddMember()
        {
            bool saved = true;

            try
            {
                // save address
                if (_Address != null)
                {
                    saved = _Address.Save();

                    if (saved)
                        _AddressId = _Address.Id;
                }
                
                // save emergency address
                if (saved && _EmergencyAddress != null)
                {
                    saved = _EmergencyAddress.Save();

                    if (saved)
                        _EmergencyAddressId = _EmergencyAddress.Id;

                }

                // save web account info
                if (saved && _WebAccount != null)
                {
                    saved = _WebAccount.Save();

                    if (saved)
                        _WebAccountId = _WebAccount.Id;
                }

                // save the member
                if (saved)
                {
                    object newId = Adapter.InsertMemeber(LastName, FirstName, IsActive, Status, AnniversaryDate, ((_WebAccountId > 0) ? (int?)_WebAccountId : null),
                        _Sex.ToString(), BirthDate, Occupation, ((_AddressId > 0) ? (int?)_AddressId : null), ((_EmergencyAddressId > 0) ? (int?)_EmergencyAddressId : null),
                        PhoneWork, PhoneMobile, Email, EmailWork, Notes, DateTime.Now);

                    if (newId != null)
                        _Id = (int)newId;

                    // Return true if precisely one row was inserted, otherwise false
                    saved = (_Id > 0);
                }
            }
            catch (Exception ex)
            {
                if (_Address != null && _Address.Id > 0)
                    _Address.Delete();

                if (_EmergencyAddress != null && _EmergencyAddress.Id > 0)
                    _EmergencyAddress.Delete();

                if (_WebAccount != null && _WebAccount.Id > 0)
                    _WebAccount.Delete();
            }

            return saved;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        internal bool UpdateMember()
        {
            Shiloh.memberDataTable dtMembers = Adapter.GetMemberById(Id);
            if (dtMembers.Count == 0)
                return false;

            // save off address
            if (_Address != null)
            {
                if (_Address.Save())
                    _AddressId = _Address.Id;                
            }

            // save off address
            if (_EmergencyAddress != null)
            {
                if (_EmergencyAddress.Save())
                    _EmergencyAddressId = _EmergencyAddress.Id;
            }

            // save off webaccount
            if (_WebAccount != null)
            {
                if (_WebAccount.Save())
                    _WebAccountId = _WebAccount.Id;
            }

            Shiloh.memberRow member = dtMembers[0];

            member.dateCreated = DateCreated;
            member.addressId = ((_AddressId > 0) ? _AddressId : member.addressId);
            member.anniverisaryDate = AnniversaryDate;
            member.birthDate = BirthDate;
            member.email = Email;
            member.emailWork = EmailWork;
            member.emergencyAddressId = ((_EmergencyAddressId > 0) ? _EmergencyAddressId : member.emergencyAddressId);
            member.firstName = FirstName;
            member.isActive = IsActive;
            member.lastName = LastName;
            member.notes = Notes;
            member.occupation = Occupation;
            member.phoneMobile = PhoneMobile;
            member.phoneWork = PhoneWork;
            member.sex = Sex.ToString();
            member.statusId = Status;
            member.webAccountId = ((_WebAccountId > 0) ? _WebAccountId : member.webAccountId);

            int rowsAffected = Adapter.Update(member);

            // Return true if precisely one row was inserted, otherwise false
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        internal bool DeleteMember(int ID)
        {
            bool deleted = false;

            deleted = ((int)Adapter.DeleteMember(ID) == 1);

            return deleted;
        }

        #endregion
    }
}
