using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleShopModels
{
    public class Store
    {
        public int StoreId { get; private set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public List<Department> StoreDepartments { get; set; }
        public int ZipCode { get; set; }
        public string CityName { get; set; }

        [JsonConstructor]
        public Store(string storeName, string storeAddress, int zipCode, string cityName)
        {
            StoreName = storeName;
            StoreAddress = storeAddress;
            ZipCode = zipCode;
            CityName = cityName;
        }
        public Store(int id, string name, string address, int zipCode, string city)
        {
            StoreId = id;
            StoreName = name;
            StoreAddress = address;
            ZipCode = zipCode;
            CityName = city;
        }
        public void AssignId(int id)
        {
            StoreId = id;
        }
    }
}
