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
    public class ORM_Roles: Interface.IORM_Roles
    {
        readonly SqlConnection Conn;
        DB_Connection db = new();

        public ORM_Roles()
        {
            Conn = db.Connect;
        }

        public Roles CreateRole(Roles roles)
        {
            string query = "INSERT INTO Roles(Role_name) " +
                "VALUES(@name);" +
                "SELECT SCOPE_IDENTITY() AS id;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@name", roles.RoleName);

            roles.AssignId(db.DBConnAction(cmd));
            return roles;
        }
        public string DeleteRole(int Id)
        {
            string status = "";

            string query = "DELETE FROM Roles " +
                "WHERE Role_ID = @id;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@id", Id);

            status = db.DBConnAction(status, cmd);
            return status;
        }
        public Roles GetRole(int Id)
        {
            Roles role = null;

            string query = "SELECT Role_ID, Role_name FROM Roles " +
                "WHERE Roles.Role_ID = @id;";

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
                role = new(reader.GetInt32(0), reader.GetString(1));
                x++;
            }
            reader.Close();
            if (x != 1) return null;
            return role;
        }
        public List<Roles> GetRoles()
        {
            List<Roles> roles = new();
            string query = "SELECT Role_ID, Role_name FROM Roles;";

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
                Roles role = new(reader.GetInt32(0), reader.GetString(1));
                roles.Add(role);
            }
            reader.Close();
            return roles;
        }
        public Roles SetRole(Roles roles)
        {
            string query = "UPDATE Roles " +
                "SET " +
                "Role_name = @name " +
                "WHERE Role_ID = @id;";

            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@name", roles.RoleName);
            cmd.Parameters.AddWithValue("@id", roles.RoleId);

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
                return roles;
            }
            else
            {
                return null;
            }
        }
    }
}
