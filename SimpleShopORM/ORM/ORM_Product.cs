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
    public class ORM_Product: Interface.IORM_Product
    {
        readonly SqlConnection Conn;
        DB_Connection db = new();

        public ORM_Product()
        {
            Conn = db.Connect;
        }

        public Product CreateProduct(Product product)
        {

            string query = "INSERT INTO Products(Product_name,  Product_description, Product_Price, Manufacture_ID, Product_Type_ID) " +
                "VALUES(@name, @description, @price, @manufactureId, @typeId);" +
                "SELECT SCOPE_IDENTITY() AS id;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@name", product.ProductName);
            cmd.Parameters.AddWithValue("@description", product.ProductDescribe);
            cmd.Parameters.AddWithValue("@price", product.ProductPrice);
            cmd.Parameters.AddWithValue("@manufactureId", product.ProductManufactor.ManufactureId);
            cmd.Parameters.AddWithValue("@typeId", product.ProductType.ProductTypeId);

            product.AssignId(db.DBConnAction(cmd));

            return product;

        }
        public string DeleteProduct(int Id)
        {
            string status = "";

            string query = "DELETE FROM Products " +
                "WHERE Products.Product_ID = @id;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@id", Id);

            status = db.DBConnAction(status, cmd);
            return status;
        }
        public Product GetProduct(int Id)
        {
            Product product = null;

            string query = "SELECT Product_ID, Product_name, Product_description, Product_Price, Products.Manufacture_ID, Manufacturers.Manufacture_name, Product_Type_ID, ProductTypes.ProductType_name FROM Products " +
                "INNER JOIN Manufacturers ON Products.Manufacture_ID = Manufacturers.Manufacture_ID " +
                "INNER JOIN ProductTypes ON Products.Product_Type_ID = ProductTypes.ProductType_ID " +
                "WHERE Products.Product_ID = @id;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@id", Id);

            if (Conn.State == System.Data.ConnectionState.Closed)
            {
                try {Conn.Open(); }
                catch (Exception ex) { throw new(ex.Message); }
            }
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            int x = 0;

            while (reader.Read())
            {
                product = new(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(3), reader.GetString(2),
                    new(reader.GetInt32(6), reader.GetString(7)),
                    new(reader.GetInt32(4), reader.GetString(5))
                    );
                x++;
            }
            reader.Close();
            if (x != 1) return null;
            return product;
        }
        public List<Product> GetProducts()
        {
            List<Product> products = new();

            string query = "SELECT Product_ID, Product_name, Product_description, Product_Price, Products.Manufacture_ID, Manufacturers.Manufacture_name, Product_Type_ID, ProductTypes.ProductType_name FROM Products " +
                "INNER JOIN Manufacturers ON Products.Manufacture_ID = Manufacturers.Manufacture_ID " +
                "INNER JOIN ProductTypes ON Products.Product_Type_ID = ProductTypes.ProductType_ID;";
            SqlCommand cmd = new(query, Conn);

            if (Conn.State == System.Data.ConnectionState.Closed)
            {
                try {Conn.Open(); }
                catch (Exception ex) { throw new(ex.Message); }
            }
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Product product = new(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(3), reader.GetString(2),
                    new(reader.GetInt32(6), reader.GetString(7)),
                    new(reader.GetInt32(4), reader.GetString(5))
                    );
                products.Add(product);
            }
            reader.Close();
            return products;
        }
        public Product SetProduct(Product product)
        {
            string query = "UPDATE Products " +
                "SET " +
                "Product_name = @name, " +
                "Product_description = @describe, " +
                ".Product_price = @price, " +
                "Manufacture_ID = @manufacture, " +
                "Product_Type_ID = @type " +
                "WHERE Products.Product_ID = @id;";

            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@name", product.ProductName);
            cmd.Parameters.AddWithValue("@describe", product.ProductDescribe);
            cmd.Parameters.AddWithValue("@price", product.ProductPrice);
            cmd.Parameters.AddWithValue("@manufacture", product.ProductManufactor.ManufactureId);
            cmd.Parameters.AddWithValue("@type", product.ProductType.ProductTypeId);
            cmd.Parameters.AddWithValue("@id", product.ProductId);

            if (Conn.State == System.Data.ConnectionState.Closed)
            {
                try { Conn.Open(); }
                catch (Exception ex) { throw new(ex.Message); }
            }
            int row = cmd.ExecuteNonQuery();
            Conn.Close();
            if (row == 1)
            {
                return product;
            }
            else
            {
                return null;
            }
        }
    }
}
