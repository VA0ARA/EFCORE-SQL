namespace EFCORELEARNING.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
        public int UnitPrice { get; set; } //money
        public bool Discontinued { get; set; } //bit
        // many-many
        public ICollection<OrderDetail>? ListOfOrderDetailsOfProduct { get; set; }

    }
}
