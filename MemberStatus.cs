using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL.ShilohTableAdapters;

namespace Shiloh.BL
{
    #region MemberSatus Class

    public class MemberStatus
    {
        #region Constructors
        
        public MemberStatus()
        {
        }

        public MemberStatus(Shiloh.memberStatusRow Row)
        {
            if (Row != null)
            {
                _Id = Row.ID;
                Name = Row.Name;
                Description = Row.Description;
            }
        }

        #endregion

        #region Properties

        internal int _Id;
        public int Id
        {
            get { return _Id; }
        }

        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        #region DataAccess Methods

        private memberStatusTableAdapter _Adapter = null;
        protected memberStatusTableAdapter Adapter
        {
            get
            {
                if (_Adapter == null)
                    _Adapter = new memberStatusTableAdapter("shilohConnectionString");

                return _Adapter;
            }
        }

        protected internal Shiloh.memberStatusDataTable GetAll()
        {
            return _Adapter.GetMemberStatuses();
        }

        #endregion
    }

    #endregion

    #region MemberStatuses

    public class MemberStatuses : List<MemberStatus>
    {
        static public MemberStatuses GetAll()
        {
            MemberStatuses stats = new MemberStatuses();

            MemberStatus _status = new MemberStatus();

            foreach (Shiloh.memberStatusRow row in _status.GetAll())
                stats.Add(new MemberStatus(row));

            return stats;
        }
    }

    #endregion

}
