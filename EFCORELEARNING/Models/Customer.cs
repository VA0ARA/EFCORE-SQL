namespace EFCORELEARNING.Models
{
    public class Customer
    {
        public int  CurtomerId { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? State { get; set; }
        //1-m with Order
        public List<Order>? OrdersOfCustomer { get; set; }
    }
}
