using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL;
using Shiloh.BL.ShilohTableAdapters;

namespace Shiloh.BL
{
    public class BibleKJV
    {
        public BibleKJV() : this(0)
        {
        }

        public BibleKJV(int VerseId)
        {
            InitObject();
            _Id = VerseId;            
        }

        public BibleKJV(int BibleBookId, int Chapter, int Verse)
        {
            InitObject();
            Get(BibleBookId, Chapter, Verse);
        }

        public BibleKJV(Shiloh.bibleKJVRow Row)
        {
            InitObject();
            CopyRowToObject(Row);
            GetBookName(Row);
        }

        #region Properties

        int _Id;
        public int Id
        {
            get { return _Id; }
        }

        public int bookId {get; set;}

        string _book = string.Empty;
        public string book
        {
            get
            {
                return _book;
            }
        }

        public int chapter {get; set;}
        public int verse {get; set;}

        string _verseText = string.Empty;
        public string verseText
        {
            get
            {
                return _verseText;
            }
        }

        #endregion

        #region Methods

        private void InitObject()
        {
            _verseText = string.Empty;
            _book = string.Empty;
        }

        public void Get()
        {
            if (Id > 0)
            {
                Shiloh.bibleKJVDataTable dtTable = GetVerseByID(Id);
                LoadVerseData(dtTable);       
            }
        }

        public void Get(int BookId, int Chapter, int Verse)
        {
            Shiloh.bibleKJVDataTable dtTable = GetByBookChapterVerse(BookId,Chapter,Verse);
            LoadVerseData(dtTable);            
        }

        private void LoadVerseData(Shiloh.bibleKJVDataTable BibleTable)
        {
            if (BibleTable != null && BibleTable.Rows.Count > 0)
            {
                Shiloh.bibleKJVRow row = BibleTable.Rows[0] as Shiloh.bibleKJVRow;

                CopyRowToObject(row);
                GetBookName(row);
            }
            else
                InitObject();
        }

        internal void CopyRowToObject(Shiloh.bibleKJVRow Row)
        {
            if (Row != null)
            {
                _Id = Row.verseId;
                bookId = Row.bookId;
                chapter = Row.chapter;
                verse = Row.verse;
                _verseText = Row.verseText;
            }
        }

        private void GetBookName(Shiloh.bibleKJVRow Row)
        {
            _book = string.Empty;

            if (Row != null)
            {
                if (Row.bookId > 0)
                {
                    BibleBook bk = new BibleBook(Row.bookId);
                    _book = bk.name;
                }
            }
        }

        #endregion

        #region DataAdapter Methods

        private bibleKJVTableAdapter _Adapter = null;
        protected bibleKJVTableAdapter Adapter
        {
            get
            {
                if (_Adapter == null)
                    _Adapter = new bibleKJVTableAdapter("shilohConnectionString");

                return _Adapter;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Shiloh.bibleKJVDataTable GetWholeBible()
        {
            return Adapter.GetBible();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        protected Shiloh.bibleKJVDataTable GetVerseByID(int ID)
        {
            return Adapter.GetVerseById(ID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        protected Shiloh.bibleKJVDataTable GetByBookChapterVerse(int BibleBookId, int Chapter, int Verse)
        {
            Shiloh.bibleKJVDataTable dtTable = Adapter.GetByBookChapterVerse(BibleBookId, Chapter, Verse);           

            return dtTable;
        }       

        #endregion
    }
}
