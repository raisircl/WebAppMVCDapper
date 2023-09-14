using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebAppMVCDapper.Models;
using System.Data.SqlClient;
using Dapper;

namespace WebAppMVCDapper.Controllers
{
    public class CustomerController : Controller
    {

        public IActionResult Index()
        {
            string constr = @"Data Source=.\sqlexpress;Initial Catalog=sircldb;Persist Security Info=True;User ID=sa;Password=rai11**";
            List<Customer> customers = new List<Customer>();
            IDbConnection conn = new SqlConnection(constr);
            customers=conn.Query<Customer>("select * from Customers").ToList();
            //return Json(customers);
            return View(customers);
        }
        public IActionResult Details(int id)
        {
            string constr = @"Data Source=.\sqlexpress;Initial Catalog=sircldb;Persist Security Info=True;User ID=sa;Password=rai11**";
            Customer? customer = new Customer();  
            IDbConnection conn = new SqlConnection(constr);
            customer = conn.Query<Customer>($"select * from customers where customerid={id}").SingleOrDefault();
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
            string constr = @"Data Source=.\sqlexpress;Initial Catalog=sircldb;Persist Security Info=True;User ID=sa;Password=rai11**";
            
            IDbConnection conn = new SqlConnection(constr);
            string cmd = "insert into customers(firstname,lastname,email) values(@FirstName,@LastName,@Email)";
            int affectedrows=conn.Execute(cmd, customer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            string constr = @"Data Source=.\sqlexpress;Initial Catalog=sircldb;Persist Security Info=True;User ID=sa;Password=rai11**";
            Customer? customer = new Customer();
            IDbConnection conn = new SqlConnection(constr);
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
            string constr = @"Data Source=.\sqlexpress;Initial Catalog=sircldb;Persist Security Info=True;User ID=sa;Password=rai11**";

            IDbConnection conn = new SqlConnection(constr);
            string cmd = "update customers set firstname=@FirstName,lastname=@LastName,email=@Email where customerid=@CustomerId";
            int affectedrows = conn.Execute(cmd, customer);
            return RedirectToAction("Index");
        }

      
        public IActionResult Delete(int id)
        {
            string constr = @"Data Source=.\sqlexpress;Initial Catalog=sircldb;Persist Security Info=True;User ID=sa;Password=rai11**";
            Customer? customer = new Customer();
            IDbConnection conn = new SqlConnection(constr);
            int x= conn.Execute($"delete  from customers where customerid={id}");

            return RedirectToAction("Index");
        }
    }
}
