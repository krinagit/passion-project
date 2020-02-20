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
using Passionproject2.Models.ViewModel;

namespace Passionproject2.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        private BakeryContext db = new BakeryContext();
        private object selectedOrder;

        public List<Customer> Customer { get; private set; }
        public object Order { get; private set; }

        // GET: Orders/List
        public ActionResult List()
        {
            //How could we modify this to include a search bar?
            List<Order> order = db.Orders.SqlQuery("Select * from Orders").ToList();
          
            
            return View(order);

        }

        public ActionResult New()
        {
            Debug.WriteLine("inserting into Orders");
            //List<Customer> Customer = db.Customers.SqlQuery("Select * from Customers").ToList();

            // Debug.WriteLine("hello");
            //foreach (var c in Customer)
            //{
            //    Debug.WriteLine(c.CustomerName);


            //}
            //return View(customer);
            return View();

        }

        [HttpPost]
        public ActionResult Add(string OrderItem, string OrderDate, int OrderItemQty, decimal OrderCost, int CustomerID)
        {
            //pull the data from the arguments of add  method

            //cost is represented in the system as an integer
            int Cost = (int)(OrderCost * 100);

            string query = "insert into Orders (OrderItem, OrderDate, OrderItemQty,OrderCost,CustomerId) values (@OrderItem, @OrderDate, @OrderItemQty,@OrderCost,@CustomerID)";

            SqlParameter[] sqlparams = new SqlParameter[5];
            sqlparams[0] = new SqlParameter("@OrderItem", OrderItem);
            sqlparams[1] = new SqlParameter("@OrderDate", OrderDate);
            sqlparams[2] = new SqlParameter("@OrderItemQty", OrderItemQty);
            sqlparams[3] = new SqlParameter("@OrderCost", OrderCost);
            sqlparams[4] = new SqlParameter("@CustomerID", CustomerID);


            db.Database.ExecuteSqlCommand(query, sqlparams);

            //the new order will b displayed in the list
            return RedirectToAction("List");
        }

        public ActionResult Show(int? id)
        {
            if (id == null)
            {
           return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        //string query = "select * from Orders where OrderID = @id";
            //    var parameter = new SqlParameter("@id", id);
            Order order = db.Orders.SqlQuery("select * from Orders where OrderID = @id", new SqlParameter("@id", id)).FirstOrDefault();
            if (order == null)
            {
                return HttpNotFound();
            }
          
            //need information about the list of Orders associated with that customer

           string query = "select * from orders inner join customers on Customers.CustomerID = Orders.CustomersID where CustomerID = @id";
            SqlParameter param = new SqlParameter("@id", id);
            List<Customer> Customers = db.Customers.SqlQuery(query, param).ToList();

            List<Customer> all_customers = db.Customers.SqlQuery("Select * from Customers").ToList();



             ShowOrder viewmodel = new ShowOrder();
             viewmodel.Order = order;
             viewmodel.Customer = Customers;
            return View(viewmodel);
        }

         public ActionResult Update(int id)
        {
          string query = "select * from Orders where OrderID = @id";
          var parameter = new SqlParameter("@id", id);
          Order order = db.Orders.SqlQuery(query, parameter).FirstOrDefault();

          List<Customer> Customers = db.Customers.SqlQuery("select * from Customers").ToList();

            UpdateOrder updateOrder = new UpdateOrder();
            updateOrder.Order = order;
            updateOrder.Customers = Customers;


          return View(updateOrder);

        }
        [HttpPost]
        public ActionResult Update(int id, string OrderItem, DateTime OrderDate, int OrderItemQty, decimal OrderCost, int CustomerId)
        {


            string query = "update Orders set OrderItem = @OrderItem, OrderDate = @OrderDate, OrderItemQty=@OrderItemQty, OrderCost=@OrderCost where OrderID = @id";

            //cost is represented in the system as an integer
            int Cost = (int)(OrderCost * 100);

            Debug.WriteLine("Cost is" + Cost);

            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@id", id);
            sqlparams[1] = new SqlParameter("@OrderItem", OrderItem);
            sqlparams[2] = new SqlParameter("@OrderDate", OrderDate);
            sqlparams[3] = new SqlParameter("@OrderItemQty", OrderItemQty);
            sqlparams[4] = new SqlParameter("@OrderCost", OrderCost);
          

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }


        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from Orders where OrderID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Order order = db.Orders.SqlQuery(query, param).FirstOrDefault();


            return View(order);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from Orders where  OrderID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);



            return RedirectToAction("List");
        }
    }
}