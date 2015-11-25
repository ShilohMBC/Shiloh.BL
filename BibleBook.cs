using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL;
using Shiloh.BL.ShilohTableAdapters;

namespace Shiloh.BL
{
    public class BibleBook
    {
        public BibleBook() : this(0)
        {
        }

        public BibleBook(int BookId)
        {            
            InitObject();
            _Id = BookId;
            Get();
        }

        internal BibleBook(Shiloh.bibleBookRow Row)
        {
            InitObject();
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

        #endregion

        #region Methods

        private void InitObject()
        {
            name = string.Empty;
            description = string.Empty;
        }

        public void Get()
        {
            if (Id > 0)
            {
                Shiloh.bibleBookDataTable dtTable = GetByBookId(Id);

                if (dtTable != null && dtTable.Rows.Count > 0)
                {
                    Shiloh.bibleBookRow row = dtTable.Rows[0] as Shiloh.bibleBookRow;

                    if (row != null)
                    {
                        CopyRowToObject(row);
                    }
                    else
                        InitObject();
                }
            }
        }

        internal void CopyRowToObject(Shiloh.bibleBookRow Row)
        {
            if (Row != null)
            {
                _Id = Row.bookId;
                name = Row.name;
                description = Row.description;
            }
        }

        #endregion

        #region DataAdapter Methods

        private bibleBookTableAdapter _Adapter = null;
        protected bibleBookTableAdapter Adapter
        {
            get
            {
                if (_Adapter == null)
                    _Adapter = new bibleBookTableAdapter("shilohConnectionString");

                return _Adapter;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Shiloh.bibleBookDataTable GetAllBooks()
        {
            return Adapter.GetAllBooks();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public Shiloh.bibleBookDataTable GetByBookId(int ID)
        {
            return Adapter.GetById(ID);
        }

        #endregion

    }
}
