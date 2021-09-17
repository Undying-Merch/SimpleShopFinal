using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleShopModels
{
    public class ProductTypes
    {
        public int ProductTypeId { get; private set; }
        public string ProductTypeName { get; set; }


        public ProductTypes(string productTypeName)
        {
            ProductTypeName = productTypeName;
        }
        [JsonConstructor]
        public ProductTypes(int productTypeId, string productTypeName)
        {
            ProductTypeId = productTypeId;
            ProductTypeName = productTypeName; 
        }
        public void AssignId(int id)
        {
            ProductTypeId = id;
        }
    }
}
