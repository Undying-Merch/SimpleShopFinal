using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShopModels;

namespace SimpleShopORM.Interface
{
    public interface IORM_Order
    {
        public Order GetOrder(int id);
        public List<Order> GetOrders();
        public Order CreateOrder(Order order);
        public string DeleteOrder(int id);
        public Order Setorder(Order order);
        public int AddProductToOrder(int orderId, Product product, int amount);
        public string RemoveProductFromOrder(int id);
        public List<Product> GetProductsInOrder(int id);
    }
}
