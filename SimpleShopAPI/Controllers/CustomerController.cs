using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleShopModels;

namespace SimpleShopAPI.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly SimpleShopORM.ORM.ORM_Customers ORM;

        public CustomerController()
        {
            ORM = new();
        }

        [HttpGet("{Id}")]
        public ActionResult<Customer> Get(int Id)
        {
            Customer customer;
            try
            {
                customer = ORM.GetCustomer(Id);
            }
            catch (Exception ex)
            {

                throw new ArgumentException("Something went wrong " + ex.Message);
            }

            if (customer == null)
            {
                return NotFound();
            }

            //200 ok
            return customer;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            List<Customer> customers;

            try
            {
                customers = ORM.GetCustomers();
            }
            catch (Exception ex)
            {

                throw new ArgumentException("Something went wrong " + ex.Message);
            }

            if (customers.Count < 1)
            {
                return NotFound();
            }

            return customers;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            try
            {
                ORM.CreateCustomer(customer);
            }
            catch (Exception ex)
            {

                throw new ArgumentException("Something went wrong " + ex.Message);
            }
            return customer;
        }

        [HttpPut("{Id}")]
        public ActionResult<Customer> Put(int Id, [FromBody] Customer customer)
        {
            try
            {
                customer.AssignId(Id);
                ORM.SetCustomer(customer);
            }
            catch (Exception ex)
            {

                throw new ArgumentException("Something went wrong " + ex.Message);
            }

            return customer;
        }

        [HttpDelete("{Id}")]
        public ActionResult<string> Delete(int Id)
        {
            string results;
            try
            {
                results = ORM.DeleteCustomer(Id);
            }
            catch (Exception ex)
            {

                throw new ArgumentException("Something went wrong " + ex.Message);
            }

            return results;
        }
    }
}
