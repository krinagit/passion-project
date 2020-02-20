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
    public class BakeryController : Controller
    {
        private BakeryContext db = new BakeryContext();

        // GET: Bakery
        public ActionResult List()
        {
            var bakeries = db.Bakeries.SqlQuery("Select * from Bakeries").ToList();

            return View(bakeries);
        }

        // GET: Bakery/Details
        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Bakery bakery = db.Bakeries.SqlQuery("select * from Bakeries where bakeryid=@BakeryID", new SqlParameter("@BakeryID", id)).FirstOrDefault();
            if (bakery == null)
            {
                return HttpNotFound();
            }
            return View(bakery);
        }

        //THE [HttpPost] Means that this method will only be activated on a POST form submit to the following URL
        //URL: /Bakery/Add
        [HttpPost]
        public ActionResult Add(string BakeryName, string BakeryAddress)
        {

            //Debug.WriteLine("Want to create a bakery with name " + BakeryName + " and address " + BakeryAddress) ;

            string query = "insert into Bakeries (BakeryName, BakeryAddress) values (@BakeryName,@BakeryAddress)";
            SqlParameter[] sqlparams = new SqlParameter[2]; //0,1 pieces of information to add

            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@BakeryName", BakeryName);
            sqlparams[1] = new SqlParameter("@BakeryAddress", BakeryAddress);


            //db.Database.ExecuteSqlCommand will run insert, update, delete statements
            //db.Bakery.SqlCommand will run a select statement, for example.
            db.Database.ExecuteSqlCommand(query, sqlparams);


            //run the list method to return to a list of bakeries so we can see our new one!
            return RedirectToAction("List");
        }

        public ActionResult New()
        {

            return View();
        }

        public ActionResult Update(int id)
        {
            //need information about a particular bakery
            Bakery bakery = db.Bakeries.SqlQuery("select * from Bakeries where BakeryID = @BakeryID", new SqlParameter("@BakeryID", id)).FirstOrDefault();
            return View(bakery);
        }

        [HttpPost]
        public ActionResult Update(int id, string BakeryName, string BakeryAddress)
        {


            //Debug.WriteLine("I am trying to edit a bakery's name to "+BakeryName+" and change address to "+BakeryAddress);

            string query = "update Bakeries set BakeryName=@BakeryName, BakeryAddress=@BakeryAddress where BakeryID=@BakeryID";
            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@BakeryID", id);
            sqlparams[1] = new SqlParameter("@BakeryName", BakeryName);
            sqlparams[2] = new SqlParameter("@BakeryAddress", BakeryAddress);


            db.Database.ExecuteSqlCommand(query, sqlparams);

            //logic for updating the bakery in the database goes here
            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from Bakeries where BakeryID = @BakeryID";
            SqlParameter param = new SqlParameter("@BakeryID", id);
            Bakery bakery = db.Bakeries.SqlQuery(query, param).FirstOrDefault();

            return View(bakery);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from Bakeries where BakeryID = @BakeryID";
            SqlParameter param = new SqlParameter("@BakeryID", id);
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