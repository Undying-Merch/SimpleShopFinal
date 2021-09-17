using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShopModels;

namespace SimpleShopORM.Interface
{
    public interface IORM_Roles
    {
        public Roles GetRole(int id);
        public List<Roles> GetRoles();
        public Roles CreateRole(Roles roles);
        public string DeleteRole(int id);
        public Roles SetRole(Roles roles);
    }
}
