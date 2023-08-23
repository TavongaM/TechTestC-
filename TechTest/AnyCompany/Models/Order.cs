namespace AnyCompany
{
    public class Order
    {
        public Order() { }

        public Order(double amount, double vat)
        {
            Amount = amount;
            VAT = vat;
        }

        public int Id { get; set; }
        public double Amount { get; set; }
        public double VAT { get; set; }
        internal int CustomerId { get; set; }
    }
}
