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
    public class ORM_OrderStatus: Interface.IORM_OrderStatus
    {
        readonly SqlConnection Conn;
        DB_Connection db = new();

        public ORM_OrderStatus()
        {
            Conn = db.Connect;
        }

        public OrderStatus CreateOrderStatus(OrderStatus orderStatus)
        {
            string query = "INSERT INTO Order_Status(Order_Status_Type) " +
                "VALUES(@type);" +
                "SELECT SCOPE_IDENTITY() AS id;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@type", orderStatus.OrderStatusName);

            orderStatus.AssignId(db.DBConnAction(cmd));
            return orderStatus;
        }
        public string DeleteOrderStatus(int Id)
        {
            string status = "";

            string query = "DELETE FROM Order_Status " +
                "WHERE Order_Status_ID = @id;";

            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@id", Id);

            status = db.DBConnAction(status, cmd);

            return status;
        }
        public OrderStatus GetOrderStatus(int Id)
        {
            OrderStatus orderStatus = null;

            string query = "SELECT Order_Status_ID, Order_Status_Type FROM Order_Status " +
                "WHERE Order_Status_ID = @id;";
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
                orderStatus = new(reader.GetInt32(0), reader.GetString(1));
                x++;
            }
            reader.Close();
            if (x != 1) return null;
            return orderStatus;
        }
        public List<OrderStatus> GetOrderStatuses()
        {
            List<OrderStatus> orderStatuses = new();

            string query = "SELECT Order_Status_ID, Order_Status_Type FROM Order_Status;";
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
                OrderStatus orderStatus = new(reader.GetInt32(0), reader.GetString(1));
                orderStatuses.Add(orderStatus);
            }
            reader.Close();
            return orderStatuses;
        }
        public OrderStatus SetOrderStatus(OrderStatus orderStatus)
        {
            string query = "UPDATE Order_Status " +
                "SET Order_Status_Type = @type " +
                "WHERE Order_Status.Order_Status_ID = @id;";

            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@type", orderStatus.OrderStatusName);
            cmd.Parameters.AddWithValue("@id", orderStatus.OrderStatusId);
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
                return orderStatus;
            }
            else
            {
                return null;
            }
        }
    }
}
