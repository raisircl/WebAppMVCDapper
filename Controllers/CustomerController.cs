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
    }
}
