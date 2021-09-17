using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShopModels;

namespace SimpleShopORM.Interface
{
    public interface IORM_OrderStatus
    {
        public OrderStatus GetOrderStatus(int id);
        public List<OrderStatus> GetOrderStatuses();
        public OrderStatus CreateOrderStatus(OrderStatus orderStatus);
        public string DeleteOrderStatus(int id);
        public OrderStatus SetOrderStatus(OrderStatus orderStatus);
    }
}
