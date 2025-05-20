namespace EFCORELEARNING.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public int? mgrid { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Title { get; set; }
        public string? TitleofCourtesy { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime Hiredate { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? State { get; set; }
        //1-m with Order
        public List<Order>? OrdersOfEmployee { get; set; }
    }
}
