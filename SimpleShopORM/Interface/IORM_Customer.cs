using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShopModels;

namespace SimpleShopORM.Interface
{
    public interface IORM_Customer
    {
        public Customer GetCustomer(int id);
        public List<Customer> GetCustomers();
        public Customer CreateCustomer(Customer customer);
        public string DeleteCustomer(int id);
        public Customer SetCustomer(Customer customer);
    }
}
