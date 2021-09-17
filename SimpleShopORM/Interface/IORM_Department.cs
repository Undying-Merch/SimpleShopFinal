using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShopModels;

namespace SimpleShopORM.Interface
{
    public interface IORM_Department
    {
        public Department GetDepartment(int id);
        public List<Department> GetDepartments();
        public Department CreateDepartment(Department department);
        public string DeleteDepartment(int id);
        public Department SetDepartment(Department department);
    }
}
