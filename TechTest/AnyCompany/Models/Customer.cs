using System;
using System.Collections.Generic;

namespace AnyCompany
{
    public class Customer
    {
        public Customer() { }

        public Customer(string country, DateTime dateOfBirth, string name)
        {
            Name = name;
            Country = country;
            DateOfBirth = dateOfBirth;
        }

        public string Name { get; set; }

        public int Id { get; set; }

        public string Country { get; set; }

        public DateTime DateOfBirth { get; set; }

        public List<Order> CustomerOrders { get; set; }
    }
}
