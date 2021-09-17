using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleShopModels
{
    public class Manufacture
    {
        public int ManufactureId { get; private set; }
        public string ManufactureName { get; set; }


        public Manufacture(string manufactureName)
        {
            ManufactureName = manufactureName;
        }
        [JsonConstructor]
        public Manufacture(int manufactureId, string manufactureName)
        {
            ManufactureId = manufactureId;
            ManufactureName = manufactureName;
        }
        public void AssignId(int id)
        {
            ManufactureId = id;
        }
    }
}
