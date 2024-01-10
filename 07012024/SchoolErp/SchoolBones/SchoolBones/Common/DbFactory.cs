using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Focus8W.BL
{
    public class DbFactory
    {
        string mExceptionMessage;
        public string ExceptionMessage { get { return mExceptionMessage; } }
        public bool HasException { get; set; }

        bool _databaseExists = false;

        static string databaseServer = @"MOHD-SHEEBU\SQLSERVER2019";
        static string masterDefaultCatalog = "Master";
        string _masterConnectionString = $"Data Source={databaseServer};Initial Catalog={masterDefaultCatalog};Integrated Security=True";
        public string MasterConnectionString
        {
            get
            {
                return _masterConnectionString;
            }
            set
            {
                _masterConnectionString = value;
            }
        }

        static string DefaultCatalog = "MyDb";
        string _ConnectionString = $"Data Source={databaseServer};Initial Catalog={DefaultCatalog};Integrated Security=True";
        string ConnectionString
        {
            get
            {
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
        }

        public bool CheckDatabase()
        {

            try
            {
                using (SqlConnection conn = new SqlConnection() { ConnectionString = MasterConnectionString })
                {
                    using (SqlCommand cmd = new SqlCommand($"IF EXISTS (SELECT name FROM sys.databases WHERE name = '{DefaultCatalog}') SELECT 1 ELSE SELECT 0", conn))
                    {
                        conn.Open();
                        int value = (int)cmd.ExecuteScalar();
                        conn.Close();

                        if (value != 1)
                        {
                            CreateDatabase();
                            _databaseExists = true;
                        }
                        else
                        {
                            _databaseExists = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                mExceptionMessage = ex.Message;
                _databaseExists = false;
                HasException = true;
            }

            return _databaseExists;

        }
        bool CreateDatabase()
        {
            string tableCreateScript = $@"
            USE [{DefaultCatalog}]
            CREATE TABLE [dbo].[Article](
                [ItemName] [nvarchar](50) NOT NULL,
                [Barcode] [nvarchar](50) NOT NULL,
                [Price] [money] NOT NULL,
             CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED
            (
                [Barcode] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY];
            CREATE TABLE [dbo].[Worker](
                [FirstName] [nvarchar](50) NOT NULL,
                [Password] [nvarchar](50) NOT NULL,
                [LastName] [nvarchar](50) NOT NULL,
            PRIMARY KEY CLUSTERED
            (
                [Password] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY];";

            using (SqlConnection conn = new SqlConnection() { ConnectionString = MasterConnectionString })
            {
                try
                {
                    using (SqlCommand comm = new SqlCommand($"CREATE DATABASE [{DefaultCatalog}];", conn))
                    {
                        conn.Open();

                        comm.ExecuteNonQuery();

                        comm.CommandText = tableCreateScript;
                        comm.ExecuteNonQuery();

                        return true;

                    }
                }
                catch (Exception ex)
                {
                    mExceptionMessage = ex.Message;
                    HasException = true;
                    return false;
                }

            }

        }

        //Db Operation
        
    }
}


