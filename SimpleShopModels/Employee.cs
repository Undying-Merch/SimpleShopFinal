using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleShopModels
{
    public class Employee
    {
        public int EmployeeId { get; private set; }
        public string EmployeeName { get; set; }
        public Roles EmployeeRole { get; set; }
        public Department EmployeeDepartment { get; set; }

        [JsonConstructor]
        public Employee(string employeeName, Roles employeeRole, Department employeeDepartment)
        {
            EmployeeName = employeeName;
            EmployeeRole = employeeRole;
            EmployeeDepartment = employeeDepartment;
        }
        public Employee(int id, string name, Roles role, Department employeeDepartment)
        {
            EmployeeId = id;
            EmployeeName = name;
            EmployeeRole = role;
            EmployeeDepartment = employeeDepartment;
        }
        public void AssignId(int id)
        {
            EmployeeId = id;
        }
    }
}
