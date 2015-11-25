using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL.ShilohTableAdapters;

namespace Shiloh.BL
{
    public class WebAccount
    {
        #region Constructors

        public WebAccount() : this(0)
        {
            
        }

        public WebAccount(int WebAccountId)
        {
            InitObject();
            _Id = WebAccountId;
            GetById();
        }

        #endregion

        #region Properties

        int _Id;
        public int Id
        {
            get { return _Id; }
        }
        public string LoginName { get; set; }

        string _Password;
        public string Password
        {
            set { _Password = value; }
        }

        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        internal int _AddressId;

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
        }

        public string Website { get; set; }
        public string SocialSites { get; set; }

        DateTime _DateCreated;
        public DateTime DateCreated
        {
            get { return _DateCreated; }
        }

        #endregion

        #region Methods

        private void InitObject()
        {
            _Password = string.Empty;
            LoginName = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            _AddressId = -1;
            _Address = null;
            Website = string.Empty;
            SocialSites = string.Empty;
            _DateCreated = DateTime.MinValue;            
        }

        internal void CopyRowDataToObject(Shiloh.webAccountRow Row)
        {
            if (Row != null)
            {
                _DateCreated = Row.dateCreated;
                _Id = Row.ID;
                this.Email = Row.emailAddress;
                this.FirstName = Row.firstName;
                this.IsActive = Row.isActive;
                this.IsAdmin = Row.isAdmin;
                this.LastName = Row.lastName;
                this.LoginName = Row.loginName;
                this.SocialSites = Row.socialSites;
                this.Website = Row.website;
                _AddressId = (!Row.IsaddressIdNull()) ? Row.addressId : -1;
            }
        }

        internal void GetById()
        {
            if (Id > 0)
            {
                Shiloh.webAccountDataTable dtAccounts = GetWebAccountById(Id);
                if (dtAccounts != null && dtAccounts.Rows.Count > 0)
                {
                    CopyRowDataToObject((Shiloh.webAccountRow)dtAccounts.Rows[0]);
                }
            }
        }

        public bool Login(string LoginName, string Password)
        {
            bool loggedIn = false;

            if (!string.IsNullOrEmpty(LoginName))
            {
                Shiloh.webAccountDataTable dtAccounts = GetWebAccountByLoginName(LoginName, Password);
                if (dtAccounts != null && dtAccounts.Rows.Count > 0)
                {
                    CopyRowDataToObject((Shiloh.webAccountRow)dtAccounts.Rows[0]);
                    loggedIn = true;
                }
            }

            return loggedIn;
        }

        public bool Save()
        {
            bool saved = false;

            if (Id > 0)
                saved = UpdateWebAccount();
            else
                saved = AddWebAccount();

            return saved;
        }

        public bool Delete()
        {
            return DeleteWebAccount(Id);
        }

        #endregion

        #region DataAdapter Methods

        private webAccountTableAdapter _Adapter = null;
        protected webAccountTableAdapter Adapter
        {
            get
            {
                if (_Adapter == null)
                    _Adapter = new webAccountTableAdapter("shilohConnectionString");

                return _Adapter;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Shiloh.webAccountDataTable GetAll()
        {
            return Adapter.GetWebAccounts();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        internal Shiloh.webAccountDataTable GetWebAccountById(int ID)
        {
            return Adapter.GetWebAccountById(ID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        internal Shiloh.webAccountDataTable GetWebAccountByLoginName(string LoginName, string Password)
        {
            return Adapter.GetWebAccountByLoginName(LoginName, Password);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        internal bool AddWebAccount()
        {
            int rowsAffected = Adapter.InsertWebAccount(LoginName, _Password, IsActive, IsAdmin, LastName, 
                FirstName, Email, null, Website, SocialSites, DateCreated);
                

            // Return true if precisely one row was inserted, otherwise false
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        internal bool UpdateWebAccount()
        {
            Shiloh.webAccountDataTable dtAccounts = Adapter.GetWebAccountById(Id);
            if (dtAccounts.Count == 0)
                return false;

            Shiloh.webAccountRow account = dtAccounts[0];

            account.isActive = IsActive;
            account.isAdmin = IsAdmin;
            account.emailAddress = Email;
            account.firstName = FirstName;
            account.lastName = LastName;
            account.loginName = LoginName;
            account.password = _Password;
            account.socialSites = SocialSites;
            account.website = Website;
            account.dateCreated = DateCreated;

            int rowsAffected = Adapter.Update(account);

            // Return true if precisely one row was inserted, otherwise false
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public bool UpdatePassword(string Password)
        {
            Shiloh.webAccountDataTable dtAccounts = Adapter.GetWebAccountById(Id);
            if (dtAccounts.Count == 0)
                return false;

            int rowsAffected = Adapter.UpdateWebAccountChangePassword(LoginName, Password);

            // Return true if precisely one row was inserted, otherwise false
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        internal bool DeleteWebAccount(int ID)
        {
            int rowsAffected = Adapter.DeleteWebAccountById(ID);

            // Return true if precisely one row was deleted, otherwise false
            return rowsAffected == 1;
        }

        #endregion
    }
}
