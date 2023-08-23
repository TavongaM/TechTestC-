using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnyCompany;
using AnyCompany.DTO;

namespace AnyCompany.Tests
{
    [TestClass]
    public class DB_SaveTest
    {
        [TestMethod]
        public void TestOrders()
        {

            //==============================================================================================//
            // Second Test //
            //==============================================================================================//
            // Arrange
            var cust1dto = new CustomerDTO("South Africa", new DateTime(1977, 06, 27), "Tavonga Makumbe");
            var cust2dto = new CustomerDTO("USA", new DateTime(1944, 08, 14), "Donald Trump");
            var cust3dto = new CustomerDTO("China", new DateTime(1969, 10, 11), "Steven Hawkins");
            Customer cust1, cust2, cust3;

            //Act
            CustomerRepository.ClearAll();
            cust1 = CustomerRepository.Save(cust1dto);
            cust2 =  CustomerRepository.Save(cust2dto);
            cust3 = CustomerRepository.Save(cust3dto);
            var allCustomers = CustomerRepository.LoadAll();

            //Assert
            Assert.IsNotNull(cust1);
            Assert.IsNotNull(cust2);
            Assert.IsNotNull(cust3);
            Assert.AreEqual(3, allCustomers.Count);

            //==============================================================================================//
            // Second Test //
            //==============================================================================================//

            // Arrange
            var order1dto = new OrderDTO(20.45, 1.11);
            var order2dto = new OrderDTO(40.90, 2.22);
            var order3dto = new OrderDTO(50.67, 3.33);
            var order4dto = new OrderDTO(55.90, 3.44);
            var order5dto = new OrderDTO(23.77, 1.55);
            var order6dto = new OrderDTO(150.23, 6.00);
            Order order1, order2, order3, order4, order5, order6;

            //Act
            OrderRepository orderRepo = new OrderRepository();
            orderRepo.ClearAll();
            order1 = orderRepo.Save(order1dto, cust1.Id);
            order2 = orderRepo.Save(order2dto, cust1.Id);
            order3 = orderRepo.Save(order3dto, cust1.Id);
            order4 = orderRepo.Save(order4dto, cust2.Id);
            order5 = orderRepo.Save(order5dto, cust2.Id);
            order6 = orderRepo.Save(order6dto, cust3.Id);
            var allCustomers2 = CustomerRepository.LoadAll();

            //Assert
            Assert.AreEqual(3, allCustomers2.Where(x => x.Id == cust1.Id).FirstOrDefault().CustomerOrders.Count);
            Assert.AreEqual(2, allCustomers2.Where(x => x.Id == cust2.Id).FirstOrDefault().CustomerOrders.Count);
            Assert.AreEqual(1, allCustomers2.Where(x => x.Id == cust3.Id).FirstOrDefault().CustomerOrders.Count);


            //cleanup
            orderRepo.ClearAll();
            CustomerRepository.ClearAll();
            orderRepo = null;
        }
    }
}
