using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShopModels;

namespace SimpleShopORM.Interface
{
    public interface IORM_Product
    {
        public Product GetProduct(int id);
        public List<Product> GetProducts();
        public Product CreateProduct(Product product);
        public string DeleteProduct(int id);
        public Product SetProduct(Product product);
    }
}
