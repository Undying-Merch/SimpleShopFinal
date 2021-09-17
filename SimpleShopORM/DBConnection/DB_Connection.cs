using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShopORM.DBConnection
{
    class DB_Connection
    {
        private string host = "localhost";
        private string username = "sa";
        private string password = "Admin1234";
        private string database = "SimpleShopData";

        public SqlConnection Connect { get; set; }

        public DB_Connection()
        {
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder()
            {
                InitialCatalog = database,
                UserID = username,
                Password = password,
                DataSource = host
            };

            Connect = new SqlConnection(connString.ToString());
        }

        public int DBConnAction(SqlCommand cmd)
        {
            if (Connect.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    Connect.Open();
                }
                catch (Exception ex)
                {
                    throw new(ex.Message);
                }
            }

            int type = Convert.ToInt32(cmd.ExecuteScalar());
            Connect.Close();
            return type;
        }
        public string DBConnAction(string type, SqlCommand cmd)
        {
            if (Connect.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    Connect.Open();
                }
                catch (Exception ex)
                {
                    throw new(ex.Message);
                }
            }

            int rowChanged = cmd.ExecuteNonQuery();
            Connect.Close();

            if (rowChanged != 1)
            {
                type = "Something went wrong";
            }
            else if (rowChanged == 1)
            {
                type = "Operation Succesful";
            }
            return type;
        }
    }
}
