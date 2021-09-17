using SimpleShopModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShopORM.DBConnection;

namespace SimpleShopORM
{
    public class ORM_MsSql
    {

        

       /* public Customer GetCustomer(int id)
        {
            Customer customer = null;

            string query = "SELECT id, navn FROM kunde WHERE id = @val";
            SqlCommand cmd = new SqlCommand(query, dbConn);
            cmd.Parameters.AddWithValue("@val", id);

            if (dbConn.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    dbConn.Open();
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }

            return customer;
        }

        public List<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }*/
    }
}
