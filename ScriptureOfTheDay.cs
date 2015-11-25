using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL;
using Shiloh.BL.ShilohTableAdapters;

namespace Shiloh.BL
{
    #region ScriptureOfTheDay

    public class ScriptureOfTheDay
    {
        public enum eBibleVersion { KJV, NLT, English }

        public ScriptureOfTheDay() :this(0)
        {
            
        }

        public ScriptureOfTheDay(int SODid)
        {
            InitObject();
            _Id = SODid;
            Get();
        }

        public ScriptureOfTheDay(DateTime SODdate)
        {
            InitObject();
            Get(SODdate);
        }


        public ScriptureOfTheDay(Shiloh.scriptureOfTheDayRow Row)
        {
            InitObject();
            CopyRowToObject(Row);
            GetVerseText(Row);
        }

        #region Properties

        int _Id;
        public int Id
        {
            get { return _Id; }
        }
        public eBibleVersion bibleVersion { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }

        string _book = string.Empty;
        public string book
        {
            get { return _book; }
        }

        public int bibleBookId { get; set; }
        public int chapter { get; set; }
        public int verse { get; set; }
        
        string _scripture = string.Empty;
        public string scripture
        {
            get
            {
                return _scripture;
            }
        }

        #endregion

        #region Methods

        public void Get()
        {            
            if (Id > 0)
            {
                Shiloh.scriptureOfTheDayDataTable dtSOD = GetSODByID(Id);
                LoadScriptureData(dtSOD);
            }
        }

        protected void Get(DateTime SOD_Date)
        {
            
            {
                Shiloh.scriptureOfTheDayDataTable dtSOD = GetByDate(SOD_Date);
                LoadScriptureData(dtSOD);
            }
        }

        private void LoadScriptureData(Shiloh.scriptureOfTheDayDataTable SciptureTable)
        {
            if (SciptureTable != null && SciptureTable.Rows.Count > 0)
            {
                Shiloh.scriptureOfTheDayRow kjvRow = SciptureTable.Rows[0] as Shiloh.scriptureOfTheDayRow;

                if (kjvRow != null)
                {
                    CopyRowToObject(kjvRow);
                    GetVerseText(kjvRow);
                }
            }            
        }

        protected void InitObject()
        {
            bibleVersion = eBibleVersion.KJV;
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            day = DateTime.Now.Day;
        }

        internal void CopyRowToObject(Shiloh.scriptureOfTheDayRow Row)
        {
            if (Row != null)
            {
                _Id = Row.sodId;
                year = Row.year;
                month = Row.month;
                day = Row.day;
                bibleBookId = Row.bibleBookId;
                chapter = Row.chapter;
                verse = Row.verse;
            }
        }

        private void GetVerseText(Shiloh.scriptureOfTheDayRow Row)
        {
            _book = _scripture = string.Empty;
            
            if (Row != null)
            {
                if (bibleVersion == eBibleVersion.KJV)
                {
                    BibleKJV kjv = new BibleKJV(Row.bibleBookId, Row.chapter, Row.verse);

                    if (!string.IsNullOrEmpty(kjv.verseText))
                    {
                        _book = kjv.book;
                        _scripture = kjv.verseText;
                    }
                }
            }
        }

        #endregion

        #region Data Adapter Functions

        private scriptureOfTheDayTableAdapter _Adapter = null;
        protected scriptureOfTheDayTableAdapter Adapter
        {
            get
            {
                if (_Adapter == null)
                    _Adapter = new scriptureOfTheDayTableAdapter("shilohConnectionString");

                return _Adapter;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Shiloh.scriptureOfTheDayDataTable GetAllSODs()
        {
            return Adapter.GetAllScripturesOfTheDay();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        protected Shiloh.scriptureOfTheDayDataTable GetSODByID(int ID)
        {
            return Adapter.GetSODbyId(ID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        internal Shiloh.scriptureOfTheDayDataTable GetByDate(DateTime SODdate)
        {
            return GetByDate(SODdate.Year, SODdate.Month, SODdate.Day);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        internal Shiloh.scriptureOfTheDayDataTable GetByDate(int Year, int Month, int Day)
        {
            Shiloh.scriptureOfTheDayDataTable dtTable = Adapter.GetByDate(Year, Month, Day);

            if (dtTable != null && dtTable.Rows.Count > 0)
            {
                GetVerseText((Shiloh.scriptureOfTheDayRow)dtTable.Rows[0]);
            }

            return dtTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        internal bool AddSOD()
        {
            int rowsAffected = Adapter.Insert(year, month, day, bibleBookId, chapter, verse);

            // Return true if precisely one row was inserted, otherwise false
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        internal bool UpdateSOD()
        {
            Shiloh.scriptureOfTheDayDataTable dtTable = Adapter.GetSODbyId(Id);
            if (dtTable.Count == 0)
                return false;

            Shiloh.scriptureOfTheDayRow row = dtTable[0];

            row.year = year;
            row.month = month;
            row.day = day;
            row.bibleBookId = bibleBookId;
            row.chapter = chapter;
            row.verse = verse;

            //dtActivity.AddactivityRow(activity);
            int rowsAffected = Adapter.Update(row);

            // Return true if precisely one row was inserted, otherwise false
            return rowsAffected == 1;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        internal bool DeleteSOD(int ID)
        {
            int rowsAffected = Adapter.DeleteById(Id);

            // Return true if precisely one row was deleted, otherwise false
            return rowsAffected == 1;
        }

        #endregion
    }

    #endregion

    #region ScripturesOfTheDay

    public class ScripturesOfTheDay : List<ScriptureOfTheDay>
    {
        public ScripturesOfTheDay(DateTime SODdate)
        {
            _SODdate = SODdate;
            Get();
        }


        DateTime _SODdate = DateTime.MinValue;
        public DateTime LookupDate
        {
            get
            {
                return _SODdate;
            }
        }

        protected void Get()
        {
            if (!_SODdate.Equals(DateTime.MinValue))
            {
                ScriptureOfTheDay sod = new ScriptureOfTheDay();

                Shiloh.scriptureOfTheDayDataTable scriptures = sod.GetByDate(_SODdate.Year, _SODdate.Month, _SODdate.Day);

                this.Clear();
                foreach (Shiloh.scriptureOfTheDayRow scripture in scriptures)
                {
                    this.Add(new ScriptureOfTheDay(scripture));
                }
            }
        }
    }

    #endregion
}
