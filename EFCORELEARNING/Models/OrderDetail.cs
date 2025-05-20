namespace EFCORELEARNING.Models
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int UnitPrice { get; set; }//money
        public int Qty { get; set; }//small int 
        public int Discount { get; set; }//numeric
        //many-many
        public Order? OrderOfOrderDetail { get; set; }
        // many-many
        public Product? ProductOfOrderDetail { get; set; }

    }
}
