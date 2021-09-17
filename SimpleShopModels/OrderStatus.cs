using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleShopModels
{
    public class OrderStatus
    {

        public int OrderStatusId { get; private set; }

        public string OrderStatusName { get; set; }

        // Constructor, takes id and name of OrderStatus
        [JsonConstructor]
        public OrderStatus(string orderStatusName)
        {
            OrderStatusName = orderStatusName;
        }
        public OrderStatus(int orderStatusId, string orderStatusName)
        {
            OrderStatusId = orderStatusId;
            OrderStatusName = orderStatusName;
        }
        public void AssignId(int id)
        {
            OrderStatusId = id;
        }
    }
}
