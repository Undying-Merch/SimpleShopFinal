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
    public class ORM_Order: Interface.IORM_Order
    {
        readonly SqlConnection Conn;
        DB_Connection db = new();

        public ORM_Order()
        {
            Conn = db.Connect;
        }

        public int AddProductToOrder(int orderId, Product product, int amount)
        {
            string query = "INSERT INTO Order_Products(Order_ID, Product_ID, Product_amount, Product_Price) " +
                "VALUES(@orderId, @productId, @amount, @price);";

            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@orderId", orderId);
            cmd.Parameters.AddWithValue("@productId", product.ProductId);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@price", product.ProductPrice);

            int productInOrderID = db.DBConnAction(cmd);
            return productInOrderID;
        }

        public Order CreateOrder(Order order)
        {
            string query = "INSERT INTO Orders(Order_date, Costomer_ID, Order_Status_ID) " +
                "VALUES(@dateTime, @customerId, @orderStatus);" +
                "SELECT SCOPE_IDENTITY() AS id;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@customerId", order.CustomerId);
            cmd.Parameters.AddWithValue("@orderStatus", order.OrderStatus.OrderStatusId);

            order.AssignId(db.DBConnAction(cmd));
            return order;
        }
        public string DeleteOrder(int Id)
        {
            string status = "";

            string query = "DELETE FROM Orders " +
                "WHERE Order_ID = @orderId;";

            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@orderId", Id);

            status = db.DBConnAction(status, cmd);
            return status;
        }
        public Order GetOrder(int Id)
        {
            Order order = null;

            string query = "SELECT Order_ID, Order_date, Order_Status_ID, Order_Status.Order_Status_Type FROM Orders " +
                "INNER JOIN Order_Status ON Orders.Order_Status_ID = Order_Status.Order_Status_ID " +
                "WHERE Order_ID = @id;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@id", Id);

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
                order = new(reader.GetInt32(0), reader.GetDateTime(1), new(reader.GetInt32(2), reader.GetString(3)));
                x++;
            }
            reader.Close();
            if (x != 1) return null;

            return order;
        }
        public List<Order> GetOrders()
        {
            List<Order> orders = new();

            string query = "SELECT Order_ID, Order_date, Order_Status_ID, Order_Status.Order_Status_Type FROM Orders " +
                "INNER JOIN Order_Status ON Orders.Order_Status_ID = Order_Status.Order_Status_ID;";
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
                Order order = new(reader.GetInt32(0), reader.GetDateTime(1), new(reader.GetInt32(2), reader.GetString(3)));
                orders.Add(order);
            }
            reader.Close();
            return orders;
        }
        public List<Product> GetProductsInOrder(int Id)
        {
            List<Product> products = new();
            string query = "SELECT Product_ID, Products.Product_name, Products.Product_description, Products.Manufacture_ID, Manufacturers.Manufacture_name, Products.Product_Type_ID, ProductTypes.ProductType_name, Product_amount, Product_Price FROM Order_Products " +
                "INNER JOIN Products ON Order_Products.Product_ID = Products.Product_ID " +
                "INNER JOIN Manufacturers ON Products.Manufacture_ID = Manufacturers.Manufacture_ID " +
                "INNER JOIN ProductTypes ON Products.Product_Type_ID = ProductTypes.ProductType_ID " +
                "WHERE ID = @id";

            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@id", Id);

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
                Product product = new(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(8), reader.GetString(2), new(reader.GetInt32(5), reader.GetString(6)), new(reader.GetInt32(3), reader.GetString(4)));

                for (int i = 0; i < reader.GetInt32(7); i++)
                {
                    products.Add(product);
                }
            }
            reader.Close();

            return products;
        }
        public string RemoveProductFromOrder(int Id)
        {
            string status = "";
            string query = "DELETE FROM Order_Products " +
                "WHERE ID = @id;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@id", Id);

            status = db.DBConnAction(status, cmd);
            return status;
        }
        public Order Setorder(Order order)
        {
            string query = "UPDATE Orders SET " +
                "Order_Status_ID = @statusId " +
                "WHERE Employees.Employee_ID = @id;";

            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@statusId", order.OrderStatus.OrderStatusId);
            cmd.Parameters.AddWithValue("@id", order.OrderId);

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
                return order;
            }
            else
            {
                return null;
            }
        }
    }
}
