using EFCORELEARNING.DATA;
using EFCORELEARNING.DTOs;
using EFCORELEARNING.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace EFCORELEARNING.Controllers
{
    // for test every thing in this Project use debuger to understand what happen!!!!!!
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
            return View();
        }
        public IActionResult Fundamental()
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
            //            SELECT

            //    TOP(5) WITH TIES CustomerID,
            // COUNT(OrderID) AS Num
            //FROM dbo.Orders
            //GROUP BY CustomerID
            //ORDER BY Num DESC;
            //            GO
            var top5Customers = _Context.Orders
                .GroupBy(o => o.CustomerID)
                .Select(g => new { g.Key, Num = g.Count() })
                .OrderByDescending(g => g.Num)
                .Take(5)
                .ToList();
            //            SELECT
            //                EmployeeID,

            //    COUNT(OrderID) AS Num
            //FROM dbo.Orders
            //WHERE EmployeeID BETWEEN 1 AND 3
            //GROUP BY ALL EmployeeID
            //ORDER BY EmployeeID; GO
            var resultde = (from order in _Context.Orders
                         where order.EmployeeID >= 1 && order.EmployeeID <= 3
                         group order by order.EmployeeID into g
                         orderby g.Key
                         select new
                         {
                             EmployeeID = g.Key,
                             Num = g.Count()
                         }).ToList();
            //            SELECT
            //    EmployeeID,

            //    COUNT(OrderID) AS Num
            //FROM dbo.Orders
            //    WHERE EmployeeID BETWEEN 1 AND 3
            //GROUP BY ALL EmployeeID

            //    HAVING COUNT(OrderID) > 100
            //ORDER BY EmployeeID;
            //            GO
            //            
            var resultty = (from order in _Context.Orders
                           where order.EmployeeID >= 1 && order.EmployeeID <= 3
                         group order by order.EmployeeID into g
                         where g.Count() > 100 //having
                         orderby g.Key
                         select new
                         {
                             EmployeeID = g.Key,
                             Num = g.Count()
                         }).ToList();



            #endregion
            return View();
        }
        public IActionResult JoinsAndSetOpertores()
        {
            #region Cross join=(*)
            //SELECT
            // CustomerID, EmployeeID
            //FROM dbo.Customers
            //CROSS JOIN dbo.Employees;
            //GO
            var result = (from customer in _Context.Customers
                         from employee in _Context.Employees
                         select new
                         {
                             customer.CurtomerId,
                             employee.EmployeeID
                         }).ToList();

            #endregion
            #region SELFE JOIN 
            // SELECT

            //E1.FirstName, E1.LastName,
            //E2.FirstName, E2.LastName
            //FROM dbo.Employees AS E1
            //CROSS JOIN dbo.Employees AS E2
            //ORDER BY E1.FirstName, E1.LastName;
            //GO
            var resultr = (from e1 in _Context.Employees
                           from e2 in _Context.Employees
                         orderby e1.FirstName, e1.LastName
                         select new
                         {
                             E1_FirstName = e1.FirstName,
                             E1_LastName = e1.LastName,
                             E2_FirstName = e2.FirstName,
                             E2_LastName = e2.LastName
                         }).ToList();

            #endregion
            #region INNER JOIN=natural join>>have same property
            //SELECT

            //E.FirstName, E.LastName,
            //O.OrderID
            //FROM dbo.Employees AS E
            //JOIN dbo.Orders AS O
            //ON E.EmployeeID = O.EmployeeID;
            // GO
            var resultn = (from e in _Context.Employees
                         join o in _Context.Orders on e.EmployeeID equals o.EmployeeID
                         select new
                         {
                             e.FirstName,
                             e.LastName,
                             o.OrderID
                         }).ToList();
            // SELECT

            // C.City,
            //COUNT(O.OrderID) AS Num
            //FROM dbo.Customers AS C
            //JOIN dbo.Orders AS O
            // ON C.CustomerID = O.CustomerID
            //GROUP BY C.City
            //HAVING COUNT(O.OrderID) > 50;
            // GO
            // 
            var resultj = (from c in _Context.Customers
                         join o in _Context.Orders on c.CurtomerId equals o.CustomerID
                         group o by c.City into g
                         where g.Count() > 50 //having
                         select new
                         {
                             City = g.Key,
                             Num = g.Count()
                         }).ToList();
            //SELECT

            //TOP(3) WITH TIES P.ProductName,
            //SUM(OD.Qty) AS Total
            //FROM dbo.Products AS P
            //JOIN dbo.OrderDetails AS OD
            //ON P.ProductID = OD.ProductID
            //GROUP BY P.ProductName
            //ORDER BY Total DESC;
            //GO
            var grouped = from od in _Context.OrderDetails
                          join p in _Context.Products on od.ProductID equals p.ProductID
                          group od by p.ProductName into g
                          select new
                          {
                              ProductName = g.Key,
                              Total = g.Sum(x => (int)x.Qty)
                          };

            var topTotal = grouped
                          .OrderByDescending(g => g.Total)
                          .Take(3)
                          .ToList();

            int thirdValue = topTotal.Last().Total;//  use for TOP (3) WITH TIES

            var resulte = (grouped
                .Where(g => g.Total >= thirdValue)
                .OrderByDescending(g => g.Total)).ToList();//TOP (3) WITH TIES


            #endregion
            #region Muilti join 
            //SELECT

            //  C.CompanyName,
            //O.OrderID,
            //OD.ProductID,
            //OD.Qty
            //FROM dbo.Customers AS C
            //JOIN dbo.Orders AS O
            // ON C.CustomerID = O.CustomerID
            //JOIN dbo.OrderDetails AS OD
            //ON O.OrderID = OD.OrderID;
            //  GO
            var resulttt = from c in _Context.Customers
                         join o in _Context.Orders on c.CurtomerId equals o.CustomerID into ordersGroup
                         from o in ordersGroup.DefaultIfEmpty()
                         join od in _Context.OrderDetails on o.OrderID equals od.OrderID into detailsGroup
                         from od in detailsGroup.DefaultIfEmpty()
                         where o != null // اگر بخوای فقط سفارش‌هایی که وجود داره، این شرط رو بذار، یا حذفش کن
                         select new
                         {
                             CompanyName = c.CompanyName,
                             OrderID = o != null ? o.OrderID : 0,
                             ProductID = od != null ? od.ProductID : 0,
                             Qty = od != null ? od.Qty : (short)0
                         };

            var list = resulttt.ToList();
            //SELECT

            // C.CustomerID, C.CompanyName,
            //COUNT(DISTINCT O.OrderID) AS NumOrders,
            //SUM(OD.Qty) AS TotalQuantity
            //FROM dbo.Customers AS C
            //JOIN dbo.Orders AS O
            //ON C.CustomerID = O.CustomerID
            //JOIN dbo.OrderDetails AS OD
            //ON O.OrderID = OD.OrderID

            //WHERE C.State = N'تهران'
            //GROUP BY C.CustomerID, C.CompanyName;
            // GO
            //buge!!!!!!!!!!!!!!!!!!!!!!
            var resultrt = _Context.Customers
                .Where(c => c.State == "تهران")
                .Join(_Context.Orders, c => c.CurtomerId, o => o.CustomerID, (c, o) => new { c, o })
                .Join(_Context.OrderDetails, co => co.o.OrderID, od => od.OrderID, (co, od) => new { co.c, co.o, od })
                .GroupBy(x => new { x.c.CurtomerId, x.c.CompanyName })
                .Select(g => new
                {
                    CustomerID = g.Key.CurtomerId,
                    CompanyName = g.Key.CompanyName,
                    NumOrders = g.Select(x => x.o.OrderID).Distinct().Count(),
                    TotalQuantity = g.Sum(x => x.od.Qty)
                })
                .ToList();




            #endregion
            #region OuterJoin 
            // SELECT

            // C.CustomerID, C.CompanyName,
            //O.OrderID,
            //OD.ProductID,
            //OD.Qty
            //FROM dbo.Customers AS C
            //LEFT JOIN dbo.Orders AS O
            // ON C.CustomerID = O.CustomerID
            //LEFT JOIN dbo.OrderDetails AS OD
            // ON O.OrderID = OD.OrderID;
            // GO
            
            var query = _Context.Customers
                               .GroupJoin(_Context.Orders,
                               c => c.CurtomerId,
                               o => o.CustomerID,
                               (c, ordersGroup) => new { c, ordersGroup })
                               .SelectMany(
                               x => x.ordersGroup.DefaultIfEmpty(),
                               (x, o) => new { x.c, o })
                               .GroupJoin(_Context.OrderDetails,
                               co => co.o != null ? co.o.OrderID : 0,
                               od => od.OrderID,
                               (co, detailsGroup) => new { co.c, co.o, detailsGroup })
                               .SelectMany(
                               x => x.detailsGroup.DefaultIfEmpty(),
                               (x, od) => new
                               {
                               CustomerID = x.c.CurtomerId,
                               CompanyName = x.c.CompanyName,
                               OrderID = x.o != null ? x.o.OrderID : (int?)null,
                               ProductID = od != null ? od.ProductID : (int?)null,
                               Qty = od != null ? od.Qty : (short?)null
                               })
                               .ToList();
            //SQL Query
                      var sql = @"
                             SELECT
                                 C.CurtomerId, C.CompanyName,
                                 O.OrderID,
                                 OD.ProductID,
                                 OD.Qty
                             FROM dbo.Customers AS C
                             LEFT JOIN dbo.Orders AS O
                                 ON C.CurtomerId = O.CustomerID
                             LEFT JOIN dbo.OrderDetails AS OD
                                 ON O.OrderID = OD.OrderID";

            var resultdf = _Context.Set<CustomerOrderDetailDto>()
                                  .FromSqlRaw(sql)
                                  .ToList();


            #endregion
            #region Set Operator
            //SELECT
            // State, Region, City FROM dbo.Employees
            //UNION ALL
            //SELECT

            // State, Region, City FROM dbo.Customers
            //ORDER BY Region;
            // GO
                               var resultghj = _Context.Employees
                   .Select(e => new { e.State, e.Region, e.City })
                   .Concat(
                   _Context.Customers
                   .Select(c => new { c.State, c.Region, c.City })
                   )
                   .OrderBy(x => x.Region)
                   .ToList();

            //SELECT
            // State, Region, City FROM dbo.Employees
            //INTERSECT
            //SELECT
            // State, Region, City FROM dbo.Customers;
            // GO
            //buge !!!!!!!!!!!!!!!!
            var employeeLocations = _Context.Employees
                .Select(e => new { e.State, e.Region, e.City });

            var customerLocations = _Context.Customers
                .Select(c => new { c.State, c.Region, c.City });

            var intersected = employeeLocations
                .Intersect(customerLocations)
                .ToList();

            var employeeLocationsm = _Context.Employees
           .Select(e => new { e.State, e.Region, e.City });

            var customerLocationsm = _Context.Customers
                .Select(c => new { c.State, c.Region, c.City });

            var exceptResult = employeeLocations
                .Except(customerLocations)
                .ToList();




            #endregion
            return View();
        }
        public IActionResult Question()
        {
            #region Q1
            //What is the difference in days between the newest and oldest order for each customer from Isfahan
            /*            SELECT
                         TOP(1) WITH TIES C.CustomerID,
                        COUNT(O.OrderID) AS Num
                        FROM dbo.Customers AS C
                        JOIN dbo.Orders AS O
                         ON C.CustomerID = O.CustomerID
                        WHERE C.City = N'تهران'
                        GROUP BY C.CustomerID
                        ORDER BY Num;
                        GO*/
      
          
            var query = (from c in _Context.Customers
                         join o in _Context.Orders on c.CurtomerId equals o.CustomerID
                         where c.City == "تهران"
                         group o by c.CurtomerId into g
                         select new
                         {
                             CustomerId = g.Key,
                             Num = g.Count()
                         })
                        .ToList();

            if (query.Any())
            {
                var minOrderCount = query.Min(x => x.Num);

                var topCustomers = query
                    .Where(x => x.Num == minOrderCount)
                    .ToList();

               
            }
            else
            {
                Console.WriteLine("No Way.");
            }



            #endregion
            #region Q2
            //How many orders of the product 'Orange Juice' have customers from the provinces Tehran or Kerman placed?"
            /*            SELECT
                C.CustomerID,C.State,
                SUM(OD.Qty) AS Total
            FROM dbo.Customers AS C
            JOIN dbo.Orders AS O
                ON C.CustomerID = O.CustomerID
            JOIN dbo.OrderDetails AS OD
                ON O.OrderID = OD.OrderID
            JOIN dbo.Products AS P
                ON OD.ProductID = P.ProductID
                WHERE P.ProductName = N'آب پرتقال'
                AND C.City IN(N'تهران',N'کرمان')
            GROUP BY C.CustomerID, C.State;
                        GO*/
            var result = (from c in _Context.Customers
                          join o in _Context.Orders on c.CurtomerId equals o.CustomerID
                          join od in _Context.OrderDetails on o.OrderID equals od.OrderID
                          join p in _Context.Products on od.ProductID equals p.ProductID
                          where p.ProductName == "آب پرتقال"
                                && 
                                (c.City == "تهران" || c.City == "کرمان")
                          group od by new { c.CurtomerId, c.City } into g
                          select new
                          {
                              CustomerID = g.Key.CurtomerId,
                              State = g.Key.City,
                              Total = g.Sum(x => x.Qty)
                          }).ToList();
            #endregion
            #region Q3
            //What is the difference in days between the newest and oldest order of each customer from Isfahan?"
            /*            SELECT
                C.CustomerID,
                DATEDIFF(DAY, MIN(O.OrderDate), MAX(O.OrderDate)) AS Day_Diff
            FROM dbo.Customers AS C
            JOIN dbo.Orders AS O
                ON C.CustomerID = O.CustomerID
                WHERE C.State = N'اصفهان'
            GROUP BY C.CustomerID;
                        GO*/
            var result55 = (from c in _Context.Customers
                          join o in _Context.Orders on c.CurtomerId equals o.CustomerID
                          where c.City == "اصفهان"
                          group o by c.CurtomerId into g
                          select new
                          {
                              CustomerID = g.Key,
                              Day_Diff = EF.Functions.DateDiffDay(g.Min(x => x.OrderDate), g.Max(x => x.OrderDate))
                          })
              .ToList();


            #endregion
            #region Q4
            //Which customers have never set any orders and have their State field as NULL
            /*            SELECT
                C.CustomerID
            FROM Customers AS C
            LEFT JOIN dbo.Orders AS O
                ON C.CustomerID = O.CustomerID
                WHERE C.State IS NULL
            GROUP BY C.CustomerID
                HAVING COUNT(O.OrderID) = 0;
                        GO*/
            var customersWithoutOrders = _Context.Customers
                             .Where(c => c.State == null)
                             .GroupJoin(
                                 _Context.Orders,
                                 c => c.CurtomerId,
                                 o => o.CustomerID,
                                 (c, orders) => new { Customer = c, Orders = orders }
                             )
                             .Where(co => !co.Orders.Any()) 
                             .Select(co => co.Customer.CurtomerId)
                             .ToList();

            #endregion
            #region Q5
            //How many order for Customer who live in Zanjan?
            /*            SELECT
                C.CustomerID,
                COUNT(O.OrderID) AS Num
            FROM dBO.Customers AS C
            LEFT JOIN dbo.Orders AS O
                ON C.CustomerID = O.CustomerID
                WHERE C.City = N'زنجان'
            GROUP BY C.CustomerID;
                        GO*/
            var resultdd = _Context.Customers
                              .Where(c => c.City == "زنجان")
                              .GroupJoin(
                                  _Context.Orders,
                                  c => c.CurtomerId,
                                  o => o.CustomerID,
                                  (c, orders) => new
                                  {
                                      CustomerID = c.CurtomerId,
                                      Num = orders.Count()
                                  }
                              )
                                  .ToList();

            #endregion
            #region Q6
            //Which customer has old more than 50 and his or her orders more than 100?
            var resultmkn = _Context.Employees
                               .Where(e => EF.Functions.DateDiffYear(e.Birthdate, DateTime.Now) > 50)
                               .Join(
                                   _Context.Orders,
                                   e => e.EmployeeID,
                                   o => o.EmployeeID,
                                   (e, o) => new { e.LastName, e.EmployeeID }
                               )
                               .GroupBy(x => x.LastName)
                               .Where(g => g.Count() > 100)
                               .Select(g => new
                               {
                                   LastName = g.Key,
                                   Num_Orders = g.Count()
                               })
                               .ToList();


            /*            SELECT
                E.LastName,
                COUNT(O.OrderID) AS Num_Orders
            FROM dbo.Employees AS E
            JOIN dbo.Orders AS O
                ON E.EmployeeID = O.EmployeeID
                WHERE DATEDIFF(YEAR, E.Birthdate, GETDATE()) > 50
            GROUP BY E.LastName
                HAVING COUNT(O.OrderID) > 100;
                        GO*/
            #endregion
            #region Q7
            //have list of customer that did not set order from 2015-05-01?
            /*            SELECT
                E.EmployeeID, E.FirstName, E.LastName
            FROM dbo.Employees AS E
            EXCEPT
            SELECT
                E.EmployeeID, E.FirstName, E.LastName
            FROM dbo.Employees AS E
            JOIN dbo.Orders AS O
                ON E.EmployeeID = O.EmployeeID
                WHERE O.OrderDate BETWEEN '20160501' AND GETDATE()
            GROUP BY E.EmployeeID, E.FirstName, E.LastName;
                        GO*/
            var today = DateTime.Now;
            var fromDate = new DateTime(2016, 5, 1);

            // Step 1: 
            var employeesWithOrdersInRange = _Context.Orders
                .Where(o => o.OrderDate >= fromDate && o.OrderDate <= today)
                .Select(o => o.EmployeeID)
                .Distinct();

            // Step 2: 
            var resultxcv = _Context.Employees
                .Where(e => !employeesWithOrdersInRange.Contains(e.EmployeeID))
                .Select(e => new
                {
                    e.EmployeeID,
                    e.FirstName,
                    e.LastName
                })
                .ToList();

            #endregion
            #region Q8
            //number of orders in second of 6 months for each year 
            /*            SELECT
             YEAR(OrderDate)AS OrderDate,
             COUNT(OrderID)AS NUM
            FROM Orders
                WHERE MONTH(OrderDate) > 6
            GROUP BY ALL YEAR(OrderDate);
                        GO*/
            var resultxv = _Context.Orders
                          .Where(o => o.OrderDate.Month > 6)
                          .GroupBy(o => o.OrderDate.Year)
                          .Select(g => new
                          {
                              OrderDate = g.Key,
                              NUM = g.Count()
                          });
            string sql = resultxv.ToQueryString();
            Console.WriteLine(sql);
            var resultdfgh = resultxv.ToList();


            #endregion
            return View();
        }
        public IActionResult Subquery()
        {
            #region  01 - Self-Contained Scalar-Valued
            //Independent subquery for the most recently placed order
            //SELECT
            // EmployeeID, CustomerID, OrderID, OrderDate
            //FROM dbo.Orders
            // WHERE OrderID = (SELECT MAX(OrderID) FROM dbo.Orders);
            // GO
            var latestOrder =_Context.Orders
                        .Where(o => o.OrderID == _Context.Orders.Max(x => x.OrderID))
                        .Select(o => new {
                        o.EmployeeID,
                        o.CustomerID,
                        o.OrderID,
                        o.OrderDate
                        })
                        .FirstOrDefault();
            //"-- Independent subquery to get the number of orders per customer along with the total number of orders placed"
            // SELECT
            // CustomerID,

            // COUNT(OrderID) AS Num,
            //(SELECT COUNT(OrderID) FROM dbo.Orders) AS Total--یک بار اجرا میشود
            //FROM dbo.Orders
            //GROUP BY CustomerID;
            //GO
            var totalOrderCount = (from o in _Context.Orders
                                   select o).Count();

            var result = (from o in _Context.Orders
                          group o by o.CustomerID into g
                          select new
                          {
                              CustomerID = g.Key,
                              Num = g.Count(),
                              Total = totalOrderCount
                          }).ToList();
            #endregion
            #region 02 - Self-Contained Multi-Valued
            //SELECT
            // EmployeeID, OrderID
            //FROM dbo.Orders
            // WHERE EmployeeID IN(SELECT E.EmployeeID FROM dbo.Employees AS E
            // WHERE E.lastname LIKE N'ت%'); --کارمندان: تقوی / تهرانی
            //GO
            var employeeIds = (from e in _Context.Employees
                               where e.LastName.StartsWith("ت")
                               select e.EmployeeID).ToList();

            var orders = (from o in _Context.Orders
                          where employeeIds.Contains(o.EmployeeID)
                          select new
                          {
                              o.EmployeeID,
                              o.OrderID
                          }).ToList();
            //SELECT* FROM dbo.Customers
            //WHERE CustomerID NOT IN(SELECT CustomerID FROM dbo.Orders);
            //GO
           //1>>8ms
            var orderedCustomerIds = _Context.Orders
                .Select(o => o.CustomerID)
                .Distinct()
                .ToList();
            var customersWithoutOrders1 = _Context.Customers
                .Where(c => !orderedCustomerIds.Contains(c.CurtomerId));
                
            Console.WriteLine(customersWithoutOrders1.ToQueryString());
            var ListOfOrders1 = customersWithoutOrders1.ToList();
            //2.This is more efficient than 1 >>3ms
            var customersWithoutOrders2 = _Context.Customers
                        .GroupJoin(
                            _Context.Orders,
                            c => c.CurtomerId,
                            o => o.CustomerID,
                            (c, orders) => new { Customer = c, Orders = orders }
                        )
                        .Where(co => !co.Orders.Any())
                        .Select(co => co.Customer);
                        
            Console.WriteLine(customersWithoutOrders2.ToQueryString());
            var ListOfOrders2 = customersWithoutOrders2.ToList();
            #endregion
            #region 03 - Correlated Subquery
            //"-- SELECT using a correlated subquery to display the most recent order ID for each customer"
            //in select
            //SELECT
            //DISTINCT O1.CustomerID,
            //(SELECT MAX(O2.OrderID) FROM dbo.Orders AS O2
            //WHERE O1.CustomerID = O2.CustomerID) AS NewOrder
            //FROM dbo.Orders AS O1;
            //  GO
            var result2 = (from o in _Context.Orders
                           group o by o.CustomerID into g
                           select new
                           {
                               CustomerID = g.Key,
                               NewOrder = g.Max(x => x.OrderID)
                           }).ToList();
            //in from
            //SELECT
            //O1.CustomerID,
            //O1.OrderID
            //FROM dbo.Orders AS O1
            //WHERE O1.OrderID = (SELECT MAX(O2.OrderID) FROM dbo.Orders AS O2
            //WHERE O1.CustomerID = O2.CustomerID);
            //GO
            var latestOrders = (from o1 in _Context.Orders
                               where o1.OrderID == (from o2 in _Context.Orders
                               where o2.CustomerID == o1.CustomerID
                               select o2.OrderID).Max()
                               select new
                               {
                                   o1.CustomerID,
                                   o1.OrderID
                               }).ToList();
            //3
            //SELECT* FROM dbo.Customers AS C
            //WHERE EXISTS(SELECT 1 FROM dbo.Orders AS O
            //WHERE C.CustomerID = O.CustomerID);
            //GO
                   var customersWithOrders = _Context.Customers
                         .Where(c => _Context.Orders.Any(o => o.CustomerID == c.CurtomerId))
                         .ToList();
            //  SELECT* FROM dbo.Customers AS C
            // WHERE NOT EXISTS(SELECT 1 FROM dbo.Orders AS O
            // WHERE C.CustomerID = O.CustomerID);
            // GO
            var customersWithoutOrders = (from c in _Context.Customers
                                          where !(from o in _Context.Orders
                                                  where o.CustomerID == c.CurtomerId
                                                  select o).Any()
                                          select c).ToList();


            #endregion
            #region 01 - Derived Table
            // SELECT* FROM
            //(SELECT
            // C.CompanyName,
            //(SELECT COUNT(O.OrderID) FROM dbo.Orders AS O
            //WHERE C.CustomerID = O.CustomerID
            //HAVING COUNT(O.OrderID) > 10) AS Num
            //FROM dbo.Customers AS C) AS Tmp

            // WHERE Tmp.Num > 10;
            //  GO 
            //1Method Syntax
            var resultjj = _Context.Customers
                        .Where(c => _Context.Orders.Count(o => o.CustomerID == c.CurtomerId) > 10)
                        .Select(c => new {
                        c.CompanyName,
                        Num = _Context.Orders.Count(o => o.CustomerID == c.CurtomerId)
                        })
                        .ToList();
            //2GroupJoin 
            var resultjjj = _Context.Customers
                              .GroupJoin(
                                  _Context.Orders,
                                  c => c.CurtomerId,
                                  o => o.CustomerID,
                                  (c, orders) => new {
                                      c.CompanyName,
                                      Num = orders.Count()
                                  })
                              .Where(x => x.Num > 10)
                              .ToList();
            //CTE
            /*  WITH CustPerYear
  AS
  (
      SELECT

          YEAR(OrderDate) AS OrderYear,
          CustomerID

      FROM dbo.Orders
  )
  SELECT

      CY.OrderYear,
      COUNT(DISTINCT CY.CustomerID) AS Customers_Num
  FROM CustPerYear AS CY
  GROUP BY CY.OrderYear;
              GO*/
                      var resultjjjj = _Context.Orders
                                 .GroupBy(o => o.OrderDate.Year)
                                 .Select(g => new {
                                 OrderYear = g.Key,
                                 Customers_Num = g
                                 .Select(o => o.CustomerID)
                                 .Distinct()
                                 .Count()
                                 })
                                 .ToList();


            #endregion
            return View();
        }
        public IActionResult CreateIndex()
        {
            #region Simple Index
            int customerId = 5; 

            var orders = _Context.Orders
                                .Where(o => o.CustomerID == customerId)
                                .OrderByDescending(o => o.OrderDate)
                                .ToList();
            #endregion
            #region ViewIndex
            var summaries = _Context.Set<CustomerOrderIndex>().ToList();
            #endregion

            return View();
        }
        public IActionResult Procdure()
        {
            //DerivedTable
            #region Outer Query: Orders cost of 44%
            //            SELECT Tmp.CustomerID, Tmp.Num FROM
            //            (
            //            SELECT
            //                O.CustomerID,
            //                (SELECT COUNT(OD.OrderID) FROM dbo.OrderDetails AS OD
            //                    WHERE O.OrderID = OD.OrderID

            //                    HAVING COUNT(OrderID) > 5) AS Num
            //FROM dbo.Orders AS O
            //GROUP BY O.CustomerID, O.OrderID
            //) AS Tmp
            //WHERE Tmp.Num IS NOT NULL
            //GROUP BY Tmp.CustomerID, Tmp.Num;
            //            GO
            #endregion
            #region  Derived Table   cost of 14%
            //            SELECT Tmp.CustomerID, Tmp.Num FROM
            //(
            //SELECT
            //    O.CustomerID,
            //    (SELECT COUNT(OD.OrderID) FROM dbo.OrderDetails AS OD
            //        WHERE O.OrderID = OD.OrderID) AS Num
            //FROM dbo.Orders AS O
            //GROUP BY O.CustomerID, O.OrderID
            //) AS Tmp
            //WHERE Tmp.Num > 5
            //GROUP BY Tmp.CustomerID, Tmp.Num;
            //            GO
            #endregion
            var result = _Context.CustomerOrderSummaries
                          .FromSqlRaw("EXEC GetCustomersWithMoreThanFiveOrderDetails")
                          .ToList();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
