namespace EFCORELEARNING.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int EmployeeID { get; set; }
        public  Employee? EmployeeOfOrder { get; set; }
        public int CustomerID { get; set; }
        public Customer? CustomerOfOrder { get; set; }
        public int ShipperID { get; set; }
        public int Freight { get; set; }// money
        public DateTime OrderDate { get; set; } 
        // rel n-m  orderdetail 
        public List <OrderDetail>? ListOfOrderDetailsOfOrder { get; set; }
    }
}
