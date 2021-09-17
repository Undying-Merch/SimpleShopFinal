using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShopModels;

namespace SimpleShopORM.Interface
{
    public interface IORM_Employee
    {
        public Employee GetEmployee(int id);
        public List<Employee> GetEmployees();
        public Employee CreateEmployee(Employee employee);
        public string DeleteEmployee(int id);
        public Employee SetEmployee(Employee employee);
    }
}
