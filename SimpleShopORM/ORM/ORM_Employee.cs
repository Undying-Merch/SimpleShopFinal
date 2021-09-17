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
    public class ORM_Employee: Interface.IORM_Employee
    {
        readonly SqlConnection Conn;
        DB_Connection db = new();

        public ORM_Employee()
        {
            Conn = db.Connect;
        }

        public Employee CreateEmployee(Employee employee)
        {
            string query = "INSERT INTO Employees(Employee_name, Role_ID, Store_Department_ID) " +
                "VALUES(@name, @roleId, @storeDepartmentId);" +
                "SELECT SCOPE_IDENTITY() AS id;";
            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@name", employee.EmployeeName);
            cmd.Parameters.AddWithValue("@roleId", employee.EmployeeRole.RoleId);
            cmd.Parameters.AddWithValue("@storeDepartmnentId", employee.EmployeeDepartment.DepartmentId);

            employee.AssignId(db.DBConnAction(cmd));
            return employee;
        }

        public string DeleteEmployee(int Id)
        {
            string status = "";

            string query = "DELETE FROM Employees WHERE Employee_ID = @employeeId;";

            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@employeeId", Id);

            status = db.DBConnAction(status, cmd);
            return status;
        }
        public Employee GetEmployee(int Id)
        {
            Employee employee = null;

            string query = "SELECT Employee_ID, Employee_name, Role_ID, Roles.Role_name, Departments.Department_ID, Departments.Department_name FROM Employees " +
                "INNER JOIN Roles ON Employees.Role_ID = Roles.Role_ID " +
                "INNER JOIN Store_Has_Departments ON Employees.Store_Department_ID = Store_Has_Departments.ID " +
                "INNER JOIN Departments ON Store_Has_Departments.Department_ID = Departments.Department_ID " +
                "WHERE Employees.Employee_ID = @id;";

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
                employee = new(reader.GetInt32(0), reader.GetString(1), new(reader.GetInt32(2), reader.GetString(3)), new(reader.GetInt32(4), reader.GetString(5)));
                x++;
            }
            reader.Close();

            if (x != 1) return null;
            return employee;
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new();

            string query = "SELECT Employee_ID, Employee_name, Role_ID, Roles.Role_name, Departments.Department_ID, Departments.Department_name FROM Employees " +
                "INNER JOIN Roles ON Employees.Role_ID = Roles.Role_ID " +
                "INNER JOIN Store_Has_Departments ON Employees.Store_Department_ID = Store_Has_Departments.ID " +
                "INNER JOIN Departments ON Store_Has_Departments.Department_ID = Departments.Department_ID ";
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
                Employee employee = new(reader.GetInt32(0), reader.GetString(1), new(reader.GetInt32(2), reader.GetString(3)), new(reader.GetInt32(4), reader.GetString(5)));
                employees.Add(employee);
            }
            reader.Close();
            return employees;
        }
        public Employee SetEmployee(Employee employee)
        {
            string query = "UPDATE Employees SET " +
                "Employee_name = @name, " +
                "Role_ID = @roleId " +
                "WHERE Employee_ID = @id;";

            SqlCommand cmd = new(query, Conn);
            cmd.Parameters.AddWithValue("@name", employee.EmployeeName);
            cmd.Parameters.AddWithValue("@roleId", employee.EmployeeRole.RoleId);
            cmd.Parameters.AddWithValue("@id", employee.EmployeeId);

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
                return employee;
            }
            else
            {
                return null;
            }
        }
    }
}
