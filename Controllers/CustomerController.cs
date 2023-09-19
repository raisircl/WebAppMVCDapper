using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebAppMVCDapper.Models;
using System.Data.SqlClient;
using Dapper;

namespace WebAppMVCDapper.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IDbConnection conn;

        public CustomerController(IDbConnection conn)
        {
            this.conn = conn; //di
        }
        public IActionResult Index()
        {
            List<Customer> customers = new List<Customer>();
            //customers = conn.Query<Customer>("select * from Customers").ToList();
            customers = conn.Query<Customer>("sp_getcustomers",commandType:CommandType.StoredProcedure).ToList();

            return View(customers);
        }
        public IActionResult Details(int id)
        {
            Customer? customer = new Customer();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);
            customer = conn.Query<Customer>("sp_getcustomerbyid", parameters,commandType: CommandType.StoredProcedure).SingleOrDefault();
            if(customer!=null)
            {
                return View(customer);
            }
            return View(null);
           
            //return Json(customers);
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            //string cmd = "insert into customers(firstname,lastname,email) values(@FirstName,@LastName,@Email)";
            //int affectedrows=conn.Execute(cmd, customer);
            //return RedirectToAction("Index");
            DynamicParameters parameters= new DynamicParameters();
            parameters.Add("@fname", customer.FirstName);
            parameters.Add("@lname", customer.LastName);
            parameters.Add("@email", customer.Email);

            int x = conn.Execute("sp_addcustomer", parameters, commandType: CommandType.StoredProcedure);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Customer? customer = new Customer();
            customer = conn.Query<Customer>($"select * from customers where customerid={id}").SingleOrDefault();
            if (customer != null)
            {
                return View(customer);
            }
            return View(null);
        }
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            string cmd = "update customers set firstname=@FirstName,lastname=@LastName,email=@Email where customerid=@CustomerId";
            int affectedrows = conn.Execute(cmd, customer);
            return RedirectToAction("Index");
        }

      
        public IActionResult Delete(int id)
        {
            Customer? customer = new Customer();
            int x= conn.Execute($"delete  from customers where customerid={id}");
            return RedirectToAction("Index");
        }
    }
}
