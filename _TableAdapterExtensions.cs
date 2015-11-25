using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shiloh.BL.ShilohTableAdapters
{
    public partial class activityTableAdapter 
    {
        public activityTableAdapter(string ConnectionStringName)
        {
            string connString = global::Shiloh.BL.Properties.Settings.Default.shilohWebConnectionString;

            if (System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName] != null)
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();
                        
            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = connString;
        }
    }


    public partial class addressTableAdapter
    {
        public addressTableAdapter(string ConnectionStringName)
        {
            string connString = global::Shiloh.BL.Properties.Settings.Default.shilohWebConnectionString;

            if (System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName] != null)
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();

            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = connString;
        }
    }

    public partial class memberActivityLogTableAdapter
    {
        public memberActivityLogTableAdapter(string ConnectionStringName)
        {
            string connString = global::Shiloh.BL.Properties.Settings.Default.shilohWebConnectionString;

            if (System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName] != null)
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();

            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = connString;
        }
    }

    public partial class memberStatusTableAdapter
    {
        public memberStatusTableAdapter(string ConnectionStringName)
        {
            string connString = global::Shiloh.BL.Properties.Settings.Default.shilohWebConnectionString;

            if (System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName] != null)
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();

            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = connString;
        }
    }

    public partial class memberTableAdapter
    {
        public memberTableAdapter(string ConnectionStringName)
        {
            string connString = global::Shiloh.BL.Properties.Settings.Default.shilohWebConnectionString;

            if (System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName] != null)
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();

            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = connString;
        }
    }

    public partial class mediaCategoryTableAdapter
    {
        public mediaCategoryTableAdapter(string ConnectionStringName)
        {
            string connString = global::Shiloh.BL.Properties.Settings.Default.shilohWebConnectionString;

            if (System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName] != null)
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();

            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = connString;
        }
    }

    public partial class mediaProviderTableAdapter
    {
        public mediaProviderTableAdapter(string ConnectionStringName)
        {
            string connString = global::Shiloh.BL.Properties.Settings.Default.shilohWebConnectionString;

            if (System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName] != null)
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();

            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = connString;
        }
    }

    public partial class mediaTableAdapter
    {
        public mediaTableAdapter(string ConnectionStringName)
        {
            string connString = global::Shiloh.BL.Properties.Settings.Default.shilohWebConnectionString;

            if (System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName] != null)
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();

            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = connString;
        }
    }

    public partial class prayerRequestTableAdapter
    {
        public prayerRequestTableAdapter(string ConnectionStringName)
        {
            string connString = global::Shiloh.BL.Properties.Settings.Default.shilohWebConnectionString;

            if (System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName] != null)
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();

            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = connString;
        }
    }

    public partial class prayerResponseTableAdapter
    {
        public prayerResponseTableAdapter(string ConnectionStringName)
        {
            string connString = global::Shiloh.BL.Properties.Settings.Default.shilohWebConnectionString;

            if (System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName] != null)
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();

            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = connString;
        }
    }

    public partial class webAccountTableAdapter
    {
        public webAccountTableAdapter(string ConnectionStringName)
        {
            string connString = global::Shiloh.BL.Properties.Settings.Default.shilohWebConnectionString;

            if (System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName] != null)
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();

            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = connString;
        }
    }

    public partial class scriptureOfTheDayTableAdapter
    {
        public scriptureOfTheDayTableAdapter(string ConnectionStringName)
        {
            string connString = global::Shiloh.BL.Properties.Settings.Default.shilohWebConnectionString;

            if (System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName] != null)
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();

            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = connString;
        }
    }

    public partial class bibleKJVTableAdapter
    {
        public bibleKJVTableAdapter(string ConnectionStringName)
        {
            string connString = global::Shiloh.BL.Properties.Settings.Default.shilohWebConnectionString;

            if (System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName] != null)
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();

            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = connString;
        }
    }

    public partial class bibleBookTableAdapter
    {
        public bibleBookTableAdapter(string ConnectionStringName)
        {
            string connString = global::Shiloh.BL.Properties.Settings.Default.shilohWebConnectionString;

            if (System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName] != null)
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();

            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = connString;
        }
    }


    public partial class ministryEntryTableAdapter
    {
        public ministryEntryTableAdapter(string ConnectionStringName)
        {
            string connString = global::Shiloh.BL.Properties.Settings.Default.shilohWebConnectionString;

            if (System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName] != null)
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();

            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = connString;
        }
    }

    public partial class ministryTableAdapter
    {
        public ministryTableAdapter(string ConnectionStringName)
        {
            string connString = global::Shiloh.BL.Properties.Settings.Default.shilohWebConnectionString;

            if (System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName] != null)
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();

            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = connString;
        }
    }
}
