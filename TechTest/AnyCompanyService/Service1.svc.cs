using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using AnyCompany;
using AnyCompany.DTO;

namespace AnyCompanyService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public bool CreateCustomer(CustomerDTO cust)
        {
            var newCustomer = AnyCompany.CustomerRepository.Save(cust);
            return (newCustomer != null);
        }

        public List<Customer> GetCustomersAndTheirOrders()
        {
            var customersAndTheirOrders = AnyCompany.CustomerRepository.LoadAll();
            return customersAndTheirOrders;
        }

        public bool PlaceOrder(OrderDTO order, int customerId)
        {

            if (order.Amount == 0) return false;
            var orderRepo = new OrderRepository();

            var customer = CustomerRepository.Load(customerId);
            if (customer != null)
            {
                if (customer.Country == "UK")
                {
                    order.VAT = 0.2d;
                }
                else
                {
                    order.VAT = 0;
                }
            }
            else
            {
                return false;
            }

            var newOrder = orderRepo.Save(order, customerId);
            return (newOrder != null);
        }

    }
}
