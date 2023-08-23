using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.DTO
{
    public class OrderDTO
    {
        public OrderDTO() { }

        public OrderDTO(double amount, double vat)
        {
            Amount = amount;
            VAT = vat;
        }

        public double Amount { get; set; }
        public double VAT { get; set; }
        internal int CustomerId { get; set; }
    }
}
