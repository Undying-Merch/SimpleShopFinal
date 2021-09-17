using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShopModels;
using SimpleShopORM.DBConnection;

namespace SimpleShopORM.ORM
{
    class ORM_Store
    {
        readonly SqlConnection Conn;
        DB_Connection db = new();

        public ORM_Store()
        {
            Conn = db.Connect;
        }

        public string AddDepartmentToStore(int storeId, Department department)
        {
            string status = "";

            string query = "INSERT INTO Store_Has_Departments(Store_ID, Department_ID) " +
                "VALUES(@storeId, @departmentId);";

            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@storeId", storeId);
            cmd.Parameters.AddWithValue("@departmentId", department.DepartmentId);

            status = db.DBConnAction(status, cmd);

            return status;
        }
        public Store CreateStore(Store store)
        {
            string query = "INSERT INTO Stores(Store_name, Store_address, Zip_Code) " +
                "VALUES(@name, @address, @zipCode);" +
                "SELECT SCOPE_IDENTITY() AS id;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@name", store.StoreName);
            cmd.Parameters.AddWithValue("@address", store.StoreAddress);
            cmd.Parameters.AddWithValue("@zipCode", store.ZipCode);

            store.AssignId(db.DBConnAction(cmd));
            return store;
        }
        public string DeleteStore(int id)
        {
            string status = "";

            string query = "DELETE FROM Stores " +
                "WHERE Stores.Store_ID = @id;";

            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@id", id);

            status = db.DBConnAction(status, cmd);
            return status;
        }
        public Store GetStore(int id)
        {
            Store store = null;

            string query = "SELECT Store_ID, Store_name, Store_address, Zip_Code, PostCodes.City_name FROM Stores " +
                "INNER JOIN PostCodes ON Stores.Zip_Code = PostCodes.Zip_Code " +
                "WHERE Store_ID = @id;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@id", id);

            if (Conn.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    Conn.Open();
                }
                catch (Exception ex)
                {
                    throw new(ex.Message);
                }
            }
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            int x = 0;

            while (reader.Read())
            {
                store = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4));
                x++;
            }
            reader.Close();
            if (x != 1) return null;

            return store;
        }
        public List<Store> GetStores()
        {
            List<Store> stores = new();

            string query = "SELECT Store_ID, Store_name, Store_address, Zip_Code, PostCodes.City_name FROM Stores " +
                "INNER JOIN PostCodes ON Stores.Zip_Code = PostCodes.Zip_Code;";
            SqlCommand cmd = new(query, Conn);

            if (Conn.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    Conn.Open();
                }
                catch (Exception ex)
                {
                    throw new(ex.Message);
                }
            }
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Store store = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4));
                stores.Add(store);
            }
            reader.Close();

            return stores;
        }
        public string RemoveDepartmentFromStore(int id)
        {
            string status = "";
            string query = "DELETE FROM Store_Has_Departments " +
                "WHERE ID = @Id;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@orderId", id);

            status = db.DBConnAction(status, cmd);
            return status;
        }
        public Store SetStore(Store store)
        {
            string query = "UPDATE Stores " +
                "SET " +
                "Store_name = @name, " +
                "Store_address = @address, " +
                "Zip_Code = @zipCode " +
                "WHERE Employees.Employee_ID = @id;";

            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@name", store.StoreName);
            cmd.Parameters.AddWithValue("@address", store.StoreAddress);
            cmd.Parameters.AddWithValue("@zipCode", store.ZipCode);
            cmd.Parameters.AddWithValue("@id", store.StoreId);

            if (Conn.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    Conn.Open();
                }
                catch (Exception ex)
                {
                    throw new(ex.Message);
                }
            }
            int row = cmd.ExecuteNonQuery();
            Conn.Close();
            if (row == 1)
            {
                return store;
            }
            else
            {
                return null;
            }
        }
    }
}
