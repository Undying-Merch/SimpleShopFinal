using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SimpleShopModels
{
    public class Customer
    {
        
        public int CustomerId { get; private set; }
        public string CustomerName { get; set; }
        public List<Order> CustomerOrders { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }


        // Constructor, takes all <<prop>> exept CustomerId and fills them
        [JsonConstructor]
        public Customer(string customerName, string customerAddress, string customerPhoneNumber, string customerEmail, int zipCode, string city)
        {
            CustomerName = customerName;
            CustomerAddress = customerAddress;
            CustomerPhoneNumber = customerPhoneNumber;
            CustomerEmail = customerEmail;
            ZipCode = zipCode;
            City = city;
        }
        // Constructor overload, takes all <<prop>> and fills them
        public Customer(int id, string name, string address, string phoneNumber, string email, int postcode, string cityName)
        {
            CustomerId = id;
            CustomerName = name;
            CustomerAddress = address;
            CustomerPhoneNumber = phoneNumber;
            CustomerEmail = email;
            ZipCode = postcode;
            City = cityName;
        }

        public void AssignId(int id)
        {
            CustomerId = id;
        }
    }
}
