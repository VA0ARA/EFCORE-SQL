using EFCORELEARNING.DATA;
using EFCORELEARNING.DTOs;
using EFCORELEARNING.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EFCORELEARNING.Controllers
{
    // for test every thing in this Project use debuger ti understand what happen!!!!!!
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _Context;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _Context = context; 
        }

        public IActionResult Index()
        {
            #region SELECT-AsNoTracking-ToQueryString()
            //1.EF-Linq
            var query = (from x in _Context.Orders
                         select new
                         {
                             OrderID = x.OrderID,
                             CustomerID = x.CustomerID,
                             EmployeeID = x.EmployeeID,
                             OrderDate = x.OrderDate,
                         }).AsNoTracking();// just for readonly
            Console.WriteLine(query.ToQueryString());
            var ListOfOrders = query.ToList();
            //2.EF-SQl Query>>create DTO and .....
            //we have to create DTO for due to  firstly, EF need to see  Model(Dto) then map it on Table in Sql so you need to create dto base on Column You need to create DBset & OnModelCreating
            //or select all of properties of Entity
            var orders = _Context.Set<OrderDto>()
                .FromSqlRaw("SELECT OrderID, CustomerID, EmployeeID, OrderDate FROM dbo.Orders")
                .AsNoTracking()
                .ToList();
            #endregion
            #region DELETE Table
            //Add-Migration RemoveMyTable>>Update-Database-Table_1
            //1.dotnet ef migrations add RemoveMyTable
            //2._Context.Database.ExecuteSqlRaw("DROP TABLE TableName");
            #endregion
            #region Insert
            //var student1 = new Students { Code = 1000, FirstName = "Samira", LastName = "Mohammadi", Age = 15 };
            //var student2 = new Students { Code = 1001, FirstName = "Teraneh", LastName = "Javid", Age = 14 };

            //context.Students.AddRange(student1, student2);
            //context.SaveChanges();

            #endregion
            #region DELETE
            // EF Core: Delete Data from Orders Table
            //var orderToDelete = context.Orders.FirstOrDefault(o => o.ID == 101);
            //if (orderToDelete != null)
            //{
            //    context.Orders.Remove(orderToDelete);
            //    context.SaveChanges();
            //}
            #endregion
            #region Update
            // EF Core: Update Data in Orders Table
            //var orderToUpdate = context.Orders.FirstOrDefault(o => o.ID == 100);
            //if (orderToUpdate != null)
            //{
            //    orderToUpdate.OrderDate = "1395.03.30";
            //    context.SaveChanges();
            //}
            #endregion
            #region schema
            // EF Core: Create the schema (equivalent to CREATE SCHEMA)
            // EF Core does not have a direct way to create schemas, but you can specify schema for tables during table creation.
            //        protected override void OnModelCreating(ModelBuilder modelBuilder)
            //{
            //    // Specifying schema for Tbl1
            //    modelBuilder.Entity<Tbl1>().ToTable("Tbl1", "MySchema");
            //}
            #endregion
            #region Where
            var orderslist = _Context.Orders
                    .Where(o => o.CustomerID == 71)
                    .Select(o => new { o.OrderID, o.CustomerID })
                    .ToList();
                      //     SELECT
                      //    OrderID, OrderDate
                      //FROM dbo.Orders
                      //    WHERE OrderID IN(10248,10253,10320);
                      //            GO
                        var ordersdate = _Context.Orders
                    .Where(o => new[] { 10248, 10253, 10320 }.Contains(o.OrderID))
                    .Select(o => new { o.OrderID, o.OrderDate })
                    .ToList();
            /*                                   SELECT
                                       OrderID, OrderDate
                                   FROM dbo.Orders
                                       WHERE OrderID NOT IN(10248, 10253, 10320);
                                               GO*/
            var ordersnot = _Context.Orders
                                .Where(o => !new[] { 10248, 10253, 10320 }.Contains(o.OrderID))
                                .Select(o => new { o.OrderID, o.OrderDate })
                                .ToList();
            //            SELECT
            //    OrderID, EmployeeID
            //FROM dbo.Orders
            //    WHERE EmployeeID BETWEEN 3 AND 7;
            //            GO
               var ordersrang = _Context.Orders
                              .Where(o => o.EmployeeID >= 3 && o.EmployeeID <= 7)
                              .Select(o => new { o.OrderID, o.EmployeeID })
                              .ToList();
                       //            SELECT
                       //    OrderID, EmployeeID
                       //FROM dbo.Orders
                       //    WHERE EmployeeID IN(3,4,5,6,7);
                       //            GO
                        var ordersrang2 = _Context.Orders
                    .Where(o => new[] { 3, 4, 5, 6, 7 }.Contains(o.EmployeeID))
                    .Select(o => new { o.OrderID, o.EmployeeID })
                    .ToList();
            //            SELECT
            //    FirstName, LastName                            
            //FROM dbo.Employees
            //    WHERE LastName LIKE N'ا%';
            //            GO
            var employees = _Context.Employees
                .Where(e => EF.Functions.Like(e.LastName, "ا%"))
                .Select(e => new { e.FirstName, e.LastName })
                .ToList();
                   //            SELECT
                   //    FirstName, LastName
                   //FROM dbo.Employees
                   //    WHERE LastName LIKE N'%ی';
                   //            GO
                   
                        var employees2 = _Context.Employees
                       .Where(e => EF.Functions.Like(e.LastName, "%ی"))
                       .Select(e => new { e.FirstName, e.LastName })
                       .ToList();
            //            SELECT
            //    OrderID, productid, qty, unitprice, discount,

            //    qty* UnitPrice *(1 - discount) AS val
            //FROM dbo.OrderDetails;
            //            GO
            var orderDetails = _Context.OrderDetails
                .Select(od => new
                {
                    od.OrderID,
                    od.ProductID,
                    qty = od.Qty,
                    od.UnitPrice,
                    od.Discount,
                    val = od.Qty * od.UnitPrice * (1 - (decimal)od.Discount)
                })
                .ToList();
            //            SELECT
            //    OrderID, CustomerID, EmployeeID, OrderDate
            //FROM dbo.Orders
            //    WHERE CustomerID = 1

            //    AND EmployeeID IN(1, 3, 5)

            //    OR CustomerID = 85

            //    AND EmployeeID IN(2, 4, 6);
            //            GO

            var result = _Context.Orders
                            .Where(o =>
                                (o.CustomerID == 1 && new[] { 1, 3, 5 }.Contains(o.EmployeeID)) ||
                                (o.CustomerID == 85 && new[] { 2, 4, 6 }.Contains(o.EmployeeID))
                            )
                            .Select(o => new
                            {
                                o.OrderID,
                                o.CustomerID,
                                o.EmployeeID,
                                o.OrderDate
                            })
                            .ToList();
            //SELECT
            //EmployeeID, YEAR(OrderDate) AS OrderYear
            //FROM dbo.Orders;
            //GO
            var ordersyear = _Context.Orders
                .Select(o => new
                {
                    o.EmployeeID,
                    OrderYear = o.OrderDate.Year
                })
                .ToList();
            //            SELECT
            //    CustomerID, State, Region, City,

            //    CONCAT(State, '*', Region, '*', City) AS Cust_location
            //FROM dbo.Customers;
            //            GO
            var customerLocations2 = _Context.Customers
                            .Select(c => new {
                              c.CurtomerId,
                              c.State,
                              c.Region,
                              c.City,
                              Cust_location = (c.State ?? "") + "*" + (c.Region ?? "") + "*" + c.City
                            })
                            .ToList();



            #endregion
            #region GroupBy
            //            SELECT
            //    EmployeeID, CustomerID
            //FROM dbo.Orders
            //GROUP BY EmployeeID, CustomerID;
            //            GO
            var emp = (from x in _Context.Orders
                       select new
                       {
                           EmployeeID = x.EmployeeID,
                           CustomerID = x.CustomerID
                       })
          .GroupBy(g => new { g.EmployeeID, g.CustomerID }).ToList();
            var resultorder = _Context.Orders
                       .GroupBy(o => new { o.EmployeeID, o.CustomerID })
                       .Select(g => new
                       {
                           EmployeeID = g.Key.EmployeeID,
                           CustomerID = g.Key.CustomerID,
                           MinOrderID = g.Min(x => x.OrderID)
                       })
                       .OrderBy(x => x.MinOrderID)
                       .ToList();
            //            SELECT
            //    CustomerID,

            //    COUNT(OrderID) AS Num,
            //FROM dbo.Orders
            //GROUP BY CustomerID;
            //            GO
            var orderCounts = _Context.Orders
                       .GroupBy(o => o.CustomerID)
                       .Select(g => new { g.Key, Num = g.Count() })
                       .ToList();
            //            SELECT
            //    EmployeeID, YEAR(OrderDate) AS OrderYear,
            //    COUNT(OrderID) AS NUM,
            //    SUM(Freight) AS Rate
            //FROM dbo.Orders
            //GROUP BY EmployeeID, YEAR(OrderDate)
            //ORDER BY EmployeeID;
            //            GO
            var employeeOrders = _Context.Orders
                .GroupBy(o => new { o.EmployeeID, OrderYear = o.OrderDate.Year })
                .Select(g => new { g.Key.EmployeeID, g.Key.OrderYear, Num = g.Count(), Rate = g.Sum(o => o.Freight) })
                .OrderBy(g => g.EmployeeID)
                .ToList();


            #endregion
            #region Having 
            //SELECT
            //     CustomerID,

            //     COUNT(OrderID) AS Num
            // FROM dbo.Orders
            // GROUP BY CustomerID

            //     HAVING COUNT(OrderID) > 20;
            //             GO
            var customersWithMoreThan20Orders = _Context.Orders
                           .GroupBy(o => o.CustomerID)
                           .Where(g => g.Count() > 20)
                           .Select(g => new { g.Key, Num = g.Count() })
                           .ToList();
            //            SELECT
            //    EmployeeID,

            //    COUNT(OrderID) AS Num
            //FROM dbo.Orders
            //    WHERE EmployeeID<> 8
            //GROUP BY EmployeeID
            //    HAVING COUNT(OrderID) > 70;
            //            GO
            var ordersMoreThan70 = _Context.Orders
                   .Where(o => o.EmployeeID != 8)
                   .GroupBy(o => o.EmployeeID)
                   .Where(g => g.Count() > 70)
                   .Select(g => new { g.Key, Num = g.Count() })
                   .ToList();
            //            SELECT
            //    CustomerID, YEAR(OrderDate) AS OrderYear,
            //    COUNT(OrderID) AS NumOrders,
            //    SUM(Freight) AS TotalFreight
            //FROM dbo.Orders
            //    WHERE CustomerID = 71
            //GROUP BY CustomerID, YEAR(OrderDate);
            //            GO
            var customerOrders71 = _Context.Orders
                 .Where(o => o.CustomerID == 71)
                 .GroupBy(o => new { o.CustomerID, OrderYear = o.OrderDate.Year })
                 .Select(g => new { g.Key.CustomerID, g.Key.OrderYear, NumOrders = g.Count(), TotalFreight = g.Sum(o => o.Freight) })
                 .ToList();
            #endregion
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
