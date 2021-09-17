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
    public class ORM_Department: Interface.IORM_Department
    {
        readonly SqlConnection Conn;
        DB_Connection db = new();

        public ORM_Department()
        {
            Conn = db.Connect;
        }
        public Department CreateDepartment(Department department)
        {
            string query = "INSERT INTO Departments( Department_name) " +
                "VALUES(@name);" +
                "SELECT SCOPE_IDENTITY() AS id;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@name", department.DepartmentName);

            department.AssignId(db.DBConnAction(cmd));

            return department;
        }
        public string DeleteDepartment(int Id)
        {
            string status = "";

            string query = "DELETE FROM Departments WHERE Departments.Department_ID = @departmentID;";

            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@departmentID", Id);

            status = db.DBConnAction(status, cmd);

            return status;
        }
        public Department GetDepartment(int Id)
        {
            Department department = null;

            string query = "SELECT Departments.Department_ID, Departments.Department_name " +
                "FROM Departments WHERE Departments.Department_ID = @id;";
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
                department = new(reader.GetInt32(0), reader.GetString(1));
                x++;
            }
            reader.Close();
            if (x != 1) return null;

            return department;
        }
        public List<Department> GetDepartments()
        {
            List<Department> departments = new();

            string query = "SELECT Departments.Department_ID, Departments.Department_name FROM Departments;";
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
                Department department = new(reader.GetInt32(0), reader.GetString(1));
                departments.Add(department);
            }
            reader.Close();
            return departments;
        }
        public Department SetDepartment(Department department)
        {
            string query = "UPDATE Departments SET " +
                "Department_name = @departmentName " +
                "WHERE Department_ID = @departmentId;";

            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@departmentName", department.DepartmentName);
            cmd.Parameters.AddWithValue("@departmentId", department.DepartmentId);
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
                return department;
            }
            else
            {
                return null;
            }
        }
    }
}
