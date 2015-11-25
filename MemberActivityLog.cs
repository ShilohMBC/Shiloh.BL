using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL.ShilohTableAdapters;

namespace Shiloh.BL
{
    #region MemberActivityLog

    public class MemberActivityLog
    {
        #region Constructors

        public MemberActivityLog()
        {
            InitObject();
        }

        internal MemberActivityLog(Shiloh.memberActivityLogRow Row)
        {
            InitObject();

            if (Row != null)
                CopyRowDataToObject(Row);
        }

        #endregion

        #region Properties

        internal int _MemberId;
        public int MemberId
        {
            get { return _MemberId; }
        }

        public DateTime ActionDate { get; set; }
        public string Action { get; set; }

        DateTime _DateCreated;
        public DateTime DateCreated
        {
            get { return _DateCreated; }
        }

        #endregion

        #region Methods

        private void InitObject()
        {
            _MemberId = 0;
            _DateCreated = DateTime.MinValue;
            ActionDate = DateTime.MinValue;
            Action = string.Empty;
        }

        internal void CopyRowDataToObject(Shiloh.memberActivityLogRow Row)
        {
            if (Row != null)
            {
                _DateCreated = Row.dateCreated;
                _MemberId = Row.memberId;
                Action = Row.action;
                ActionDate = Row.actionDate;
            }
        }

        public bool Save()
        {
            bool saved = false;

            if (MemberId == 0)
                saved = AddMemberLog();

            return saved;
        }

        #endregion

        #region DataAccess Methods

        private memberActivityLogTableAdapter _Adapter = null;
        protected memberActivityLogTableAdapter Adapter
        {
            get
            {
                if (_Adapter == null)
                    _Adapter = new memberActivityLogTableAdapter();

                return _Adapter;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        internal Shiloh.memberActivityLogDataTable GetLogsForMember(int MemberId)
        {
            return Adapter.GetMemberLogsByMemberId(MemberId);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        internal bool AddMemberLog()
        {
            int rowsAffected = Adapter.InsertMemberLog(MemberId, ActionDate, Action, DateTime.Now);

            // Return true if precisely one row was inserted, otherwise false
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        internal bool DeleteLogsForMember(int MemberId)
        {
            int rowsAffected = Adapter.DeleteMemberLogs(MemberId);

            // Return true if precisely one row was deleted, otherwise false
            return rowsAffected == 1;
        }

        #endregion
    }

    #endregion

    #region MemberActivityLogs

    public class MemberActivityLogs : List<MemberActivityLog>
    {
        public MemberActivityLogs()
        {
        }

        public MemberActivityLogs(int MemberId)
        {
        }

        static public MemberActivityLogs Get(int MemberId)
        {
            MemberActivityLogs logs = new MemberActivityLogs();
            MemberActivityLog _log = new MemberActivityLog();

            foreach (Shiloh.memberActivityLogRow row in _log.GetLogsForMember(MemberId))
                logs.Add(new MemberActivityLog(row));

            return logs;
        }

        public bool Delete(int MemberId)
        {
            bool deleted = true;

            MemberActivityLog _log = new MemberActivityLog();

            deleted = _log.DeleteLogsForMember(MemberId);

            return deleted;
        }
    }

    #endregion
}
