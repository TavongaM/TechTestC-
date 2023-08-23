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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        bool CreateCustomer(CustomerDTO cust);

        [OperationContract]
        List<Customer> GetCustomersAndTheirOrders();

        [OperationContract]
        bool PlaceOrder(OrderDTO order, int customerId);
    }

}
