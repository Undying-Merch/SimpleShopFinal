using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace SimpleShopModels
{
    public class Product
    {
        public int ProductId { get; private set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescribe { get; set; }
        public ProductTypes ProductType { get; set; }
        public Manufacture ProductManufactor { get; set; }

        // Constructor, takes name and price of product
        [JsonConstructor]
        public Product( string productName, decimal productPrice, string productDescribe, ProductTypes productType, Manufacture productManufactor)
        {
            ProductName = productName;
            ProductPrice = productPrice;
            ProductDescribe = productDescribe;
            ProductType = productType;
            ProductManufactor = productManufactor;
        }
        // Constructor overload, takes id, name and price of product
        public Product(int productid, string productname, decimal prodcutprice, string productdescribe, ProductTypes producttype, Manufacture productmanufactor)
        {
            ProductId = productid;
            ProductName = productname;
            ProductPrice = prodcutprice;
            ProductDescribe = productdescribe;
            ProductType = producttype;
            ProductManufactor = productmanufactor;
        }

        public void AssignId(int id)
        {
            ProductId = id;
        }
    }
}
