using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Passionproject2.Data;
using Passionproject2.Models;
using System.Diagnostics;

namespace Passionproject2.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        private BakeryContext db = new BakeryContext();

        // GET:Customer
        public ActionResult List()
        {
            var customers = db.Customers.SqlQuery("Select * from Customers").ToList();

            return View(customers);
        }

        // GET: Customer/Details
        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = db.Customers.SqlQuery("select * from Customers where customerid=@CustomerID", new SqlParameter("@CustomerID", id)).FirstOrDefault();
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //THE [HttpPost] Means that this method will only be activated on a POST form submit to the following URL
        //URL: /Customer/Add
        [HttpPost]
        public ActionResult Add(string CustomerName, string Emailid, string Phone, string CustomerAddress)
        {

            //Debug.WriteLine("Want to create a customer with name " + BakeryName + " and emailid"+ Emailid +"and Phone" +Phone + " and address " + CustomerAddress) ;

            string query = "insert into Customers (CustomerName,Emailid,Phone, CustomerAddress) values (@CustomerName,@Emailid,@Phone,@CustomerAddress)";
            SqlParameter[] sqlparams = new SqlParameter[4]; //0,1,2,3,4 pieces of information to add

            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@CustomerName", CustomerName);
            sqlparams[1] = new SqlParameter("@Emailid", Emailid);
            sqlparams[2] = new SqlParameter("@Phone", Phone);
            sqlparams[3] = new SqlParameter("@CustomerAddress", CustomerAddress);


            //db.Database.ExecuteSqlCommand will run insert, update, delete statements
            //db.Customer.SqlCommand will run a select statement, for example.
            db.Database.ExecuteSqlCommand(query, sqlparams);


            //run the list method to return to a list of Customers so we can see our new one!
            return RedirectToAction("List");
        }

        public ActionResult New()
        {

            return View();
        }
        public ActionResult Update(int id)
        {
            //need information about a particular customer
            Customer customer = db.Customers.SqlQuery("select * from Customers where CustomerID = @CustomerID", new SqlParameter("@CustomerID", id)).FirstOrDefault();
            return View(customer);
        }

        [HttpPost]
        public ActionResult Update(int id, string CustomerName, string Emailid, string Phone, string CustomerAddress)
        {


            //Debug.WriteLine("I am trying to edit a customer's name " + BakeryName + " and emailid"+ Emailid +"and Phone" +Phone + " and address " + CustomerAddress);

            string query = "update Customers set CustomerName=@CustomerName, Emailid=@Emailid, Phone=@Phone,CustomerAddress=@CustomerAddress where CustomerID=@CustomerID";
            SqlParameter[] sqlparams = new SqlParameter[5];
            sqlparams[0] = new SqlParameter("@CustomerID", id);
            sqlparams[1] = new SqlParameter("@CustomerName", CustomerName);
            sqlparams[2] = new SqlParameter("@Emailid", Emailid);
            sqlparams[3] = new SqlParameter("@Phone", Phone);
            sqlparams[4] = new SqlParameter("@CustomerAddress", CustomerAddress);


            db.Database.ExecuteSqlCommand(query, sqlparams);

            //logic for updating the bakery in the database goes here
            return RedirectToAction("List");
        }
        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from Customers where CustomerID = @CustomerID";
            SqlParameter param = new SqlParameter("@CustomerID", id);
            Customer customer = db.Customers.SqlQuery(query, param).FirstOrDefault();

            return View(customer);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from Customers where CustomerID = @CustomerID";
            SqlParameter param = new SqlParameter("@CustomerID", id);
            db.Database.ExecuteSqlCommand(query, param);

            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}