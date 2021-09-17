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
    public class ORM_Customers : Interface.IORM_Customer
    {
        readonly SqlConnection Conn;
        DB_Connection db = new();

        public ORM_Customers()
        {
            Conn = db.Connect;
        }
        public Customer CreateCustomer(Customer customer)
        {
            string query = "INSERT INTO Customers(Customer_name, Customer_address, Customer_phone, Customer_email, Zip_Code) " +
                "VALUES(@name, @address, @phoneNumber, @email, @zipCode); SELECT SCOPE_IDENTITY() AS id;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@name", customer.CustomerName);
            cmd.Parameters.AddWithValue("@address", customer.CustomerAddress);
            cmd.Parameters.AddWithValue("@phoneNumber", customer.CustomerPhoneNumber);
            cmd.Parameters.AddWithValue("@email", customer.CustomerEmail);
            cmd.Parameters.AddWithValue("@zipCode", customer.ZipCode);

            customer.AssignId(db.DBConnAction(cmd));
            return customer;
        }
        public string DeleteCustomer(int Id)
        {
            string status = "";

            string query = "DELETE FROM Customers WHERE Customer_ID = @val;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@val", Id);

            status = db.DBConnAction(status, cmd);
            return status;
        }
        public Customer GetCustomer(int Id)
        {
            Customer customer = null;

            string query = "SELECT Customer_ID, Customer_name, Customer_address, Customer_phone, Customer_email, PostCodes.Zip_Code, PostCodes.City_name " +
                "FROM Customers " +
                "INNER JOIN PostCodes ON Customers.Zip_Code = PostCodes.Zip_Code " +
                "WHERE Customers.Customer_ID = @val;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@val", Id);

            if (Conn.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    // open database connection
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
                customer = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6));
                x++;
            }
            reader.Close();
            if (x != 1) return null;
            return customer;
        }
        public List<Customer> GetCustomers()
        {
            List<Customer> theCustomers = new();

            string query = "SELECT Customer_ID, Customer_name, Customer_address, Customer_phone, Customer_email, PostCodes.Zip_Code, PostCodes.City_name " +
                "FROM Customers " +
                "INNER JOIN PostCodes ON Customers.Zip_Code = PostCodes.Zip_Code;";
            SqlCommand cmd = new(query, Conn);
            if (Conn.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    // open database connection
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
                Customer customer = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6));
                theCustomers.Add(customer);
            }
            reader.Close();

            return theCustomers;
        }
        public Customer SetCustomer(Customer customer)
        {
            string query = "UPDATE Customers SET " +
                "Customer_name = @name, " +
                "Customer_address = @address, " +
                "Customer_phone = @phoneNumber, " +
                "Customer_email = @email, " +
                "Zip_Code = @zipCode " +
                "WHERE Customer_ID = @id;";

            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@name", customer.CustomerName);
            cmd.Parameters.AddWithValue("@address", customer.CustomerAddress);
            cmd.Parameters.AddWithValue("@phoneNumber", customer.CustomerPhoneNumber);
            cmd.Parameters.AddWithValue("@email", customer.CustomerEmail);
            cmd.Parameters.AddWithValue("@zipCode", customer.ZipCode);
            cmd.Parameters.AddWithValue("@id", customer.CustomerId);

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
                return customer;
            }
            else
            {
                return null;
            }
        }
    }
}
