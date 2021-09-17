using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleShopModels
{
    public class Order
    {
        public int OrderId { get; private set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<Product> Products { get; set; }

        public int CustomerId { get; set; }

        // Constructor, takes status for the Order
        [JsonConstructor]
        public Order(OrderStatus orderStatus)
        {
            OrderStatus = orderStatus;
        }
        public Order(int id, OrderStatus status)
        {
            OrderId = id;
            OrderStatus = status;
        }
        public Order(int id, DateTime date, OrderStatus orderStatus)
        {
            OrderId = id;
            OrderDate = date;
            OrderStatus = orderStatus;
        }
        public void AssignId(int id)
        {
            OrderId = id;
        }
    }
}
