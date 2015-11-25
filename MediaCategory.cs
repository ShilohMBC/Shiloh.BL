using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL.ShilohTableAdapters;

namespace Shiloh.BL
{
    #region MemberSatus Class

    public class MediaCategory
    {
        #region Constructors

        public MediaCategory()
        {
        }

        public MediaCategory(int CategoryId)
        {
            _Id = CategoryId; 
            Get();
        }

        public MediaCategory(Shiloh.mediaCategoryRow Row)
        {
            LoadObjectFromRow(Row);
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

        private mediaCategoryTableAdapter _Adapter = null;
        protected mediaCategoryTableAdapter Adapter
        {
            get
            {
                if (_Adapter == null)
                    _Adapter = new mediaCategoryTableAdapter("shilohConnectionString");

                return _Adapter;
            }
        }

        internal void LoadObjectFromRow(Shiloh.mediaCategoryRow Row)
        {
            if (Row != null)
            {
                _Id = Row.Id;
                Name = Row.Name;
                Description = Row.Description;
            }
        }

        protected internal Shiloh.mediaCategoryDataTable GetAll()
        {
            return Adapter.GetMediaCategory();
        }

        protected void Get()
        {
            if (Id > 0)
            {
                Shiloh.mediaCategoryDataTable category = Adapter.GetMediaCategoryById(Id);

                if (category != null && category.Count > 0)
                {
                    LoadObjectFromRow((Shiloh.mediaCategoryRow)category.Rows[0]);
                }
            }
        }


        #endregion
    }

    #endregion

    #region MediaCategories

    public class MediaCategories : List<MediaCategory>
    {
        static public MediaCategories GetAll()
        {
            MediaCategories cats = new MediaCategories();

            MediaCategory _cats = new MediaCategory();

            foreach (Shiloh.mediaCategoryRow row in _cats.GetAll())
                cats.Add(new MediaCategory(row));

            return cats;
        }
    }

    #endregion
}
