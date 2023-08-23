using System;
using System.Collections.Generic;

namespace AnyCompany.DTO
{
    public class CustomerDTO
    {
        public CustomerDTO() { }

        public CustomerDTO(string country, DateTime dateOfBirth, string name)
        {
            Name = name;
            Country = country;
            DateOfBirth = dateOfBirth;
        }

        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime DateOfBirth { get; set; }



    }
}
