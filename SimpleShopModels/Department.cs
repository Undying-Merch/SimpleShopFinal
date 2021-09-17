using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleShopModels
{
    public class Department
    {
        public int DepartmentId { get; private set; }
        public string DepartmentName { get; set; }
        public List<Employee> Employees { get; set; }

        [JsonConstructor]
        public Department(string departmentName )
        {
            DepartmentName = departmentName;
        }
        public Department(int id, string name)
        {
            DepartmentId = id;
            DepartmentName = name;
        }

        public void AssignId(int id)
        {
            DepartmentId = id;
        }
        
    }
}
