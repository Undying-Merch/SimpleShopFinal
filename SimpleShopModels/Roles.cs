using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleShopModels
{
    public class Roles
    {
        public int RoleId { get; private set; }
        public string RoleName { get; set; }

        [JsonConstructor]
        public Roles(string roleName)
        {
            RoleName = roleName;
        }

        public Roles(int roleId, string roleName)
        {
            RoleId = roleId;
            RoleName = roleName;
        }
        public void AssignId(int id)
        {
            RoleId = id;
        }
    }
}
