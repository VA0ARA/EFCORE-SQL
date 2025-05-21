namespace EFCORELEARNING.DTOs
{
    public class CustomerOrderDetailDto
    {
        public int CurtomerId { get; set; }
        public string CompanyName { get; set; }
        public int? OrderID { get; set; }
        public int? ProductID { get; set; }
        public short? Qty { get; set; }
    }
}
